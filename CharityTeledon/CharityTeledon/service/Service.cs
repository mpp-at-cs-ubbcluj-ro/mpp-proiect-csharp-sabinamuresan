using System;
using System.Collections.Generic;
using System.Linq;
using CharityTeledon.model;
using CharityTeledon.repository;

namespace CharityTeledon.service
{
    public class Service
    {
        private ICaseRepository CaseRepository;
        private IDonationRepository DonationRepository;
        private IDonorRepository DonorRepository;
        private IVolunteerRepository VolunteerRepository;

        public Service(ICaseRepository caseRepository, IDonationRepository donationRepository, IDonorRepository donorRepository, IVolunteerRepository volunteerRepository)
        {
            CaseRepository = caseRepository;
            DonationRepository = donationRepository;
            DonorRepository = donorRepository;
            VolunteerRepository = volunteerRepository;
        }
        
        public Case FindOneCase(int id){
            return CaseRepository.FindOne(id);
        }

        public IEnumerable<Case> GetAllCases(){
            return CaseRepository.GetAll();
        }

        public IEnumerable<Donation> GetAllDonations(){
            return DonationRepository.GetAll();
        }

        public Donation AddDonation(Donation donation){
            UpdateSumInCase(donation.IdCase, donation.Amount);
            return DonationRepository.Add(donation);
        }

        public Donor AddDonor(Donor donor){
            return DonorRepository.Add(donor);
        }

        public IEnumerable<Donor> GetAllDonorsForCase(Case myCase){
            return DonorRepository.GetDonorsForCase(myCase);
        }
        public IEnumerable<Donor> GetAllDonors(){
            return DonorRepository.GetAll();
        }

        public Donor FindDonorByName(string name){
            return DonorRepository.FindByName(name);
        }

        public Volunteer FindVolunteerAccount(string username, string password){
            return VolunteerRepository.FindVolunteerAccount(username, password);
        }

        public void UpdateSumInCase(int idCase, float amount)
        {
            Case myCase = FindOneCase(idCase);
            float sum = myCase.Sum;
            sum += amount;
            CaseRepository.UpdateSum(idCase, sum);
        }
    }
}