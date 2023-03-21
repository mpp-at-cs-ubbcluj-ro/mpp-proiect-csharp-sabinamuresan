using System;
using System.Collections.Generic;
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
    }
}