using Online.Travel.AuthService.API.Model;

namespace Online.Travel.AuthService.API.Infrastructure
{
    public interface ITokenGenerator
    {
        string GetJwtTokenLoggedinUser(UserModel userModel);
    }
}