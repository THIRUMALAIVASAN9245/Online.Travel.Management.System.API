namespace Online.Travel.AuthService.API.Controllers
{
    using global::Online.Travel.AuthService.API.Model;
    using MediatR;
    using System.Collections.Generic;

    /// <summary>
    /// GetUserInfoRequest class
    /// </summary>
    public class GetUserInfoRequest : IRequest<List<UserModel>>
    {
        public UserInfoRequest UserInfoRequest { get; set; }

        ///<Summary>
        /// GetUserInfoRequest constructor
        ///</Summary>  
        ///<param name="userInfoRequest">userInfoRequest</param>
        public GetUserInfoRequest(UserInfoRequest userInfoRequest)
        {
            this.UserInfoRequest = userInfoRequest;
        }
    }
}
