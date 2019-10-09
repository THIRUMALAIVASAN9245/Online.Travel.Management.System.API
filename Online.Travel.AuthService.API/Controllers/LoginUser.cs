namespace Online.Travel.AuthService.API.Controllers
{
    using AutoMapper;
    using MediatR;
    using global::System.Threading.Tasks;
    using global::System.Threading;
    using global::Online.Travel.AuthService.API.Model;
    using global::Online.Travel.AuthService.API.Entities.Repository;
    using global::Online.Travel.AuthService.API.Entities;
    using global::System.Linq;

    /// <summary>
    /// LoginUser class
    /// </summary>
    public class LoginUser : IRequestHandler<LoginUserRequest, UserModel>
    {
        private IRepository repository;

        private IMapper mapper;

        /// <summary>
        /// LoginUser constructor
        /// </summary>
        /// <param name="repository">IRepository</param>
        /// <param name="mapper">IMapper</param>
        public LoginUser(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Handle method to verify user info
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UserModel> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return await Task.FromResult<UserModel>(null);
            }

            var userDetail = repository.Query<UserDetail>()
                .FirstOrDefault(user => user.Email == request.UserRequest.Email &&
                user.PhoneNumber == request.UserRequest.PhoneNumber);

            if (userDetail == null)
            {
                return await Task.FromResult<UserModel>(null);
            }
            var createUserModel = mapper.Map<UserDetail, UserModel>(userDetail);

            return await Task.FromResult(createUserModel);
        }
    }
}