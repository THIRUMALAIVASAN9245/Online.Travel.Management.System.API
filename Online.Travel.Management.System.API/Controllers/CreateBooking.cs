namespace Online.Travel.Management.System.API.Controllers
{
    using AutoMapper;
    using Online.Travel.Management.System.API.Model;
    using MediatR;
    using Online.Travel.Management.System.API.Entities.Repository;
    using global::System.Threading.Tasks;
    using global::System.Threading;

    /// <summary>
    /// CreateBooking class
    /// </summary>
    public class CreateBooking : IRequestHandler<CreateBookingRequest, Booking>
    {
        private IRepository repository;

        private IMapper mapper;

        /// <summary>
        /// CreateBooking constructor
        /// </summary>
        /// <param name="repository">IRepository</param>
        /// <param name="mapper">IMapper</param>
        public CreateBooking(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Handle method to create new ride
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Booking> Handle(CreateBookingRequest request, CancellationToken cancellationToken)
        {
            var createBooking = mapper.Map<Booking, Entities.Booking>(request.BookingRequest);

            repository.Save(createBooking);

            var createBookingModel = mapper.Map<Entities.Booking, Booking>(createBooking);

            return await Task.FromResult(createBookingModel);
        }
    }
}
