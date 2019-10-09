namespace Online.Travel.AuthService.API.Controllers
{
    using global::Online.Travel.AuthService.API.Model;
    using MediatR;

    /// <summary>
    /// GetUserInfoRequest class
    /// </summary>
    public class GetUserInfoRequest : IRequest<UserModel>
    {
        public int Id { get; set; }

        ///<Summary>
        /// GetUserInfoRequest constructor
        ///</Summary>  
        ///<param name="Id">Guid Id</param>
        public GetUserInfoRequest(int Id)
        {
            this.Id = Id;
        }
    }
}
