using FluentValidation;
using StudioManagement.Contract.DTO.Request;

namespace StudioManagement.Contract.Validates
{
    // Validator class for user registration request
    public sealed class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            // Validate username
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username cannot be empty")
                .MinimumLength(8).WithMessage("Username must be at least 8 characters long");

            // Validate email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Invalid email format");

            // Validate password
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
            // Confirm password must match password
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password cannot be empty")
                .Equal(x => x.Password).WithMessage("Confirm Password must match Password");
            // Validate phone number
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number cannot be empty")
                .Matches(@"^\d{10}$").WithMessage("Phone number must be 10 digits");
        }
    }
}
