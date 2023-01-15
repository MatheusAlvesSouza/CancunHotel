using CancunHotel.Api.Controllers;
using CancunHotel.Api.CrossCutting;
using CancunHotel.Api.Data;
using CancunHotel.Api.Domain.Models;
using CancunHotel.Api.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using CancunHotel.Api.Services.Extensions;

namespace CancunHotel.Test
{
    public class ReservationControllerTests
    {

        private readonly ReservationController _sut;

        public ReservationControllerTests()
        {
            var reservationRepository = new ReservationRepository();
            var reservationService = new ReservationService(reservationRepository);
            var logging = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();

            _sut = new ReservationController(reservationService, logging.CreateLogger<ReservationController>());
        }

        [Fact]
        public void Should_Post_Return_OK()
        {
            // Arrange
            var now = DateOnlyExtensions.Now();
            var guest = new Guest("Matheus", "matheus@hotmail.com", false);
            var newReservation = new NewReservation(guest, now.AddDays(1), now.AddDays(3));

            // Act
            var response = _sut.Post(newReservation);

            // Assert
            var obj = (ObjectResult) response;
            var reservation = (Reservation?) obj.Value;
            var statusCodeResult = (IStatusCodeActionResult) response;

            Assert.NotNull(reservation);
            Assert.Equal(StatusCodes.Status200OK, statusCodeResult.StatusCode);
            Assert.Equal(newReservation.EndDate, reservation.EndDate);
            Assert.Equal(newReservation.StartDate, reservation.StartDate);
            Assert.Equal(newReservation.Guest.Name, reservation.Guest.Name);
            Assert.Equal(newReservation.Guest.Email, reservation.Guest.Email);
            Assert.Equal(newReservation.Guest.CheckIn, reservation.Guest.CheckIn);
        }

        [Theory]
        [MemberData(nameof(GetFailRulesCases))]
        public void Should_Post_Return_BadRequest_When_Rules_Not_Pass(DateOnly startDate, DateOnly endDate, string expectedErrorMessage)
        {
            // Arrange
            var guest = new Guest("Matheus", "matheus@hotmail.com", false);
            var newReservation = new NewReservation(guest, startDate, endDate);

            // Act
            var response = _sut.Post(newReservation);

            // Assert
            var obj = (ObjectResult)response;
            var responseBody = (Response<Reservation>?)obj.Value;
            var statusCodeResult = (IStatusCodeActionResult)response;


            Assert.NotNull(responseBody);
            Assert.False(responseBody.IsSuccess);
            Assert.Equal(StatusCodes.Status400BadRequest, statusCodeResult.StatusCode);
            Assert.Equal(expectedErrorMessage, responseBody.Error);
        }

        public static IEnumerable<object[]> GetFailRulesCases()
        {
            var now = DateOnlyExtensions.Now();
            
            yield return new object[] 
            {
                now.AddDays(3),
                now.AddDays(2), 
                ReservationErrorExtensions.StartDateGreatherThanEndDateMessage(now.AddDays(3), now.AddDays(2)) 
            };

            yield return new object[]
            {
                now.AddDays(0),
                now.AddDays(2),
                ReservationErrorExtensions.StartDateEqualOrBeforeTodayMessage(now.AddDays(0))
            };

            yield return new object[]
            {
                now.AddDays(1),
                now.AddDays(5),
                ReservationErrorExtensions.ReservationExceedsThreeDaysMessage(now.AddDays(1), now.AddDays(5))
            };

            yield return new object[]
            {
                now.AddDays(29),
                now.AddDays(31),
                ReservationErrorExtensions.NotAvailableAfterThirtyDaysMessage(now.AddDays(31))
            };
        }
    }
}