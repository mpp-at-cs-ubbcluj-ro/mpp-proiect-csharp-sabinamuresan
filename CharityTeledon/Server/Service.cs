using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CharityTeledon.model;
using CharityTeledon.repository;
using Services;

namespace Server
{
    public class Service: IServices
    {
        
        private ICaseRepository CaseRepository;
        private IDonationRepository DonationRepository;
        private IDonorRepository DonorRepository;
        private IVolunteerRepository VolunteerRepository;
        private readonly IDictionary<Int32, IObserver> loggedClients;

        public Service(ICaseRepository caseRepository, IDonationRepository donationRepository, IDonorRepository donorRepository, IVolunteerRepository volunteerRepository)
        {
            CaseRepository = caseRepository;
            DonationRepository = donationRepository;
            DonorRepository = donorRepository;
            VolunteerRepository = volunteerRepository;
            loggedClients = new Dictionary<Int32, IObserver>();
        }

        public Case FindOneCase(int id)
        {
            Console.WriteLine("In FindOneCase (Server Services)");
            return CaseRepository.FindOne(id);
        }

        public IEnumerable<Case> GetAllCases()
        {
            return CaseRepository.GetAll();
        }

        public IEnumerable<Donation> GetAllDonations()
        {
            return DonationRepository.GetAll();
        }

        public IEnumerable<Donor> GetAllDonors()
        {
            return DonorRepository.GetAll();
        }

        public Donor FindDonorByName(string name)
        {
            Console.WriteLine("Searching donor with name " + name + "in Server Service");
            return DonorRepository.FindByName(name);
        }

        public Volunteer Login(string username, string password, IObserver client)
        {
            Volunteer volunteer = VolunteerRepository.FindVolunteerAccount(username, password);
            if (volunteer != null)
            {
                if (loggedClients.ContainsKey(volunteer.Id))
                    throw new TeledonException("User already logged in.");
                loggedClients[volunteer.Id] = client;
            }
            else
            {
                throw new TeledonException("Autentification failed.");
            }

            return volunteer;
        }

        public void UpdateSumInCase(int idCase, float amount)
        {
            Console.WriteLine("In UpdateSumInCase (Server Services)");
            Case myCase = FindOneCase(idCase);
            Console.WriteLine(myCase.CaseName);
            float sum = myCase.Sum;
            sum += amount;
            CaseRepository.UpdateSum(idCase, sum);
            foreach (IObserver client in loggedClients.Values)
            {
                Task.Run(() => client.notifyCaseUpdated(myCase));
            }
            
        }

        public IEnumerable<Donor> GetAllDonorsForCase(Case myCase)
        {
            return DonorRepository.GetDonorsForCase(myCase);
        }

        public Donor AddDonor(Donor donor)
        {
            Donor addedDonor = DonorRepository.Add(donor);
            foreach (IObserver client in loggedClients.Values)
            {
                Task.Run(() => client.notifyDonorAdded(addedDonor));
            }
            return addedDonor;
        }

        public Donation AddDonation(Donation donation)
        {
            Console.WriteLine("In AddDonation (Server Services)" + donation);
            UpdateSumInCase(donation.IdCase, donation.Amount);
            return DonationRepository.Add(donation);
        }

        public void Logout(Volunteer volunteer, IObserver client)
        {
            IObserver localClient = loggedClients[volunteer.Id];
            if (localClient == null)
                throw new TeledonException("User " + volunteer.Id + " is not logged in.");
            loggedClients.Remove(volunteer.Id);
        }
    }
}