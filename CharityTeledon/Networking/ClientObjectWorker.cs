using System;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using CharityTeledon.model;
using Services;

namespace Networking
{
    public class ClientObjectWorker: IObserver
    {
        private IServices server;
        private TcpClient connection;
        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;

        public ClientObjectWorker(IServices server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                connected = true;
            }
            catch (Exception e)
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
                    object request = formatter.Deserialize(stream);
                    object response = handleRequest((Request)request);
                    if (response != null)
                    {
                        sendResponse((Response)response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);;
                }

                try
                {
                    Thread.Sleep(1000);
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
        public void notifyCaseUpdated(Case myCase)
        {
            CaseDTO caseDto = DTOUtils.getDTO(myCase);
            try
            {
                sendResponse(new UpdatedCaseResponse(caseDto));
            }
            catch (TeledonException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void notifyDonorAdded(Donor donor)
        {
            DonorDTO donorDto = DTOUtils.getDTO(donor);
            try
            {
                sendResponse(new AddedDonorResponse(donorDto));
            }
            catch (TeledonException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        private void sendResponse(Response response)
        {
            Console.WriteLine("sending response "+response);
            lock (stream)
            {
                formatter.Serialize(stream, response);
                stream.Flush();
            }

        }
        
        private Response handleRequest(Request request)
        {
            Response response = null;
            if (request is LoginRequest)
            {
                Console.WriteLine("Login request ...");
                LoginRequest logReq = (LoginRequest)request;
                VolunteerDTO volunteerDto = logReq.Volunteer;
                Volunteer volunteer = DTOUtils.getFromDTO(volunteerDto);
                try
                {
                    Volunteer vol = null;
                    lock (server)
                    {
                        Console.WriteLine("ClientWorker login");
                        vol = server.Login(volunteer.Username, volunteer.Password, this);
                    }
                    return new OkLoginResponse(DTOUtils.getDTO(vol));
                }
                catch (TeledonException e)
                {
                    connected = false;
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is LogoutRequest)
            {
                Console.WriteLine("Logout request ...");
                LogoutRequest logReq = (LogoutRequest)request;
                VolunteerDTO volunteerDto = logReq.Volunteer;
                Volunteer volunteer = DTOUtils.getFromDTO(volunteerDto);
                try
                {
                    lock (server)
                    {
                        Console.WriteLine("ClientWorker logout");
                        server.Logout(volunteer, this);
                    }
                    return new OkResponse();
                }
                catch (TeledonException e)
                {
                    connected = false;
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is GetCasesRequest)
            {
                Console.WriteLine("GetCasesRequest Request ...");
                GetCasesRequest getReq = (GetCasesRequest)request;
                try
                {
                    Case[] cases;
                    lock (server)
                    {
                        cases = server.GetAllCases().ToArray();
                        Console.WriteLine(cases);
                    }
                    CaseDTO[] casesDTO = DTOUtils.getDTO(cases);
                    return new GetCasesResponse(casesDTO);
                }
                catch (TeledonException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is GetDonorsRequest)
            {
                Console.WriteLine("GetDonorsRequest Request ...");
                GetDonorsRequest getReq = (GetDonorsRequest)request;
                try
                {
                    Donor[] donors;
                    lock (server)
                    {
                        donors = server.GetAllDonors().ToArray();
                    }

                    DonorDTO[] donorsDTO = DTOUtils.getDTO(donors);
                    return new GetDonorsResponse(donorsDTO);
                }
                catch (TeledonException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is AddDonationRequest)
            {
                Console.WriteLine("AddDonationRequest Request ...");
                AddDonationRequest addReq = (AddDonationRequest)request;
                DonationDTO donationDto = addReq.donation;
                Donation donation = DTOUtils.getFromDTO(donationDto);
                try
                {
                    lock (server)
                    {
                        
                        server.AddDonation(donation);
                    }

                    return new OkResponse();
                }
                catch (TeledonException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is AddDonorRequest)
            {
                Console.WriteLine("AddDonorRequest Request ...");
                AddDonorRequest addReq = (AddDonorRequest)request;
                DonorDTO donorDto = addReq.donor;
                Donor donor = DTOUtils.getFromDTO(donorDto);
                try
                {
                    lock (server)
                    {
                        
                        server.AddDonor(donor);
                    }

                    return new OkResponse();
                }
                catch (TeledonException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is GetDonorByNameRequest)
            {
                Console.WriteLine("GetDonorByNameRequest Request ...");
                GetDonorByNameRequest getReq = (GetDonorByNameRequest)request;
                string name = getReq.name;
                try
                {
                    Donor donor = null;
                    lock (server)
                    {
                        donor = server.FindDonorByName(name);
                    }
                    if (donor is null)
                    {
                        return new ErrorResponse("It doesn't exist a donor with this name");
                    }
                    else
                    {
                        DonorDTO donorDto = DTOUtils.getDTO(donor);
                        return new GetDonorByNameResponse(donorDto);
                    }
                }
                catch (TeledonException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is UpdateCaseRequest)
            {
                Console.WriteLine("UpdateCaseRequest Request ...");
                UpdateCaseRequest updateReq = (UpdateCaseRequest)request;
                int idCase = updateReq.idCase;
                float amount = updateReq.amount;
                try
                {
                    lock (server)
                    {
                        server.UpdateSumInCase(idCase, amount);
                    }

                    return new OkResponse();
                }
                catch (TeledonException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            return response;
        }
    }
}