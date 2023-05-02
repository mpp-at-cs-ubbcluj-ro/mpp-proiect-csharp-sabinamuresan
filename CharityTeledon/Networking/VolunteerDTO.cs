using System;

namespace Networking
{
    [Serializable]
    public class VolunteerDTO
    {
        public int id { get; }
        public string username { get; set; }
        public string password { get; set; }

        public VolunteerDTO(int id, string username, string password)
        {
            this.id = id;
            this.username = username;
            this.password = password;
        }
    }
}