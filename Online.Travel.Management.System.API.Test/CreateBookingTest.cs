using AutoMapper;
using Moq;
using Online.Travel.Management.System.API.Controllers;
using Online.Travel.Management.System.API.Entities.Repository;
using Online.Travel.Management.System.API.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Online.Travel.Management.System.API.Test
{
    public class CreateBookingTest
    {
        private CreateBookingRequest request;

        private CreateBooking underTest;

        private Mock<IRepository> repository;

        [Fact]
        public async Task HandleWithValidCreateRequestCallSaveAsExpectedResultAsync()
        {
            // Arrange                    
            var bookingModel = new Booking { Id = 299536, DropLocation = "Chennai, Chrompet" };

            var config = new MapperConfiguration(m => { m.CreateMap<Entities.Booking, Booking>(); });
            var mapper = new Mapper(config);
            var bookingList = MockBookingListResponse().ToList().AsQueryable();

            repository = new Mock<IRepository>();
            repository.Setup(m => m.Query<Entities.Booking>())
              .Returns(bookingList);

            underTest = new CreateBooking(repository.Object, mapper);
            request = new CreateBookingRequest(bookingModel);

            // Act
            CancellationToken cancellationToken;
            var result = await underTest.Handle(request, cancellationToken);

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(bookingModel.Id, result.Id);
            Assert.Equal(bookingModel.DropLocation, result.DropLocation);
        }

        private static List<Entities.Booking> MockBookingListResponse()
        {
            var bookingList = new List<Entities.Booking>
            {
                new Entities.Booking
                {
                    Id = 1,
                    CustomerId = 1,
                    EmployeeId = 2,
                  PickupLocation = "Chennai",
                    DropLocation = "Bangalore"
                },
                new Entities.Booking
                {
                     Id = 1,
                    CustomerId = 3,
                    EmployeeId = 2,
                    PickupLocation = "Chennai",
                    DropLocation = "Bangalore"
                }
            };

            return bookingList;
        }
    }
}