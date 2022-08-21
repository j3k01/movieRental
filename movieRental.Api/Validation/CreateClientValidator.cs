using FluentValidation;
using movieRental.Api.BindingModels;

namespace movieRental.Api.Validation
{
    public class CreateClientValidator : AbstractValidator<CreateClient>
    {
        public CreateClientValidator()
        {
            RuleFor(x => x.ClientFname).NotNull();
            RuleFor(x => x.ClientLname).NotNull();
            RuleFor(x => x.ClientMail).NotNull();
        }
    }
}