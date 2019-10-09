namespace Online.Travel.Management.System.API.Controllers
{
    using AutoMapper;
    using Online.Travel.Management.System.API.Entities.Repository;
    using MediatR;
    using Online.Travel.Management.System.API.Entities;
    using Online.Travel.Management.System.API.Model;
    using global::System.Threading.Tasks;
    using global::System.Threading;
    using global::System.Collections.Generic;
    using global::System.Linq;

    /// <summary>
    /// GetBookingById class
    /// </summary>
    public class GetBooking : IRequestHandler<GetBookingRequest, List<Model.BookingResponse>>
    {
        private IRepository repository;

        private IMapper mapper;

        /// <summary>
        /// GetBookingById constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public GetBooking(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Handle Method to get booking rides
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Model.BookingResponse>> Handle(GetBookingRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return await Task.FromResult<List<Model.BookingResponse>>(null);
            }

            IQueryable<Model.BookingResponse> queryGetBookings = BookingQuery();

            if (request.bookingRequest.Operation == "GetByCustomer")
            {
                var getByCustomer = queryGetBookings.Where(a => a.CustomerDetails.Id == request.bookingRequest.Id);
                return await Task.FromResult(getByCustomer.ToList());
            }
            else if (request.bookingRequest.Operation == "GetByEmployee")
            {
                var getByEmployee = queryGetBookings.Where(a => a.EmployeeDetails.Id == request.bookingRequest.Id);
                return await Task.FromResult(getByEmployee.ToList());
            }

            return await Task.FromResult(queryGetBookings.ToList());
        }

        private IQueryable<Model.BookingResponse> BookingQuery()
        {
            return repository.Query<Entities.Booking>()
                .GroupJoin(repository.Query<UserDetail>(), cus => cus.CustomerId, od => od.Id, (booking, customer) => new BookingResponse
                {
                    BookingDetails = mapper.Map<Entities.Booking, Model.Booking>(booking),
                    CustomerDetails = mapper.Map<UserDetail, Model.UserModel>(customer.FirstOrDefault()),
                }).GroupJoin(repository.Query<UserDetail>(), o => o.BookingDetails.EmployeeId, od => od.Id, (booking, employeeDetails) => this.getData(booking, employeeDetails));
        }

        private BookingResponse getData(BookingResponse booking, IEnumerable<UserDetail> employeeDetails) => new Model.BookingResponse
        {
            BookingDetails = booking.BookingDetails,
            CustomerDetails = booking.CustomerDetails,
            EmployeeDetails = mapper.Map<UserDetail, Model.UserModel>(employeeDetails.FirstOrDefault())
        };
    }
}