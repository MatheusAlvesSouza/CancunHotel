using CancunHotel.Api.CrossCutting;
using CancunHotel.Api.Domain.Interfaces;
using CancunHotel.Api.Domain.Models;

namespace CancunHotel.Api.Data
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly Dictionary<string, Reservation> reservationIds;
        private readonly Dictionary<DateOnly, string> reservationDates;
        private readonly object reservationLock;

        public ReservationRepository()
        {
            reservationLock = new object();
            reservationDates = new Dictionary<DateOnly, string>();
            reservationIds = new Dictionary<string, Reservation>();
        }

        public void Add(Reservation reservation)
        {
            lock(reservationLock)
            {
                reservationIds.Add(reservation.Id, reservation);

                reservation.Dates().ForEach(date =>
                {
                    reservationDates.Add(date, reservation.Id);
                });
            }
        }

        public void Update(Reservation reservation)
        {
            lock (reservationLock)
            {
                Delete(reservation.Id);

                reservationIds[reservation.Id] = reservation;

                reservation.Dates().ForEach(date =>
                {
                    reservationDates[date] = reservation.Id;
                });
            }
        }

        public void Delete(string id)
        {
            lock (reservationLock)
            {
                foreach (var datePair in reservationDates.Where(date => date.Value == id))
                    reservationDates.Remove(datePair.Key);

                reservationIds.Remove(id);
            }
        }

        public bool Exists(string id)
        {
            return reservationIds.ContainsKey(id);
        }

        public Reservation? Get(string id)
        {
            reservationIds.TryGetValue(id, out var reservation);

            return reservation;
        }

        public List<DateOnly> FindAvailableReservationDates(string reservationToIgnore)
        {
            lock(reservationLock)
            {
                var dates = DateOnlyExtensions.Now().ListTo(DateOnlyExtensions.Now(addDays: 30));

                var availableDates = dates.Except(reservationDates
                    .Where(reservationDate => reservationDate.Value != reservationToIgnore)
                    .Select(reservationDate => reservationDate.Key));

                return availableDates.ToList();
            }
        }
    }
}
