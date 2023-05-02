using System;
using System.Collections.Generic;
using System.Data;
using CharityTeledon.model;
using log4net;

namespace CharityTeledon.repository
{

    public class DonationDbRepository : IDonationRepository
    {
        private static readonly ILog log = LogManager.GetLogger("DonationDbRepository");
        private IDictionary<String, string> props;

        public DonationDbRepository(IDictionary<String, string> props)
        {
            log.Info("Creating DonationDbRepository ");
            this.props = props;
        }

        public Donation FindOne(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Donation> GetAll()
        {
            IDbConnection con = DBUtils.getConnection(props);
            IList<Donation> donations = new List<Donation>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Donations";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        float amount = dataR.GetFloat(1);
                        int idCase = dataR.GetInt32(2);
                        int idDonor = dataR.GetInt32(3);
                        Donation donation = new Donation(id, amount, idCase, idDonor);
                        donations.Add(donation);
                    }
                }
            }

            return donations;
        }

        public Donation Add(Donation entity)
        {
            log.InfoFormat("Entering Add value {0}", entity);
            IDbConnection con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "INSERT INTO Donations(amount, id_case, id_donor)  values (@amount, @idCase, @idDonor)";
                var paramAmount = comm.CreateParameter();
                paramAmount.ParameterName = "@amount";
                paramAmount.Value = entity.Amount;
                comm.Parameters.Add(paramAmount);

                var paramIdCase = comm.CreateParameter();
                paramIdCase.ParameterName = "@idCase";
                paramIdCase.Value = entity.IdCase;
                comm.Parameters.Add(paramIdCase);

                var paramIdDonor = comm.CreateParameter();
                paramIdDonor.ParameterName = "@idDonor";
                paramIdDonor.Value = entity.IdDonor;
                comm.Parameters.Add(paramIdDonor);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    log.Error("No donation added !");
                    throw new Exception("No donation added !");
                }
            }
            log.InfoFormat("Succesfully added value {0}", entity);
            return entity;
        }

        public Donation Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Donation Update(Donation entity)
        {
            throw new NotImplementedException();
        }
    }
}