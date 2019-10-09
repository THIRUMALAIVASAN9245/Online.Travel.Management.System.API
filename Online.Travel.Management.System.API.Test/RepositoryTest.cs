using Microsoft.EntityFrameworkCore;
using Moq;
using Online.Travel.Management.System.API.Entities;
using Online.Travel.Management.System.API.Entities.Repository;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Online.Travel.Management.System.API.Test
{
    public class RepositoryTest
    {
        private IRepository repository;

        private Mock<BookingDbContext> movieCruiserDbContext;

        [Fact]
        public void QueryMethodCallRetrunsExeExpectedResult()
        {
            // Arrange
            movieCruiserDbContext = new Mock<BookingDbContext>();
            movieCruiserDbContext.Setup(r => r.Set<Booking>()).Returns(GetMockWatchList());
            repository = new Repository(movieCruiserDbContext.Object);

            // Act            
            var result = repository.Query<Booking>();

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public void SaveMethodCallRetrunsExeExpectedResult()
        {
            // Arrange
            movieCruiserDbContext = new Mock<BookingDbContext>();
            movieCruiserDbContext.Setup(r => r.Set<Booking>()).Returns(GetMockWatchList());
            repository = new Repository(movieCruiserDbContext.Object);
            var bookingModel = new Booking
            {
                Id = 1,
                CustomerId = 1,
                EmployeeId = 2,
                PickupLocation = "Chennai",
                DropLocation = "Bangalore"
            };

            // Act            
            var result = repository.Save<Booking>(bookingModel);

            // Assert  
            Assert.NotNull(result);
        }

        private static DbSet<Booking> GetMockWatchList()
        {
            var bookingList = new List<Booking>
            {
               new Booking
               {
                    Id = 1,
                    CustomerId = 1,
                    EmployeeId = 2,
                    PickupLocation = "Chennai",
                    DropLocation = "Bangalore"
               },
               new Booking
               {
                     Id = 1,
                    CustomerId = 3,
                    EmployeeId = 2,
                    PickupLocation = "Chennai",
                    DropLocation = "Bangalore"
               }
            };

            DbSet<Booking> mockBookingList = GetQueryableMockDbSet(bookingList);

            return mockBookingList;
        }

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return dbSet.Object;
        }
    }
}
