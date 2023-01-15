using CancunHotel.Api.CrossCutting;

namespace CancunHotel.Api.Services.Extensions
{
    public static class ReservationErrorExtensions
    {
        public static string StartDateGreatherThanEndDateMessage(DateOnly startDate, DateOnly endDate) 
            => $"StartDate cannot be greather than EndDate. StartDate {startDate}. EndDate {endDate}.";

        public static string StartDateEqualOrBeforeTodayMessage(DateOnly startDate)
            => $"You just can reserve dates after today. StartDate {startDate}.";

        public static string ReservationExceedsThreeDaysMessage(DateOnly startDate, DateOnly endDate)
            => $"You just can reserve max 3 days by reservation. StartDate {startDate}. EndDate {endDate}.";

        public static string NotAvailableAfterThirtyDaysMessage(DateOnly endDate)
            => $"You just can reserve until {DateOnlyExtensions.Now(addDays: 30)}. EndDate {endDate}.";

        public static string ReservedDaysMessage(IEnumerable<DateOnly> daysNotAvailable)
            => $"Some days in this reservation already reserved. Reserved days: {daysNotAvailable.ToDatesString()}";
        public static string ReservationNotFoundMessage(string id)
            => $"Not found reservation {id}.";
    }
}
