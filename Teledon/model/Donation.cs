using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teledon.model
{
    internal class Donation:Entity<int>
    {
        private Case caseToDonate { get; set; }
        private Donor donor { get; set; }
        private float amount { get; set; }

        public Donation(int id, Case caseToDonate, Donor donor, float amount): base(id)
        {
            this.caseToDonate = caseToDonate;
            this.donor = donor;
            this.amount = amount;
        }
    }
}
