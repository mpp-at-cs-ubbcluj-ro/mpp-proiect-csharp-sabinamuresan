using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teledon.model
{
    internal class Donor:Entity<int>
    {
        private string donorName { get; set; }
        private string donorAddress { get; set; }
        private string donorPhoneNumber { get; set; }

        public Donor(int id, string donorName, string donorAddress, string donorPhoneNumber): base(id)
        {
            this.donorName = donorName;
            this.donorAddress = donorAddress;
            this.donorPhoneNumber = donorPhoneNumber;
        }
    }
}
