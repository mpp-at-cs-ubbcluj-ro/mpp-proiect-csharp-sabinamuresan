using System;
using System.Collections.Generic;
using System.Data;
using CharityTeledon.model;
using log4net;

namespace CharityTeledon.repository
{

    public class DonorDbRepository : IDonorRepository
    {
        private static readonly ILog log = LogManager.GetLogger("DonorDbRepository");
        private IDictionary<String, string> props;

        public DonorDbRepository(IDictionary<String, string> props)
        {
            log.Info("Creating DonorDbRepository ");
            this.props = props;
        }

        public Donor FindOne(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Donor> GetAll()
        {
            throw new NotImplementedException();
        }

        public Donor Add(Donor entity)
        {
            log.InfoFormat("Entering Add value {0}", entity);
            IDbConnection con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "INSERT INTO Donors(name, address, phone_number)  values (@name, @address, @phoneNumber)";
                var paramName = comm.CreateParameter();
                paramName.ParameterName = "@name";
                paramName.Value = entity.DonorName;
                comm.Parameters.Add(paramName);

                var paramAddress = comm.CreateParameter();
                paramAddress.ParameterName = "@address";
                paramAddress.Value = entity.DonorAddress;
                comm.Parameters.Add(paramAddress);

                var paramPhoneNumber = comm.CreateParameter();
                paramPhoneNumber.ParameterName = "@phoneNumber";
                paramPhoneNumber.Value = entity.DonorPhoneNumber;
                comm.Parameters.Add(paramPhoneNumber);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    log.Error("No donation added !");
                    throw new Exception("No donor added !");
                }
            }
            log.InfoFormat("Succesfully added value {0}", entity);
            return entity;
        }

        public Donor Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Donor Update(Donor entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Donor> GetDonorsForCase(Case c)
        {
            log.InfoFormat("Entering GetDonorsForCase for value {0}", c);
            IDbConnection con = DBUtils.getConnection(props);
            IList<Donor> donors = new List<Donor>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText =
                    "SELECT * FROM Donors INNER JOIN Donations D ON D.id_donor = Donors.id INNER JOIN Cases C ON D.id_case = C.id and C.id = @idCase";
                var paramIdCase = comm.CreateParameter();
                paramIdCase.ParameterName = "@idCase";
                paramIdCase.Value = c.Id;
                comm.Parameters.Add(paramIdCase);
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        String name = dataR.GetString(1);
                        String address = dataR.GetString(2);
                        String phoneNumber = dataR.GetString(3);
                        Donor donor = new Donor(id, name, address, phoneNumber);
                        donors.Add(donor);
                    }
                }
            }
            log.Info("Exiting GetDonorsForCase");
            return donors;
        }
    }
}