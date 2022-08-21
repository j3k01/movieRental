using FluentValidation;
using movieRental.Api.BindingModels;

namespace movieRental.Api.Validation
{
    public class DeleteClientValidator : AbstractValidator<DeleteClient>
    {
        public DeleteClientValidator()
        {
            RuleFor(x => x.ClientId).NotNull();
        }
    }
}
