using CancunHotel.Api.CrossCutting;
using CancunHotel.Api.Domain.Interfaces;
using CancunHotel.Api.Domain.Models;
using CancunHotel.Api.Services.Extensions;

namespace CancunHotel.Api.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public Response<Reservation> Add(Reservation reservation)
        {
            var validationResponse = ValidateReservationRules(in reservation);

            if (!validationResponse.IsSuccess)
                return validationResponse;

            _reservationRepository.Add(reservation);

            return Response<Reservation>.Success(reservation);
        }

        public Response<Reservation> Update(Reservation reservation)
        {
            var exists = _reservationRepository.Exists(reservation.Id);

            if (!exists)
                return Response<Reservation>.Failure(ReservationErrorExtensions.ReservationNotFoundMessage(reservation.Id));

            var validationResponse = ValidateReservationRules(in reservation);

            if (!validationResponse.IsSuccess)
                return validationResponse;

            _reservationRepository.Update(reservation);

            return Response<Reservation>.Success(reservation);
        }

        public Response<string> Delete(string id)
        {
            var exists = _reservationRepository.Exists(id);

            if (!exists)
            {
                return Response<string>.Failure(ReservationErrorExtensions.ReservationNotFoundMessage(id));
            }

            _reservationRepository.Delete(id);

            return Response<string>.Success(id);
        }

        public Response<List<DateOnly>> FindAvailableReservationDates()
        {
            return Response<List<DateOnly>>.Success(_reservationRepository.FindAvailableReservationDates(string.Empty));
        }

        public Response<Reservation?> Get(string id)
        {
            return Response<Reservation?>.Success(_reservationRepository.Get(id));
        }

        private Response<Reservation> ValidateReservationRules(in Reservation reservation)
        {
            if (reservation.StartDate > reservation.EndDate)
                return Response<Reservation>.Failure(ReservationErrorExtensions.StartDateGreatherThanEndDateMessage(reservation.StartDate, reservation.EndDate));

            if (!reservation.StartDate.IsAfterToday())
                return Response<Reservation>.Failure(ReservationErrorExtensions.StartDateEqualOrBeforeTodayMessage(reservation.StartDate));

            if (reservation.EndDate.DayNumberBetween(reservation.StartDate) > 3)
                return Response<Reservation>.Failure(ReservationErrorExtensions.ReservationExceedsThreeDaysMessage(reservation.StartDate, reservation.EndDate));

            if (reservation.EndDate.DayNumberBetween(DateOnlyExtensions.Now(addDays: 30)) > 0)
                return Response<Reservation>.Failure(ReservationErrorExtensions.NotAvailableAfterThirtyDaysMessage(reservation.EndDate));

            var availableDates = _reservationRepository.FindAvailableReservationDates(reservation.Id);
            var reservationDates = reservation.Dates();

            var daysNotAvailable = reservationDates.Where(reservationDate => !availableDates.Any(availableDate => reservationDate == availableDate));

            if (daysNotAvailable.Any())
                return Response<Reservation>.Failure(ReservationErrorExtensions.ReservedDaysMessage(daysNotAvailable));

            return Response<Reservation>.Success(reservation);
        }
    }
}
