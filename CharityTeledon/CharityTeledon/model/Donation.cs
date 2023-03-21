using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityTeledon.model
{
    public class Donation:Entity<int>
    {
        public float Amount { get; set; }
        public int IdCase { get; set; }
        public int IdDonor { get; set; }

        public Donation(int id, float amount, int IdCase, int IdDonor): base(id)
        {
            this.Amount = amount;
            this.IdCase = IdCase;
            this.IdDonor = IdDonor;
        }
        
        public override string ToString()
        {
            return "{" + base.ToString() + " " + Amount + "," + IdCase + "," + IdDonor + "}";
        }
    }
}
