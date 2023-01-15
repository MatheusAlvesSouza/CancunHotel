using CancunHotel.Api.Domain.Models;
using FluentValidation;

namespace CancunHotel.Api.Validators
{
    public class ReservationValidator : AbstractValidator<Reservation>
    {
        public ReservationValidator()
        {
            RuleFor(reservation => reservation.Id).NotEmpty();
            RuleFor(reservation => reservation.Guest).NotEmpty();
        }
    }
}
