using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityTeledon.model
{
    public class Volunteer:Entity<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Volunteer(int id, string username, string password): base(id)
        {
            this.Username = username;   
            this.Password = password;
        }
    }
}
