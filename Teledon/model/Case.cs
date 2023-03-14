using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teledon.model
{
    internal class Case:Entity<int>
    { 
        private string caseName { get; set; } 
        private float sum { get; set; }

        public Case(int id, string caseName, float sum): base(id)
        {
            this.caseName = caseName;
            this.sum = sum;
        }

        public void addToSum(float sum)
        {
            this.sum += sum;
        }

    
    }
}
