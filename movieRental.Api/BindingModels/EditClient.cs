using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using movieRental.Common.Enums;

namespace movieRental.Api.BindingModels
{
    public class EditClient
    {
        //        [Required]
        [Display(Name = "Firstname")]
        public string ClientFname { get; set; }

        [Display(Name = "Lastname")]
        public string ClientLname { get; set; }

        //        [Required]
        //        [EmailAddress]
                [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string ClientMail { get; set; }
    }

    public class EditClientValidator : AbstractValidator<EditClient>
    {
        public EditClientValidator()
        {
            RuleFor(x => x.ClientFname).NotNull();
            RuleFor(x => x.ClientLname).NotNull();
            RuleFor(x => x.ClientMail).EmailAddress();
        }
    }

}