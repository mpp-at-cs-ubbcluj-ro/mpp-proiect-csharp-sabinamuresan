using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using CharityTeledon.model;
using Services;

namespace Networking
{
    public class ServerObjectProxy : IServices
    {
        private string host;
	    private int port;
	    private IObserver client;
	    private NetworkStream stream;
	    private IFormatter formatter;
	    private TcpClient connection;
	    private Queue<Response> responses;
	    private volatile bool finished;
	    private EventWaitHandle _waitHandle;

	    public ServerObjectProxy(string host, int port) 
	    {
		    this.host = host;
		    this.port = port;
		    responses = new Queue<Response>();
	    }

	    public Case FindOneCase(int id)
	    { 
		    throw new System.NotImplementedException();
	    }

	    public IEnumerable<Case> GetAllCases()
	    {
		    Console.WriteLine("Proxy GetAllCases");
		    sendRequest(new GetCasesRequest());
		    Response response = readResponse();
		    if (response is ErrorResponse)
		    {
			    ErrorResponse err = (ErrorResponse)response;
			    throw new TeledonException(err.Message);
		    }

		    GetCasesResponse rsp = (GetCasesResponse)response;
		    CaseDTO[] casesDto =rsp.cases;
		    Case[] cases = DTOUtils.getFromDTO(casesDto);
		    return cases;

	    }

	    public IEnumerable<Donation> GetAllDonations()
	    {
		    throw new System.NotImplementedException();
	    }

	    public Donation AddDonation(Donation donation)
	    {
		    Console.WriteLine("Proxy AddDonation" + donation);
		    DonationDTO donationDto = new DonationDTO(donation.Id, donation.IdCase, donation.IdDonor, donation.Amount);
		    sendRequest(new AddDonationRequest(donationDto));
		    Console.WriteLine("Request sent");
		    Response response = readResponse();
		    Console.WriteLine("Response recieved");
		    if (response is ErrorResponse)
		    {
			    ErrorResponse err = (ErrorResponse)response;
			    throw new TeledonException(err.Message);
		    }

		    return donation;
	    }

	    public Donor AddDonor(Donor donor)
	    {
		    Console.WriteLine("Proxy AddDonor"); 
		    DonorDTO donorDto = new DonorDTO(donor.Id, donor.DonorName, donor.DonorAddress, donor.DonorPhoneNumber);
		    sendRequest(new AddDonorRequest(donorDto));
		    Response response = readResponse();
		    if (response is ErrorResponse)
		    {
			    ErrorResponse err = (ErrorResponse)response;
			    throw new TeledonException(err.Message);
		    }

		    return donor;
	    }

	    public IEnumerable<Donor> GetAllDonorsForCase(Case myCase)
	    {
		    throw new System.NotImplementedException();
	    }

	    public IEnumerable<Donor> GetAllDonors()
	    {
		    Console.WriteLine("Proxy GetAllDonors");
		    sendRequest(new GetDonorsRequest());
		    Response response = readResponse();
		    if (response is ErrorResponse)
		    {
			    ErrorResponse err = (ErrorResponse)response;
			    throw new TeledonException(err.Message);
		    }

		    GetDonorsResponse rsp = (GetDonorsResponse)response;
		    DonorDTO[] donorsDto =rsp.donors;
		    Donor[] donors = DTOUtils.getFromDTO(donorsDto);
		    return donors;
	    }

	    public Donor FindDonorByName(string name)
	    {
		    Console.WriteLine("Proxy FindDonorByName...");
		    sendRequest(new GetDonorByNameRequest(name));
		    Console.WriteLine("Request sent");
		    Response response = readResponse();
		    Console.WriteLine("Response recieved");
		    if (response is ErrorResponse)
		    {
			    ErrorResponse err = (ErrorResponse)response;
			    throw new TeledonException(err.Message);
		    }
		    Console.WriteLine("OkResponse...");
		    GetDonorByNameResponse donorRsp = (GetDonorByNameResponse)response;
		    DonorDTO donorDto = donorRsp.donor;
		    Donor donor = DTOUtils.getFromDTO(donorDto);
		    return donor;
	    }

