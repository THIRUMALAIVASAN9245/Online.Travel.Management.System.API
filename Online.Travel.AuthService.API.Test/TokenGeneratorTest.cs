namespace AuthService.API.Test
{
    using Online.Travel.AuthService.API.Infrastructure;
    using Online.Travel.AuthService.API.Model;
    using Xunit;

    public class TokenGeneratorTest
    {
        private readonly ITokenGenerator tokenGenerator;        

        public TokenGeneratorTest()
        {
            tokenGenerator = new TokenGenerator();
        }

        [Fact]
        public void GetJwtTokenLoggedinUserWithUserIdReturnsExpectedResult()
        {
            //Arrange            
            var userDetailModel = new UserModel
            {
                Id = 1,
                FirstName = "Thiru"
            };

            //Act
            var actual = tokenGenerator.GetJwtTokenLoggedinUser(userDetailModel);

            //Assert                        
            Assert.NotEmpty(actual);
        }
    }
}
