using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityTeledon.model
{
    public class Donor:Entity<int>
    {
        public string DonorName { get; set; }
        public string DonorAddress { get; set; }
        public string DonorPhoneNumber { get; set; }

        public Donor(int id, string donorName, string donorAddress, string donorPhoneNumber)
        {
            this.Id = id;
            this.DonorName = donorName;
            this.DonorAddress = donorAddress;
            this.DonorPhoneNumber = donorPhoneNumber;
        }
        
        public Donor(string donorName, string donorAddress, string donorPhoneNumber)
        {
            this.DonorName = donorName;
            this.DonorAddress = donorAddress;
            this.DonorPhoneNumber = donorPhoneNumber;
        }
        public override string ToString()
        {
            return "{" + base.ToString() + " " + DonorName + "," + DonorAddress + "," + DonorPhoneNumber + "}";
        }
    }
}
