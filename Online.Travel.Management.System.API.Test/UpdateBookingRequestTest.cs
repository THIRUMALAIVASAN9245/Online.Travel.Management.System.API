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
    public class UpdateBookingRequestTest
    {
        private UpdateBookingRequest request;

        private UpdateBooking underTest;

        private Mock<IRepository> repository;

        [Fact]
        public async Task HandleWithValidUpdateRequestCallSaveAsExpectedResultAsync()
        {
            // Arrange                    
            var bookingModel = new Booking { Id = 299536, DropLocation = "Chennai, Chrompet" };

            var config = new MapperConfiguration(m => { m.CreateMap<Entities.Booking, Booking>(); m.CreateMap<Booking, Entities.Booking>(); });
            var mapper = new Mapper(config);

            var bookingEntity = new Entities.Booking { Id = 299536, DropLocation = "Chennai, Chrompet" };
            repository = new Mock<IRepository>();
            repository.Setup(m => m.Get<Entities.Booking>(It.IsAny<int>())).Returns(bookingEntity);
            repository.Setup(m => m.Update<Entities.Booking>(It.IsAny<Entities.Booking>())).Returns(bookingEntity);

            underTest = new UpdateBooking(repository.Object, mapper);
            request = new UpdateBookingRequest(bookingModel);

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