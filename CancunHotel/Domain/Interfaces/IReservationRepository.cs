using CancunHotel.Api.Domain.Models;

namespace CancunHotel.Api.Domain.Interfaces
{
    public interface IReservationRepository
    {
        public void Add(Reservation reservation);
        public void Update(Reservation reservation);
        public void Delete(string id);
        public bool Exists(string id);
        public Reservation? Get(string id);
        public List<DateOnly> FindAvailableReservationDates(string reservationToIgnore);
    }
}
