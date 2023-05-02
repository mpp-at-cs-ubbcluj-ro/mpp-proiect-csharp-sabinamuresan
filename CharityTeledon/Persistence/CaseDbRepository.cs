using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using CharityTeledon.model;

namespace CharityTeledon.repository
{

    public class CaseDbRepository : ICaseRepository
    {
        private static readonly ILog log = LogManager.GetLogger("CaseDbRepository");
        private IDictionary<String, string> props;

        public CaseDbRepository(IDictionary<String, string> props)
        {
            log.Info("Creating CaseDbRepository");
            this.props = props;
        }

        public Case FindOne(int id)
        {
            log.InfoFormat("Entering findOne with value {0}", id);
            IDbConnection con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Cases WHERE id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idCase = dataR.GetInt32(0);
                        String name = dataR.GetString(1);
                        float sum = dataR.GetFloat(2);
                        Case myCase = new Case(idCase, name, sum);
                        log.InfoFormat("Exiting findOne with value {0}", myCase);
                        return myCase;
                    }
                }
            }

            log.InfoFormat("Exiting findOne with value {0}", null);
            return null;
        }

        public IEnumerable<Case> GetAll()
        {
            log.Info("Entering GetAll ");
            IDbConnection con = DBUtils.getConnection(props);
            IList<Case> cases = new List<Case>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Cases";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        String name = dataR.GetString(1);
                        float sum = dataR.GetFloat(2);
                        Case myCase = new Case(id, name, sum);
                        cases.Add(myCase);
                    }
                }
            }
            log.Info("Exiting GetAll");
            return cases;
        }

        public Case Add(Case entity)
        {
            throw new NotImplementedException();
        }

        public Case Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Case Update(Case entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateSum(int id, float sum)
        {
            log.Info("Entering updateSum ");
            IDbConnection con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "UPDATE Cases SET sum = @sum WHERE id=@id";

                var paramSum = comm.CreateParameter();
                paramSum.ParameterName = "@sum";
                paramSum.Value = sum;
                comm.Parameters.Add(paramSum);
            
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                var result = comm.ExecuteNonQuery();
                if(result == 0)
                    log.InfoFormat("Case with id {0} not updated!",id);
                else log.InfoFormat("Case with id {0} updated successfully!", id);
            }

        }
    }
}