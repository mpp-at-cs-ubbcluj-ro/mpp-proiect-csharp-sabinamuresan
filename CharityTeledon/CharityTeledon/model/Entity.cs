using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityTeledon.model
{
    public class Entity<ID>
    {
        public ID Id { get; set; }

        public Entity(ID Id)
        {
            this.Id = Id;
        }
    }
}
