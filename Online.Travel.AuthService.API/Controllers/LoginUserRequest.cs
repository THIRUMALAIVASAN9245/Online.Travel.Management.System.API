namespace Online.Travel.AuthService.API.Controllers
{
    using MediatR;
    using global::Online.Travel.AuthService.API.Model;

    /// <summary>
    /// LoginUserRequest class
    /// </summary>
    public class LoginUserRequest : IRequest<UserModel>
    {
        public UserModel UserRequest { get; set; }

        ///<Summary>
        /// LoginUserRequest constructor
        ///</Summary>  
        ///<param name="userRequest">userRequest</param>
        public LoginUserRequest(UserModel userRequest)
        {
            this.UserRequest = userRequest;
        }
    }
}