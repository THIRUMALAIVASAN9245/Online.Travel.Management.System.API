namespace Online.Travel.Management.System.API.Controllers
{
    using Online.Travel.Management.System.API.Model;
    using MediatR;

    /// <summary>
    /// CreateBookingRequest class
    /// </summary>
    public class CreateBookingRequest : IRequest<Booking>
    {
        public Booking BookingRequest { get; set; }

        ///<Summary>
        /// CreateBookingRequest constructor
        ///</Summary>  
        ///<param name="bookingRequest">bookingRequest</param>
        public CreateBookingRequest(Booking bookingRequest)
        {
            this.BookingRequest = bookingRequest;
        }
    }
}
