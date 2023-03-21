using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityTeledon.model
{
    public class Case:Entity<int>
    { 
        public string CaseName { get; set; } 
        public float Sum { get; set; }

        public Case(int id, string caseName, float sum): base(id)
        {
            this.CaseName = caseName;
            this.Sum = sum;
        }

        public void AddToSum(float Sum)
        {
            this.Sum += Sum;
        }

        public override string ToString()
        {
            return "{" + base.ToString() + " " + CaseName + "," + Sum + "}";
        }
    }
}
