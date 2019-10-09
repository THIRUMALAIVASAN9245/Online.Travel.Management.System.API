namespace Online.Travel.Management.System.API.Controllers
{
    using Online.Travel.Management.System.API.Model;
    using MediatR;
    using global::System.Collections.Generic;

    public class GetBookingRequest : IRequest<List<BookingResponse>>
    {
        public BookingRequest bookingRequest { get; set; }

        ///<Summary>
        /// GetBookingByIdRequest constructor
        ///</Summary>  
        ///<param name="bookingRequest">bookingRequest</param>
        public GetBookingRequest(BookingRequest bookingRequest)
        {
            this.bookingRequest = bookingRequest;
        }
    }
}
