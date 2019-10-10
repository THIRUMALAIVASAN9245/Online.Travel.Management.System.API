using MediatR;
using Online.Travel.Management.System.API.Model;

namespace Online.Travel.Management.System.API.Controllers
{
    /// <summary>
    /// UpdateBookingRequest class
    /// </summary>
    public class UpdateBookingRequest : IRequest<Booking>
    {
        public Booking BookingModel { get; set; }

        ///<Summary>
        /// UpdateBookingRequest constructor
        ///</Summary>  
        ///<param name="bookingModel">bookingModel</param>
        public UpdateBookingRequest(Booking bookingModel)
        {
            this.BookingModel = bookingModel;
        }
    }
}
