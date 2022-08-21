using System;

namespace movieRental.Domain.Client
{
    public class Client
    {
        public int ClientId { get; set; }
        public string ClientFname { get; private set; }
        public string ClientLname { get; private set; }
        public string ClientMail { get; private set; }

        public Client(int clientid, string clientfname, string clientlname, string clientmail)
        {
            ClientId = clientid;
            ClientFname = clientfname;
            ClientLname = clientlname;
            ClientMail = clientmail;
        }
        public Client(string clientfname, string clientlname, string clientmail)
        {
            ClientFname = clientfname;
            ClientLname = clientlname;
            ClientMail = clientmail;
        }

        public void EditClient(string clientfname, string clientlname, string clientmail)
        {
            ClientFname = clientfname;
            ClientLname = clientlname;
            ClientMail = clientmail;
        }

        public void RemoveClient(int clientid, string clientfname, string clientlname,string clientmail)
        {
            ClientId = clientid;
            ClientFname = clientfname;
            ClientLname = clientlname;
            ClientMail = clientmail;
        }


    }
}
