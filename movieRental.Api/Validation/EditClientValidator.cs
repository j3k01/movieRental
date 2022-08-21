using FluentValidation;
using movieRental.Api.BindingModels;

namespace movieRental.Api.Validation
{
    public class EditClientValidator : AbstractValidator<EditClient>
    {
        public EditClientValidator()
        {
            RuleFor(x => x.ClientFname).NotNull();
            RuleFor(x => x.ClientLname).NotNull();
            RuleFor(x => x.ClientMail).NotNull();
        }
    }
}
