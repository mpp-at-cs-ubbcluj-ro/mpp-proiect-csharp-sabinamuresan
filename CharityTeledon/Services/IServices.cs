using System.Collections.Generic;
using CharityTeledon.model;

namespace Services
{
    public interface IServices
    {
        Case FindOneCase(int id);

        IEnumerable<Case> GetAllCases();

        IEnumerable<Donation> GetAllDonations();

        Donation AddDonation(Donation donation);

        Donor AddDonor(Donor donor);

        IEnumerable<Donor> GetAllDonorsForCase(Case myCase);

        IEnumerable<Donor> GetAllDonors();

        Donor FindDonorByName(string name);

        Volunteer Login(string username, string password, IObserver client);

        void UpdateSumInCase(int idCase, float amount);
        void Logout(Volunteer volunteer, IObserver client);
    }
}