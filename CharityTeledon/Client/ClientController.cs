using System;
using System.Collections.Generic;
using CharityTeledon.model;
using Services;

namespace Client
{
    public class ClientController: IObserver
    {
        public event EventHandler<TeledonUserEventArgs> updateEvent;
        private readonly IServices server;

        private Volunteer loggedUser;

        public ClientController(IServices server)
        {
            this.server = server;
            loggedUser = null;
        }

        public Volunteer getLoggedUser()
        {
            return loggedUser;
        }
        public void notifyCaseUpdated(Case myCase)
        {
            TeledonUserEventArgs userEventArgs = new TeledonUserEventArgs(TeledonUserEvent.UpdatedCase, myCase.Id);
            OnUserEvent(userEventArgs);
        }

        public void notifyDonorAdded(Donor donor)
        {
            TeledonUserEventArgs userEventArgs = new TeledonUserEventArgs(TeledonUserEvent.AddedDonor, donor.Id);
            OnUserEvent(userEventArgs);
        }
        
        protected virtual void OnUserEvent(TeledonUserEventArgs e)
        {
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called");
        }

        public Volunteer Login(string username, string password)
        {
            Volunteer volunteer =  server.Login(username, password, this);
            loggedUser = volunteer;
            return volunteer;
        }

        public void Logout(Volunteer volunteer)
        {
            server.Logout(volunteer, this);
            loggedUser = null;
        }

        public IEnumerable<Case> GetAllCases()
        {
            return server.GetAllCases();
        }

        public IEnumerable<Donor> GetAllDonors()
        {
            return server.GetAllDonors();
        }

        public Donor FindDonorByName(string name)
        {
            try
            {
                return server.FindDonorByName(name);
            }
            catch (TeledonException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Donor AddDonor(Donor donor)
        {
            return server.AddDonor(donor);
        }

        public Donation AddDonation(Donation donation)
        {
            Console.WriteLine("Add donation in Client Controller");
            return server.AddDonation(donation);
        }
    }
}