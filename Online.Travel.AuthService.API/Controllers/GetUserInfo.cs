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
    using System.Collections.Generic;

    /// <summary>
    /// GetUserInfo class
    /// </summary>
    public class GetUserInfo : IRequestHandler<GetUserInfoRequest, List<UserModel>>
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
        public async Task<List<UserModel>> Handle(GetUserInfoRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return await Task.FromResult<List<UserModel>>(null);
            }

            if (request.UserInfoRequest.Operation == "GetByCustomer")
            {
                var getByCustomer = repository.Query<Entities.UserDetail>().Where(a=>a.RoleId == 1);
                var getByCustomerResult = mapper.Map<List<UserDetail>, List<UserModel>>(getByCustomer.ToList());
                return await Task.FromResult(getByCustomerResult);
            }
            else if (request.UserInfoRequest.Operation == "GetByEmployee")
            {
                var getByCustomer = repository.Query<Entities.UserDetail>().Where(a => a.RoleId == 2);
                var getByCustomerResult = mapper.Map<List<UserDetail>, List<UserModel>>(getByCustomer.ToList());
                return await Task.FromResult(getByCustomerResult);
            }

            var getByIdCustomer = repository.Query<Entities.UserDetail>().Where(a => a.Id == request.UserInfoRequest.Id);
            var result = mapper.Map<List<UserDetail>, List<UserModel>>(getByIdCustomer.ToList());
            return await Task.FromResult(result);
        }
    }
}