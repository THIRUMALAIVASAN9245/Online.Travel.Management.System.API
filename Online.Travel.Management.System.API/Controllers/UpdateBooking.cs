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
    public class UpdateBooking : IRequestHandler<UpdateBookingRequest, UserModel>
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
        public Task<UserModel> Handle(UpdateBookingRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Task.FromResult<UserModel>(null);
            }

            var userDetail = repository.Get<Entities.Booking>(request.UserModel.Id);

            if (userDetail == null)
            {
                return Task.FromResult<UserModel>(null);
            }

            mapper.Map(request.UserModel, userDetail);

            repository.Update(userDetail);

            return Task.FromResult(request.UserModel);
        }
    }
}
