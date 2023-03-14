using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teledon.model
{
    public class Volunteer:Entity<int>
    {
        private string username { get; set; }
        private string password { get; set; }

        public Volunteer(int id, string username, string password): base(id)
        {
            this.username = username;   
            this.password = password;
        }
    }
}
