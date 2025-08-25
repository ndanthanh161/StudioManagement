// StudioManagement.Contract/Validates/RoomRequestValidator.cs
using FluentValidation;
using StudioManagement.Contract.DTO.Request;

namespace StudioManagement.Contract.Validates
{
    public sealed class RoomRequestValidator : AbstractValidator<RoomRequest>
    {
        public RoomRequestValidator()
        {
            RuleFor(x => x.RoomName)
                .NotEmpty().WithMessage("RoomName cannot be empty")
                .MaximumLength(100).WithMessage("RoomName can be at most 100 characters long");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(1).WithMessage("Quantity must be >= 1")
                .LessThanOrEqualTo(1000).WithMessage("Quantity is too large (<= 1000)");

            RuleFor(x => x.RoomPrice)
                .GreaterThanOrEqualTo(0).WithMessage("RoomPrice must be >= 0")
                .LessThanOrEqualTo(1_000_000_000m).WithMessage("RoomPrice is too large");
        }
    }
}
