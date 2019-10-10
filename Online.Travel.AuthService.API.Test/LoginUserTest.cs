using AutoMapper;
using Moq;
using Online.Travel.AuthService.API.Controllers;
using Online.Travel.AuthService.API.Entities;
using Online.Travel.AuthService.API.Entities.Repository;
using Online.Travel.AuthService.API.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Online.Travel.Management.System.API.Test
{
    public class LoginUserTest
    {
        private LoginUserRequest request;

        private LoginUser underTest;

        private Mock<IRepository> repository;

        [Fact]
        public async Task HandleWithValidCreateRequestCallSaveAsExpectedResultAsync()
        {
            // Arrange                    
            var userModel = new UserModel { Id = 1, FirstName = "Thirumalai", PhoneNumber = 123456, Email = "Thirumalai@test.com", Password = "Test@123" };

            var config = new MapperConfiguration(m => { m.CreateMap<UserDetail, UserModel>(); });
            var mapper = new Mapper(config);
            var bookingList = MockUserListResponse().ToList().AsQueryable();

            repository = new Mock<IRepository>();
            repository.Setup(m => m.Query<UserDetail>())
              .Returns(bookingList);

            underTest = new LoginUser(repository.Object, mapper);
            request = new LoginUserRequest(userModel);

            // Act
            CancellationToken cancellationToken;
            var result = await underTest.Handle(request, cancellationToken);

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(userModel.Id, result.Id);
            Assert.Equal(userModel.FirstName, result.FirstName);
        }

        private static List<UserDetail> MockUserListResponse()
        {
            var userList = new List<UserDetail>
            {
                new UserDetail
                {
                    Id = 1, FirstName = "Thirumalai", PhoneNumber = 123456, Email = "Thirumalai@test.com", Password = "Test@123"
                },
                new UserDetail
                {
                    Id = 1, FirstName = "Rathish", PhoneNumber = 123456, Email = "Rathish@test.com", Password = "Test@123"
                }
            };

            return userList;
        }
    }
}