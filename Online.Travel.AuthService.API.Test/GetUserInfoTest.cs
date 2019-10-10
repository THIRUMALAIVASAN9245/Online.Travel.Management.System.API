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
    public class GetUserInfoTest
    {
        private GetUserInfoRequest request;

        private GetUserInfo underTest;

        private Mock<IRepository> repository;

        [Fact]
        public async Task HandleWithValidCreateRequestCallSaveAsExpectedResultAsync()
        {
            // Arrange                    
            var userModel = new UserInfoRequest { Id = 1 };
            var config = new MapperConfiguration(m => { m.CreateMap<UserDetail, UserModel>(); m.CreateMap<UserModel, UserDetail>(); });
            var mapper = new Mapper(config);
            var userDetail = MockUserListResponse().ToList().AsQueryable();
            repository = new Mock<IRepository>();
            repository.Setup(m => m.Query<UserDetail>()).Returns(userDetail);

            underTest = new GetUserInfo(repository.Object, mapper);
            request = new GetUserInfoRequest(userModel);

            // Act
            CancellationToken cancellationToken;
            var result = await underTest.Handle(request, cancellationToken);

            // Assert  
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task HandleWithGetByCustomerValidCreateRequestCallSaveAsExpectedResultAsync()
        {
            // Arrange                    
            var userModel = new UserInfoRequest { Id = 1, Operation = "GetByCustomer" };
            var config = new MapperConfiguration(m => { m.CreateMap<UserDetail, UserModel>(); m.CreateMap<UserModel, UserDetail>(); });
            var mapper = new Mapper(config);
            var userDetail = MockUserListResponse().ToList().AsQueryable();
            repository = new Mock<IRepository>();
            repository.Setup(m => m.Query<UserDetail>()).Returns(userDetail);

            underTest = new GetUserInfo(repository.Object, mapper);
            request = new GetUserInfoRequest(userModel);

            // Act
            CancellationToken cancellationToken;
            var result = await underTest.Handle(request, cancellationToken);

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task HandleWithGetByEmployeeValidCreateRequestCallSaveAsExpectedResultAsync()
        {
            // Arrange                    
            var userModel = new UserInfoRequest { Id = 1, Operation = "GetByEmployee" };
            var config = new MapperConfiguration(m => { m.CreateMap<UserDetail, UserModel>(); m.CreateMap<UserModel, UserDetail>(); });
            var mapper = new Mapper(config);
            var userDetail = MockUserListResponse().ToList().AsQueryable();
            repository = new Mock<IRepository>();
            repository.Setup(m => m.Query<UserDetail>()).Returns(userDetail);

            underTest = new GetUserInfo(repository.Object, mapper);
            request = new GetUserInfoRequest(userModel);

            // Act
            CancellationToken cancellationToken;
            var result = await underTest.Handle(request, cancellationToken);

            // Assert  
            Assert.NotNull(result);
            Assert.Single(result);
        }

        private static List<UserDetail> MockUserListResponse()
        {
            var userList = new List<UserDetail>
            {
                new UserDetail
                {
                    Id = 1, FirstName = "Vasan", RoleId= 1
                },
                new UserDetail
                {
                    Id = 2, FirstName = "Rathish", RoleId= 2
                },
                new UserDetail
                {
                    Id = 3, FirstName = "Thirumalai", RoleId= 1
                }
            };

            return userList;
        }
    }
}