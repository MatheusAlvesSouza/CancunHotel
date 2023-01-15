using System.Text.Json.Serialization;
using CancunHotel.Api.CrossCutting;

namespace CancunHotel.Api.Domain.Models
{
    public class NewReservation : Reservation
    {
        [JsonConstructor]
        public NewReservation(Guest guest, DateOnly startDate, DateOnly endDate) 
            : base(Guid.NewGuid().ToString(), guest, startDate, endDate)
        {
        }
    }

    public class Reservation
    {
        public string Id { get; }
        public Guest Guest { get; }
        public DateOnly StartDate { get; }
        public DateOnly EndDate { get; }
        public int RoomId = 1;

        [JsonConstructor]
        public Reservation(string id, Guest guest, DateOnly startDate, DateOnly endDate)
        {
            Id = id;
            Guest = guest;
            StartDate = startDate;
            EndDate = endDate;
        }

        public List<DateOnly> Dates()
        {
            var reservationDates = new List<DateOnly>();

            for (int i = 0; i <= EndDate.DayNumberBetween(StartDate); i++)
                reservationDates.Add(StartDate.AddDays(i));

            return reservationDates;
        }
    }
}
