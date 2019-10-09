using AutoMapper;
using Moq;
using Online.Travel.Management.System.API.Controllers;
using Online.Travel.Management.System.API.Entities.Repository;
using Online.Travel.Management.System.API.Model;
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
            var bookingEntitiesModel = new Entities.Booking { Id = 299536, DropLocation = "Chennai, Chrompet" };

            var config = new MapperConfiguration(m => { m.CreateMap<Entities.Booking, Booking>(); });
            var mapper = new Mapper(config);

            repository = new Mock<IRepository>();
            repository.Setup(m => m.Get<Entities.Booking>(It.IsAny<int>()))
              .Returns(bookingEntitiesModel);

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
    }
}