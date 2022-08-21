using System;
using System.ComponentModel.DataAnnotations;

namespace movieRental.Api.BindingModels
{
    public class DeleteClient
    {
        public int ClientId { get; set; }
        public int ClientFname { get; set; }
    }
}
