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
    public class GetBookingTest
    {
        private GetBookingRequest request;

        private GetBooking underTest;

        private Mock<IRepository> repository;

        [Fact]
        public async Task HandleWithValidCreateRequestCallSaveAsExpectedResultGetAllAsync()
        {
            // Arrange                    
            var bookingModelRequest = new BookingRequest { Operation = "GetAll" };

            var config = new MapperConfiguration(m => { m.CreateMap<Entities.Booking, Booking>(); });
            var mapper = new Mapper(config);
            var bookingList = MockBookingListResponse().ToList().AsQueryable();

            repository = new Mock<IRepository>();
            repository.Setup(m => m.Query<Entities.Booking>())
              .Returns(bookingList);

            underTest = new GetBooking(repository.Object, mapper);
            request = new GetBookingRequest(bookingModelRequest);

            // Act
            CancellationToken cancellationToken;
            var result = await underTest.Handle(request, cancellationToken);

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task HandleWithValidCreateRequestCallSaveAsExpectedResultGetByCustomerAsync()
        {
            // Arrange                    
            var bookingModelRequest = new BookingRequest { Id = 1, Operation = "GetByCustomer" };

            var config = new MapperConfiguration(m => { m.CreateMap<Entities.Booking, Booking>(); });
            var mapper = new Mapper(config);
            var bookingList = MockBookingListResponse().ToList().AsQueryable();

            repository = new Mock<IRepository>();
            repository.Setup(m => m.Query<Entities.Booking>())
              .Returns(bookingList);

            underTest = new GetBooking(repository.Object, mapper);
            request = new GetBookingRequest(bookingModelRequest);

            // Act
            CancellationToken cancellationToken;
            var result = await underTest.Handle(request, cancellationToken);

            // Assert  
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task HandleWithValidCreateRequestCallSaveAsExpectedResultGetByEmployeeAsync()
        {
            // Arrange                    
            var bookingModelRequest = new BookingRequest { Id = 2, Operation = "GetByEmployee" };

            var config = new MapperConfiguration(m => { m.CreateMap<Entities.Booking, Booking>(); });
            var mapper = new Mapper(config);
            var bookingList = MockBookingListResponse().ToList().AsQueryable();

            repository = new Mock<IRepository>();
            repository.Setup(m => m.Query<Entities.Booking>())
              .Returns(bookingList);

            underTest = new GetBooking(repository.Object, mapper);
            request = new GetBookingRequest(bookingModelRequest);

            // Act
            CancellationToken cancellationToken;
            var result = await underTest.Handle(request, cancellationToken);

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
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