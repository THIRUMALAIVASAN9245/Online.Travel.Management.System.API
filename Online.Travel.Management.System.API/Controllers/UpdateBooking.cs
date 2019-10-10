using AutoMapper;
using MediatR;
using Online.Travel.Management.System.API.Entities.Repository;
using Online.Travel.Management.System.API.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Online.Travel.Management.System.API.Controllers
{
    /// <summary>
    /// UpdateBooking class
    /// </summary>
    public class UpdateBooking : IRequestHandler<UpdateBookingRequest, Booking>
    {
        private IRepository repository;

        private IMapper mapper;

        /// <summary>
        /// UpdateBooking constructor
        /// </summary>
        /// <param name="repository">IRepository</param>
        /// <param name="mapper">IMapper</param>
        public UpdateBooking(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        ///  Handle method update user info
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Booking> Handle(UpdateBookingRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Task.FromResult<Booking>(null);
            }

            var bookingDetail = repository.Get<Entities.Booking>(request.BookingModel.Id);

            if (bookingDetail == null)
            {
                return Task.FromResult<Booking>(null);
            }

            mapper.Map(request.BookingModel, bookingDetail);

            repository.Update(bookingDetail);

            return Task.FromResult(request.BookingModel);
        }
    }
}
