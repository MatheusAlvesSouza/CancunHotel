using CancunHotel.Api.Domain.Models;
using FluentValidation;

namespace CancunHotel.Api.Validators
{
    public class GuestValidator : AbstractValidator<Guest>
    {
        public GuestValidator()
        {
            RuleFor(guest => guest.Name).NotEmpty();
            RuleFor(guest => guest.Email).NotEmpty();
            RuleFor(guest => guest.CheckIn).NotEmpty();
        }
    }
}
