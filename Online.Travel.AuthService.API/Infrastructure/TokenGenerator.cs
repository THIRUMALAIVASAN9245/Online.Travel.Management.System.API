using Online.Travel.AuthService.API.Model;

namespace Online.Travel.AuthService.API.Infrastructure
{
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class TokenGenerator : ITokenGenerator
    {
        public string GetJwtTokenLoggedinUser(UserModel userModel)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Taxi_Booking_Security_Key"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "http://localhost:51380",
                audience: "http://localhost:52987",
                claims: Claims(userModel.Id),
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: creds
            );
            var response = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                userDetails = userModel
            };

            return JsonConvert.SerializeObject(response);
        }

        private static Claim[] Claims(int userId)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            return claims;
        }
    }
}