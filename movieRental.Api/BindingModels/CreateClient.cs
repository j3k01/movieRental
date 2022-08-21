using System;
using System.ComponentModel.DataAnnotations;
//using movieRental.Common.Enums;

namespace movieRental.Api.BindingModels
{
    public class CreateClient
    {
        [Required]
        [Display(Name = "Firstname")]
        public string ClientFname { get; set; }

        [Required]
        [Display(Name = "Lastname")]
        public string ClientLname { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string ClientMail { get; set; }
    }
}