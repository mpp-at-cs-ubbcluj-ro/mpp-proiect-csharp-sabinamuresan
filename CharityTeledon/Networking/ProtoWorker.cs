using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using Google.Protobuf;
using Services;
using Teledon.Protocol;
using Case = CharityTeledon.model.Case;
using Donor = CharityTeledon.model.Donor;

namespace Networking
{
    public class ProtoWorker : IObserver
    {
        private IServices server;
        private TcpClient connection;
        private NetworkStream stream;
        private volatile bool connected;
        
        public ProtoWorker(IServices server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {

                stream = connection.GetStream();
                connected = true;
            }
            catch (TeledonException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        
        public virtual void run()
        {
            while (connected)
            {
                try
                {

                    TeledonRequest request = TeledonRequest.Parser.ParseDelimitedFrom(stream);
                    TeledonResponse response = handleRequest(request);
                    if (response != null)
                    {
                        sendResponse(response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(10);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }

        private TeledonResponse handleRequest(TeledonRequest request)
        {
            TeledonResponse response = null;
            TeledonRequest.Types.Type reqType = request.Type;
            if (reqType == TeledonRequest.Types.Type.Login)
            {
                Console.WriteLine("Login request ...");
                String username = ProtoUtils.getVolunteerUsername(request);
                String password = ProtoUtils.getVolunteerPassword(request);
                CharityTeledon.model.Volunteer user = null;
                try
                {
                    lock (server)
                    {
                        user = server.Login(username, password, this);
                    }
                    return ProtoUtils.createLoginResponse(user);
                }
                catch (Exception e)
                {
                    connected = false;
                    return ProtoUtils.createErrorResponse(e.Message);
                }
            }

            if (reqType == TeledonRequest.Types.Type.Logout)
            {
                Console.WriteLine("Logout request");
                CharityTeledon.model.Volunteer user = ProtoUtils.getUser(request);
                try
                {
                    lock (server)
                    {

                        server.Logout(user, this);
                    }
                    connected = false;
                    return ProtoUtils.createOKResponse();

                }
                catch (Exception e)
                {
                    return ProtoUtils.createErrorResponse(e.Message);
                }
            }

            if (reqType == TeledonRequest.Types.Type.GetCase)
            {
                Console.WriteLine("GetCase request");
                int idCase = ProtoUtils.getCaseId(request);
                Console.WriteLine("GetCase with id" + idCase);
                try
                {
                    Case myCase = null;
                    lock (server)
                    {
                        myCase = server.FindOneCase(idCase);
                    }
                    Console.WriteLine(myCase);
                    return ProtoUtils.createGetCaseResponse(myCase);
                }
                catch (TeledonException e)
                {
                    return ProtoUtils.createErrorResponse(e.Message);
                }
            }

            if (reqType == TeledonRequest.Types.Type.GetDonorByName)
            {
                Console.WriteLine("GetDonorByName request");
                string donorName = ProtoUtils.getDonorName(request);
                try
                {
                    Donor donor = null;
                    lock (server)
                    {
                        donor = server.FindDonorByName(donorName);
                    }

                    if (donor is null)
                    {
                        return ProtoUtils.createErrorResponse("It doesn't exist a donor with this name");
                    }
                    return ProtoUtils.createGetDonorResponse(donor);
                }
                catch (TeledonException e)
                {
                    return ProtoUtils.createErrorResponse(e.Message);
                }
            }

            if (reqType == TeledonRequest.Types.Type.GetCases)
            {
                Console.WriteLine("GetCases request");
                try
                {
                    List<Case> cases;
                    lock (server)
                    {
                        cases = (List<Case>)server.GetAllCases();
                    }

                    return ProtoUtils.createGetCasesResponse(cases);
                }
                catch (TeledonException e)
                {
                    return ProtoUtils.createErrorResponse(e.Message);
                }
            }
            if (reqType == TeledonRequest.Types.Type.GetDonors)
            {
                Console.WriteLine("GetDonors request");
                try
                {
                    List<Donor> donors;
                    lock (server)
                    {
                        donors = (List<Donor>)server.GetAllDonors();
                    }

                    return ProtoUtils.createGetDonorsResponse(donors);
                }
                catch (TeledonException e)
                {
                    return ProtoUtils.createErrorResponse(e.Message);
                }
            }

            if (reqType == TeledonRequest.Types.Type.UpdateSumInCase)
            {
                Console.WriteLine("UpdateSumInCase request");
                int caseId = ProtoUtils.getCaseId(request);
                float amount = ProtoUtils.getAmount(request);
                try
                {
                    lock (server)
                    {
                        server.UpdateSumInCase(caseId, amount);
                    }

                    return ProtoUtils.createOKResponse();
                }
                catch (TeledonException e)
                {
                    return ProtoUtils.createErrorResponse(e.Message);
                }
            }

            if (reqType == TeledonRequest.Types.Type.AddDonation)
            {
                Console.WriteLine("AddDonation request");
                CharityTeledon.model.Donation donation = ProtoUtils.getDonation(request);
                try
                {
                    lock (server)
                    {
                        server.AddDonation(donation);
                    }
                    return ProtoUtils.createOKResponse();
                }
                catch (TeledonException e)
                {
                    return ProtoUtils.createErrorResponse(e.Message);
                }
            }

            if (reqType == TeledonRequest.Types.Type.AddDonor)
            {
                Console.WriteLine("AddDonor request");
                CharityTeledon.model.Donor donor = ProtoUtils.getDonor(request);
                try
                {
                    lock (server)
                    {
                        server.AddDonor(donor);
                    }

                    return ProtoUtils.createOKResponse();
                }
                catch (TeledonException e)
                {
                    return ProtoUtils.createErrorResponse(e.Message);
                }
            }

            return response;
        }
        
        private void sendResponse(TeledonResponse response)
        {
            Console.WriteLine("sending response " + response);
            lock (stream)
            {
                response.WriteDelimitedTo(stream);
                stream.Flush();
            }

        }
        
        public void notifyCaseUpdated(Case myCase)
        {
            Console.WriteLine("CaseUpdated request");
            try
            {
                sendResponse(ProtoUtils.createCaseUpdatedResponse(myCase));
            }
            catch(TeledonException e)
            {
                throw new Exception("sending error... " + e);
            }
        }

        public void notifyDonorAdded(Donor donor)
        {
            Console.WriteLine("AddedDonor request");
            try
            {
                sendResponse(ProtoUtils.createAddedDonorResponse(donor));
            }
            catch(TeledonException e)
            {
                throw new Exception("sending error... " + e);
            }
        }
    }
}