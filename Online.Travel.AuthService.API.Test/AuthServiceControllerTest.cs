using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Online.Travel.AuthService.API.Controllers;
using Online.Travel.AuthService.API.Infrastructure;
using Online.Travel.AuthService.API.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Online.Travel.Management.System.API.Test
{
    public class AuthServiceControllerTest
    {
        private Mock<IMediator> mediatR;

        private Mock<ITokenGenerator> tokenGenerator;

        private AuthServiceController controller;

        [Fact]
        public async Task PostCallsMediatRWithExpectedResult()
        {
            // Arrange
            var userModel = new UserModel { Id = 299536, FirstName = "Thirumalai" };
            mediatR = new Mock<IMediator>();
            tokenGenerator = new Mock<ITokenGenerator>();
            mediatR.Setup(m => m.Send(It.IsAny<CreateUserRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(userModel));
            controller = new AuthServiceController(mediatR.Object, tokenGenerator.Object);

            // Act
            var result = await controller.Post(userModel) as CreatedResult;

            // Assert            
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            var userDetail = result.Value as UserModel;
            Assert.NotNull(userDetail);
            Assert.Equal("Thirumalai", userDetail.FirstName);
        }

        [Fact]
        public async Task PostCallsMediatRWithExpectedNotFoundResult()
        {
            // Arrange
            var userModel = new UserModel { Id = 299536, FirstName = "Thirumalai" };
            mediatR = new Mock<IMediator>();
            tokenGenerator = new Mock<ITokenGenerator>();
            mediatR.Setup(m => m.Send(It.IsAny<CreateUserRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<UserModel>(null));
            controller = new AuthServiceController(mediatR.Object, tokenGenerator.Object);

            // Act
            var result = await controller.Post(userModel) as ObjectResult;

            // Assert
            mediatR.Verify(m => m.Send(It.IsAny<CreateUserRequest>(), It.IsAny<CancellationToken>()), Times.Once());
            Assert.NotNull(result);
            Assert.Equal(409, result.StatusCode);
        }

        [Fact]
        public async Task PostCallsWithInValidModelStateExpectedNotFoundResult()
        {
            // Arrange
            var userModel = new UserModel { Id = 299536, FirstName = "Thirumalai" };
            mediatR = new Mock<IMediator>();
            tokenGenerator = new Mock<ITokenGenerator>();
            controller = new AuthServiceController(mediatR.Object, tokenGenerator.Object);
            controller.ModelState.AddModelError("CustomerId", "CustomerId is Required");

            // Act
            var result = await controller.Post(userModel) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }


        [Fact]
        public async Task GetCallsMediatRWithExpectedResult()
        {
            // Arrange
            var userModel = new UserModel { Id = 299536, FirstName = "Thirumalai" };
            mediatR = new Mock<IMediator>();
            tokenGenerator = new Mock<ITokenGenerator>();
            mediatR.Setup(m => m.Send(It.IsAny<GetUserInfoRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(userModel));
            controller = new AuthServiceController(mediatR.Object, tokenGenerator.Object);

            // Act
            var result = await controller.Get(userModel.Id) as OkObjectResult;

            // Assert            
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var userDetail = result.Value as UserModel;
            Assert.NotNull(userDetail);
            Assert.Equal("Thirumalai", userDetail.FirstName);
        }

        [Fact]
        public async Task GetCallsMediatRWithExpectedNotFoundResult()
        {
            // Arrange
            var userModel = new UserModel { Id = 299536, FirstName = "Thirumalai" };
            mediatR = new Mock<IMediator>();
            tokenGenerator = new Mock<ITokenGenerator>();
            mediatR.Setup(m => m.Send(It.IsAny<GetUserInfoRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<UserModel>(null));
            controller = new AuthServiceController(mediatR.Object, tokenGenerator.Object);

            // Act
            var result = await controller.Get(userModel.Id) as NotFoundObjectResult;

            // Assert
            mediatR.Verify(m => m.Send(It.IsAny<GetUserInfoRequest>(), It.IsAny<CancellationToken>()), Times.Once());
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetCallsWithInValidModelStateExpectedNotFoundResult()
        {
            // Arrange
            var userModel = new UserModel { Id = 299536, FirstName = "Thirumalai" };
            mediatR = new Mock<IMediator>();
            tokenGenerator = new Mock<ITokenGenerator>();
            controller = new AuthServiceController(mediatR.Object, tokenGenerator.Object);
            controller.ModelState.AddModelError("Email", "Thirumalai@test.com");

            // Act
            var result = await controller.Get(userModel.Id) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task LoginCallsMediatRWithExpectedResult()
        {
            // Arrange
            var userModel = new UserModel { Id = 299536, FirstName = "Thirumalai" };
            mediatR = new Mock<IMediator>();
            tokenGenerator = new Mock<ITokenGenerator>();
            tokenGenerator.Setup(m => m.GetJwtTokenLoggedinUser(It.IsAny<UserModel>())).Returns("JwtSecurityTokenHandlerToken");
            mediatR.Setup(m => m.Send(It.IsAny<LoginUserRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(userModel));
            controller = new AuthServiceController(mediatR.Object, tokenGenerator.Object);

            // Act
            var result = await controller.Login(userModel) as OkObjectResult;

            // Assert            
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var jwtSecurityToken = result.Value as string;
            Assert.NotNull(jwtSecurityToken);
            Assert.Equal("JwtSecurityTokenHandlerToken", jwtSecurityToken);
        }

        [Fact]
        public async Task LoginCallsMediatRWithExpectedNotFoundResult()
        {
            // Arrange
            var userModel = new UserModel { Id = 299536, FirstName = "Thirumalai" };
            mediatR = new Mock<IMediator>();
            tokenGenerator = new Mock<ITokenGenerator>();
            tokenGenerator.Setup(m => m.GetJwtTokenLoggedinUser(It.IsAny<UserModel>())).Returns("JwtSecurityTokenHandlerToken");
            mediatR.Setup(m => m.Send(It.IsAny<LoginUserRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<UserModel>(null));
            controller = new AuthServiceController(mediatR.Object, tokenGenerator.Object);

            // Act
            var result = await controller.Login(userModel) as NotFoundObjectResult;

            // Assert
            mediatR.Verify(m => m.Send(It.IsAny<LoginUserRequest>(), It.IsAny<CancellationToken>()), Times.Once());
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task LoginCallsWithInValidModelStateExpectedNotFoundResult()
        {
            // Arrange
            var userModel = new UserModel { Id = 299536, FirstName = "Thirumalai" };
            mediatR = new Mock<IMediator>();
            tokenGenerator = new Mock<ITokenGenerator>();
            tokenGenerator.Setup(m => m.GetJwtTokenLoggedinUser(It.IsAny<UserModel>())).Returns("JwtSecurityTokenHandlerToken");
            controller = new AuthServiceController(mediatR.Object, tokenGenerator.Object);
            controller.ModelState.AddModelError("Email", "Thirumalai@test.com");

            // Act
            var result = await controller.Login(userModel) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        private static List<UserModel> MockUserListResponse()
        {
            var userList = new List<UserModel>
            {
                new UserModel
                {
                    Id = 299536, FirstName = "Thirumalai"
                },
                new UserModel
                {
                   Id = 299536, FirstName = "Thirumalai"
                }
            };

            return userList;
        }
    }
}