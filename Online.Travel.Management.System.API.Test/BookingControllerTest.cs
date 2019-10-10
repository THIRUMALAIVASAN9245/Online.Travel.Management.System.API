using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Online.Travel.Management.System.API.Controllers;
using Online.Travel.Management.System.API.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Online.Travel.Management.System.API.Test
{
    public class BookingControllerTest
    {
        private Mock<IMediator> mediatR;

        private BookingController controller;

        [Fact]
        public async Task PostCallsMediatRWithExpectedResult()
        {
            // Arrange
            var bookingModel = new Booking { Id = 299536, DropLocation = "Chennai, Chrompet" };
            mediatR = new Mock<IMediator>();
            mediatR.Setup(m => m.Send(It.IsAny<CreateBookingRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(bookingModel));
            controller = new BookingController(mediatR.Object);

            // Act
            var result = await controller.Post(bookingModel) as CreatedResult;

            // Assert            
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            var bookingDetail = result.Value as Booking;
            Assert.NotNull(bookingDetail);
            Assert.Equal("Chennai, Chrompet", bookingDetail.DropLocation);
        }

        [Fact]
        public async Task PostCallsMediatRWithExpectedNotFoundResult()
        {
            // Arrange
            var bookingModel = new Booking { Id = 299536, DropLocation = "Chennai, Chrompet" };
            mediatR = new Mock<IMediator>();
            mediatR.Setup(m => m.Send(It.IsAny<CreateBookingRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Booking>(null));
            controller = new BookingController(mediatR.Object);

            // Act
            var result = await controller.Post(bookingModel) as ObjectResult;

            // Assert
            mediatR.Verify(m => m.Send(It.IsAny<CreateBookingRequest>(), It.IsAny<CancellationToken>()), Times.Once());
            Assert.NotNull(result);
            Assert.Equal(409, result.StatusCode);
        }

        [Fact]
        public async Task PostCallsWithInValidModelStateExpectedNotFoundResult()
        {
            // Arrange
            var watchListModel = new Booking { Id = 383498 };
            mediatR = new Mock<IMediator>();
            controller = new BookingController(mediatR.Object);
            controller.ModelState.AddModelError("CustomerId", "CustomerId is Required");

            // Act
            var result = await controller.Post(watchListModel) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task GetCallsMediatRWithExpectedResult()
        {
            // Arrange
            var bookingModelRequest = new BookingRequest { Id = 2, Operation = "GetByEmployee" };
            mediatR = new Mock<IMediator>();
            mediatR.Setup(m => m.Send(It.IsAny<GetBookingRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(MockBookingListResponse()));
            controller = new BookingController(mediatR.Object);

            // Act
            var result = await controller.GetBooking(bookingModelRequest) as OkObjectResult;

            // Assert            
            Assert.NotNull(result);
            var bookingResponse = result.Value as List<BookingResponse>;
            Assert.NotNull(bookingResponse);
            Assert.Equal(2, bookingResponse.Count);
        }

        [Fact]
        public async Task GetCallsMediatRWithExpectedNoResultFound()
        {
            // Arrange
            var bookingModelRequest = new BookingRequest { Id = 2, Operation = "GetByEmployee" };
            mediatR = new Mock<IMediator>();
            mediatR.Setup(m => m.Send(It.IsAny<GetBookingRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<List<BookingResponse>>(null));
            controller = new BookingController(mediatR.Object);

            // Act
            var result = await controller.GetBooking(bookingModelRequest) as NotFoundObjectResult;

            // Assert            
            mediatR.Verify(m => m.Send(It.IsAny<GetBookingRequest>(), It.IsAny<CancellationToken>()), Times.Once());
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        private static List<BookingResponse> MockBookingListResponse()
        {
            var bookingList = new List<BookingResponse>
            {
                new BookingResponse
                {
                    BookingDetails = new Booking{ Id= 1,DropLocation="Chennai" },
                    CustomerDetails = new UserModel { Id=1, FirstName ="Thirumalai" },
                    EmployeeDetails = new UserModel { Id=1, FirstName ="Vasan" },
                },
                new BookingResponse
                {
                    BookingDetails = new Booking{ Id= 1,DropLocation="Bangalore" },
                    CustomerDetails = new UserModel { Id=1, FirstName ="Thirumalai" },
                    EmployeeDetails = new UserModel { Id=2, FirstName ="Vasan" },
                }
            };

            return bookingList;
        }
    }
}