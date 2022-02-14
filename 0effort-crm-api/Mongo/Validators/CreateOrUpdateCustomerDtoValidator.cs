using _0effort_crm_api.Contracts.DTO;
using FluentValidation;

namespace _0effort_crm_api.Mongo.Validators
{
    public class CreateOrUpdateCustomerDtoValidator : AbstractValidator<CreateOrUpdateCustomerDto>
    {
        public CreateOrUpdateCustomerDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Invalid or empty email.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
            RuleFor(x => x.Postcode).NotEmpty().WithMessage("Postcode is required.");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is required.");
        }
    }
}
