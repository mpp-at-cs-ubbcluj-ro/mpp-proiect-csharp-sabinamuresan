using System;
using System.Collections.Generic;
using System.Data;
using CharityTeledon.model;
using log4net;

namespace CharityTeledon.repository
{

    public class VolunteerDbRepository : IVolunteerRepository
    {
        private static readonly ILog log = LogManager.GetLogger("VolunteerDbRepository");
        private IDictionary<String, string> props;

        public VolunteerDbRepository(IDictionary<String, string> props)
        {
            log.Info("Creating VolunteerDbRepository ");
            this.props = props;
        }

        public Volunteer FindOne(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Volunteer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Volunteer Add(Volunteer entity)
        {
            throw new NotImplementedException();
        }

        public Volunteer Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Volunteer Update(Volunteer entity)
        {
            throw new NotImplementedException();
        }

        public Volunteer FindVolunteerAccount(string username, string password)
        {
            log.InfoFormat("Entering findVolunteerAccount with username {0}", username);
            IDbConnection con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Volunteers WHERE username=@username AND password=@password";
                IDbDataParameter paramUsername = comm.CreateParameter();
                paramUsername.ParameterName = "@username";
                paramUsername.Value = username;
                comm.Parameters.Add(paramUsername);
                IDbDataParameter paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = password;
                comm.Parameters.Add(paramPassword);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idVolunteer = dataR.GetInt32(0);
                        String usernameVolunteer = dataR.GetString(1);
                        String passwordVolunteer = dataR.GetString(2);
                        Volunteer volunteer = new Volunteer(idVolunteer, usernameVolunteer, passwordVolunteer);
                        log.InfoFormat("Exiting findVolunteerAccount with value {0}", volunteer);
                        return volunteer;
                    }
                }
            }

            log.InfoFormat("Exiting findVolunteerAccount with value {0}", null);
            return null;
        }
    }
}