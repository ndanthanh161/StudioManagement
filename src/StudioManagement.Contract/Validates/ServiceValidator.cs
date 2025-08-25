using FluentValidation;
using StudioManagement.Contract.DTO.Request;

namespace StudioManagement.Contract.Validates
{
    public class ServiceValidator : AbstractValidator<ServiceRequest>
    {
        public ServiceValidator()
        {
            RuleFor(x => x.ServiceName)
                .NotEmpty().WithMessage("ServiceName cannot be empty")
                .MaximumLength(100).WithMessage("ServiceName must not exceed 100 characters");

            RuleFor(x => x.ServicePrice)
                .GreaterThanOrEqualTo(0).WithMessage("ServicePrice must be >= 0")
                .LessThanOrEqualTo(1_000_000_000m).WithMessage("ServicePrice is too large");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
        }
    }
}
