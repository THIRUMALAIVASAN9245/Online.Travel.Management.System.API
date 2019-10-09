namespace Online.Travel.AuthService.API.Controllers
{
    using global::Online.Travel.AuthService.API.Model;
    using MediatR;

    /// <summary>
    /// CreateBookingRequest class
    /// </summary>
    public class CreateUserRequest : IRequest<UserModel>
    {
        public UserModel UserRequest { get; set; }

        ///<Summary>
        /// CreateUserRequest constructor
        ///</Summary>  
        ///<param name="userRequest">userRequest</param>
        public CreateUserRequest(UserModel userRequest)
        {
            this.UserRequest = userRequest;
        }
    }
}