using System;
using movieRental.Common.Enums;

namespace movieRental.Api.ViewModels
{
    public class ClientViewModel
    {
        public int ClientId { get; set; }
        public string ClientFname { get; set; }
        public string ClientLname { get; set; }
        public string ClientMail { get; set; }

    }
}