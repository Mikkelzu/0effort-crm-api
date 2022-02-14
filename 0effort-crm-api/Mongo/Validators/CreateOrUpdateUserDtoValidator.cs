

using _0effort_crm_api.Contracts.DTO;
using FluentValidation;

namespace _0effort_crm_api.Mongo.Validators
{
    public class CreateOrUpdateUserDtoValidator : AbstractValidator<CreateOrUpdateUserDto>
    {
        public CreateOrUpdateUserDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username can not be empty.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password can not be empty.");
        }
    }
}
