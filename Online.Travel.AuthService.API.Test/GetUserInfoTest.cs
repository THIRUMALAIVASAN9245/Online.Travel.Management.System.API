using AutoMapper;
using Moq;
using Online.Travel.AuthService.API.Controllers;
using Online.Travel.AuthService.API.Entities;
using Online.Travel.AuthService.API.Entities.Repository;
using Online.Travel.AuthService.API.Model;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Online.Travel.Management.System.API.Test
{
    public class GetUserInfoTest
    {
        private GetUserInfoRequest request;

        private GetUserInfo underTest;

        private Mock<IRepository> repository;

        [Fact]
        public async Task HandleWithValidCreateRequestCallSaveAsExpectedResultAsync()
        {
            // Arrange                    
            var userModel = new UserModel { Id = 1, FirstName = "Thirumalai" };
            var config = new MapperConfiguration(m => { m.CreateMap<UserDetail, UserModel>(); });
            var mapper = new Mapper(config);
            var userDetail = new UserDetail { Id = 1, FirstName = "Thirumalai" }; ;

            repository = new Mock<IRepository>();
            repository.Setup(m => m.Get<UserDetail>(It.IsAny<int>()))
              .Returns(userDetail);

            underTest = new GetUserInfo(repository.Object, mapper);
            request = new GetUserInfoRequest(userModel.Id);

            // Act
            CancellationToken cancellationToken;
            var result = await underTest.Handle(request, cancellationToken);

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(userModel.Id, result.Id);
            Assert.Equal(userModel.FirstName, result.FirstName);
        }
    }
}