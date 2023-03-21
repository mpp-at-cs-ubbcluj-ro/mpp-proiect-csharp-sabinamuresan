using System;
using System.Collections.Generic;
using System.Configuration;
using CharityTeledon.model;
using CharityTeledon.repository;

namespace CharityTeledon
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //configurare jurnalizare folosind log4net
            //XmlConfigurator.Configure(new System.IO.FileInfo(args[0])); 
            IDictionary<String, string> props = new SortedList<String, String>();
            props.Add("ConnectionString", GetConnectionStringByName("teledonDB"));
        
            CaseDbRepository caseRepository = new CaseDbRepository(props);
            DonationDbRepository donationRepository = new DonationDbRepository(props);
            DonorDbRepository donorRepository = new DonorDbRepository(props);
            VolunteerDbRepository volunteerRepository = new VolunteerDbRepository(props);
        
            //Tests
            foreach (var c in caseRepository.GetAll())
            {
                Console.WriteLine(c);
            }
            Console.WriteLine(caseRepository.FindOne(1));
            donationRepository.Add(new Donation(0, 120, 2, 3));
            foreach (var d in donationRepository.GetAll())
            {
                Console.WriteLine(d);
            }

            donorRepository.Add(new Donor(0, "Maria Turc", "Rodnei 36", "0756221395"));
            foreach (var d in donorRepository.GetDonorsForCase(caseRepository.FindOne(1)))
            {
                Console.WriteLine(d);
            }

        }
        
        static string GetConnectionStringByName(string name) 
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings =ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}