	    public Volunteer Login(string username, string password, IObserver client)
	    {
		    Console.WriteLine("Proxy login...");
		    initializeConnection();
		    Console.WriteLine("Connection initialised");
		    Volunteer volunteer = new Volunteer(username, password);
		    VolunteerDTO volunteerDto = DTOUtils.getDTO(volunteer);
		    sendRequest(new LoginRequest(volunteerDto));
		    Console.WriteLine("Request sent");
		    Response response = readResponse();
		    Console.WriteLine("Response read");
		    if (response is OkLoginResponse)
		    {
			    Console.WriteLine("OkLoginResponse for Login in Proxy");
			    OkLoginResponse logRsp = (OkLoginResponse)response;
			    Volunteer vol = DTOUtils.getFromDTO(logRsp.volunteerDto);
			    this.client = client;
			    return vol;
		    }
		    if (response is ErrorResponse)
		    {
			    ErrorResponse err = (ErrorResponse)response;
			    closeConnection();
			    throw new TeledonException(err.Message);
		    }

		    return null;
	    }

                public void UpdateSumInCase(int idCase, float amount)
                {
	                Console.WriteLine("Proxy UpdateSumInCase...");
	                sendRequest(new UpdateCaseRequest(idCase, amount));
	                Console.WriteLine("Request sent");
	                Response response = readResponse();
	                Console.WriteLine("Response recieved");
	                if (response is ErrorResponse)
	                {
		                ErrorResponse err = (ErrorResponse)response;
		                throw new TeledonException(err.Message);
	                }
                }

                public void Logout(Volunteer volunteer, IObserver client)
                {
	                VolunteerDTO volunteerDto = DTOUtils.getDTO(volunteer);
	                sendRequest(new LogoutRequest(volunteerDto));
	                Response response = readResponse();
	                closeConnection();
	                if (response is ErrorResponse)
	                {
		                ErrorResponse err = (ErrorResponse)response;
		                throw new TeledonException(err.Message);
	                }
                }
                
                private void initializeConnection()
                {
	                try
	                {
		                connection = new TcpClient(host, port);
		                stream = connection.GetStream();
		                formatter = new BinaryFormatter();
		                finished = false;
		                _waitHandle = new AutoResetEvent(false);
		                startReader();
	                }
	                catch (Exception e)
	                {
		                Console.WriteLine(e.StackTrace);
	                }
                }
                
                private void closeConnection()
                {
	                finished = true;
	                try
	                {
		                stream.Close();
		                connection.Close();
		                _waitHandle.Close();
		                client = null;
	                }
	                catch (Exception e)
	                {
		                Console.WriteLine(e.StackTrace);
	                }

                }
                
                private void startReader()
                {
	                Thread tw = new Thread(run);
	                tw.Start();
                }
                
                private void sendRequest(Request request)
                {
	                try
	                {
		                formatter.Serialize(stream, request);
		                stream.Flush();
	                }
	                catch (Exception e)
	                {
		                throw new TeledonException("Error sending object " + e);
	                }

                }
                private Response readResponse()
                {
	                Response response = null;
	                try
	                {
		                _waitHandle.WaitOne();
		                lock (responses)
		                {
			                response = responses.Dequeue();
		                }
	                }
	                catch (Exception e)
	                {
		                Console.WriteLine(e.StackTrace);
	                }
	                return response;
                }
                
                public virtual void run()
                {
	                while (!finished)
	                {
		                try
		                {
			                object response = formatter.Deserialize(stream);
			                Console.WriteLine("response received " + response);
			                if (response is UpdatedCaseResponse || response is AddedDonorResponse)
			                {
				                handleUpdate((Response)response);
			                }
			                else
			                {
				                lock (responses)
				                {


					                responses.Enqueue((Response)response);

				                }
				                _waitHandle.Set();
			                }
		                }
		                catch (Exception e)
		                {
			                Console.WriteLine("Reading error " + e);
		                }

	                }
                }
                private void handleUpdate(Response update)
                {
	                if (update is UpdatedCaseResponse)
	                {
		                Console.WriteLine("handleMadeDonation");
		                UpdatedCaseResponse caseUpdate = (UpdatedCaseResponse)update;
		                Case myCase = DTOUtils.getFromDTO(caseUpdate.caseDto);
		                try
		                {
			                client.notifyCaseUpdated(myCase);
			                Console.WriteLine("CaseUpdate notify done");
		                }
		                catch (TeledonException e)
		                {
			                Console.WriteLine(e.StackTrace);
		                }
	                }
	                else
	                {
		                Console.WriteLine("handleAddedDonor");
		                AddedDonorResponse donorUpdate = (AddedDonorResponse)update;
		                Donor donor = DTOUtils.getFromDTO(donorUpdate.donor);
		                try
		                {
							client.notifyDonorAdded(donor);
		                }
		                catch (TeledonException e)
		                {
			                Console.WriteLine(e.StackTrace);
		                }
		                
	                }
                }

    }
}