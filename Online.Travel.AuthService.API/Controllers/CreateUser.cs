namespace Online.Travel.AuthService.API.Controllers
{
    using AutoMapper;
    using MediatR;
    using global::System.Threading.Tasks;
    using global::System.Threading;
    using global::Online.Travel.AuthService.API.Model;
    using global::Online.Travel.AuthService.API.Entities.Repository;

    /// <summary>
    /// CreateUser class
    /// </summary>
    public class CreateUser : IRequestHandler<CreateUserRequest, UserModel>
    {
        private IRepository repository;

        private IMapper mapper;

        /// <summary>
        /// CreateUser constructor
        /// </summary>
        /// <param name="repository">IRepository</param>
        /// <param name="mapper">IMapper</param>
        public CreateUser(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Handle method to create new user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UserModel> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var createUser = mapper.Map<UserModel, AuthService.API.Entities.UserDetail>(request.UserRequest);

            repository.Save(createUser);

            var createUserModel = mapper.Map<AuthService.API.Entities.UserDetail, UserModel>(createUser);

            return await Task.FromResult(createUserModel);
        }
    }
}