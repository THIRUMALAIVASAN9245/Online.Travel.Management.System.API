namespace Online.Travel.Management.System.API.Controllers
{
    using Online.Travel.Management.System.API.Model;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using global::System.Threading.Tasks;
    using global::System;

    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class BookingController : Controller
    {
        private readonly IMediator mediatR;

        /// <summary>
        /// BookingController constructor
        /// </summary>
        /// <param name="mediatR"></param>
        public BookingController(IMediator mediatR)
        {
            this.mediatR = mediatR;
        }

        /// <summary>
        /// Create new ride
        /// </summary>
        /// <param name="bookingRequest">Create new ride</param>
        /// <returns>Saved booking data object</returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Booking))]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Post([FromBody]Booking bookingRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await mediatR.Send(new CreateBookingRequest(bookingRequest));

                if (response == null)
                {
                    return NotFound("Error Occurred While Creating a Ride");
                }

                return Created("New Ride created", response);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred While Creating a Ride" + ex.Message);
            }
        }

        /// <summary>
        /// Get Rides
        /// </summary>
        /// <returns>list of Ride</returns>
        [HttpPost]
        [Route("GetBooking")]
        [ProducesResponseType(200, Type = typeof(BookingResponse[]))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBooking(BookingRequest bookingRequest)
        {
            try
            {
                var response = await mediatR.Send(new GetBookingRequest(bookingRequest));

                if (response == null)
                {
                    return NotFound("No ride found");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("No ride found Error Occurred" + ex.Message);
            }
        }
    }
}