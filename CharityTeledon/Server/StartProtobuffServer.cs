using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Sockets;
using System.Threading;
using CharityTeledon.repository;
using Networking;
using Services;

namespace Server
{
    public class StartProtobuffServer
    {
        static void Main(string[] args)
        {
            SortedList<string, string> props = new SortedList<String, String>();
            props.Add("ConnectionString", GetConnectionStringByName("teledonDB"));
        
            CaseDbRepository caseRepository = new CaseDbRepository(props);
            DonationDbRepository donationRepository = new DonationDbRepository(props);
            DonorDbRepository donorRepository = new DonorDbRepository(props);
            VolunteerDbRepository volunteerRepository = new VolunteerDbRepository(props);
            IServices service = new Service(caseRepository, donationRepository, donorRepository, volunteerRepository);
            Console.WriteLine(caseRepository.FindOne(1));

            ProtoTeledonServer server = new ProtoTeledonServer("127.0.0.1", 55556, service);
            server.Start();
            Console.WriteLine("Server started ...");
            Console.ReadLine();
        }
        
        static string GetConnectionStringByName(string name) 
        {
            string returnValue = null;
            
            ConnectionStringSettings settings =ConfigurationManager.ConnectionStrings[name];
            
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
    
    public class ProtoTeledonServer : ConcurrentServer
    {
        private IServices server;
        private ProtoWorker worker;
        public ProtoTeledonServer(string host, int port, IServices server)
            : base(host, port)
        {
            this.server = server;
            Console.WriteLine("ProtoServer...");
        }
        protected override Thread createWorker(TcpClient client)
        {
            worker = new ProtoWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }
}