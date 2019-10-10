using MediatR;
using Online.Travel.Management.System.API.Model;

namespace Online.Travel.Management.System.API.Controllers
{
    /// <summary>
    /// UpdateBookingRequest class
    /// </summary>
    public class UpdateBookingRequest : IRequest<UserModel>
    {
        public UserModel UserModel { get; set; }

        ///<Summary>
        /// UpdateBookingRequest constructor
        ///</Summary>  
        ///<param name="userModel">userModel</param>
        public UpdateBookingRequest(UserModel userModel)
        {
            this.UserModel = userModel;
        }
    }
}
