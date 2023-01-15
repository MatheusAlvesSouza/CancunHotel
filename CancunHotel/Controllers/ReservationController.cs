using System.ComponentModel.DataAnnotations;
using CancunHotel.Api.CrossCutting;
using CancunHotel.Api.Domain.Interfaces;
using CancunHotel.Api.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CancunHotel.Api.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(IReservationService reservationService, ILogger<ReservationController> logger)
        {
            _logger = logger;
            _reservationService = reservationService;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<Reservation>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody, Required] NewReservation reservation)
        {
            try
            {
                var response = _reservationService.Add(reservation);

                if (!response.IsSuccess)
                    return BadRequest(response);

                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");

                return StatusCode(500);
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<Reservation>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody, Required] Reservation reservation)
        {
            try
            {
                var response = _reservationService.Update(reservation);

                if (!response.IsSuccess)
                    return BadRequest(response);

                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");

                return StatusCode(500);
            }
        }

        [HttpGet, Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<Reservation>), StatusCodes.Status400BadRequest)]
        public IActionResult Get([FromRoute, Required] string id)
        {
            try
            {
                var response = _reservationService.Get(id);

                if (!response.IsSuccess)
                    return BadRequest(response);

                if (response.Result is null)
                    return NotFound();

                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");

                return StatusCode(500);
            }
        }

        [HttpDelete, Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute, Required] string id)
        {
            try
            {
                var response = _reservationService.Delete(id);

                if (!response.IsSuccess)
                    return BadRequest(response);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");

                return StatusCode(500);
            }
        }

        [HttpGet("GetAvailableDates")]
        [ProducesResponseType(typeof(List<DateOnly>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<List<DateOnly>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAvailableDates()
        {
            try
            {
                var response = _reservationService.FindAvailableReservationDates();

                if (!response.IsSuccess)
                    return BadRequest(response);

                return Ok(response.Result);
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");

                return StatusCode(500);
            }
        }
    }
}
