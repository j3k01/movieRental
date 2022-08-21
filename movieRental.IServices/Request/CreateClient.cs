using System;


namespace movieRental.IServices.Requests
{
    public class CreateClient
    {
        public string ClientFname { get; set; }
        public string ClientLname { get; set; }
        public string ClientMail { get; set; }
    }
}
