using CancunHotel.Api.CrossCutting;
using CancunHotel.Api.Domain.Models;

namespace CancunHotel.Api.Domain.Interfaces
{
    public interface IReservationService
    {
        public Response<Reservation> Add(Reservation reservation);
        public Response<Reservation> Update(Reservation reservation);
        public Response<string> Delete(string id);
        public Response<Reservation?> Get(string id);
        public Response<List<DateOnly>> FindAvailableReservationDates();
    }
}
