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
    /// GetUserInfo class
    /// </summary>
    public class GetUserInfo : IRequestHandler<GetUserInfoRequest, UserModel>
    {
        private IRepository repository;

        private IMapper mapper;

        /// <summary>
        /// LoginUser constructor
        /// </summary>
        /// <param name="repository">IRepository</param>
        /// <param name="mapper">IMapper</param>
        public GetUserInfo(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Handle method to get user info
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UserModel> Handle(GetUserInfoRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return await Task.FromResult<UserModel>(null);
            }

            var userDetail = repository.Query<UserDetail>().FirstOrDefault(user => user.Id == request.Id);

            if (userDetail != null)
            {
                return await Task.FromResult<UserModel>(null);
            }
            var createUserModel = mapper.Map<UserDetail, UserModel>(userDetail);

            return await Task.FromResult(createUserModel);
        }
    }
}