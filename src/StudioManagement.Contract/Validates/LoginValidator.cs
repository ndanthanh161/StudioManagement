using FluentValidation;
using Microsoft.Extensions.Logging;
using StudioManagement.Contract.DTO.Request;

namespace StudioManagement.Contract.Validates
{
    public sealed class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator(ILogger<LoginValidator> logger)
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username cannot be empty");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty");

        }
    }
}
