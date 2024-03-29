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
    public class CreateUserTest
    {
        private CreateUserRequest request;

        private CreateUser underTest;

        private Mock<IRepository> repository;

        [Fact]
        public async Task HandleWithValidCreateRequestCallSaveAsExpectedResultAsync()
        {
            // Arrange                    
            var userModel = new UserModel { Id = 299536, FirstName = "Thirumalai" };

            var config = new MapperConfiguration(m => { m.CreateMap<UserDetail, UserModel>(); m.CreateMap<UserModel, UserDetail>(); });
            var mapper = new Mapper(config);
            var UserList = MockUserListResponse().ToList().AsQueryable();

            repository = new Mock<IRepository>();
            repository.Setup(m => m.Query<UserDetail>())
              .Returns(UserList);

            underTest = new CreateUser(repository.Object, mapper);
            request = new CreateUserRequest(userModel);

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
                    Id = 1, FirstName = "Vasan"
                },
                new UserDetail
                {
                    Id = 2, FirstName = "Rathish"
                }
            };

            return userList;
        }
    }
}