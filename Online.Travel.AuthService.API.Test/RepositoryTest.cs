using Microsoft.EntityFrameworkCore;
using Moq;
using Online.Travel.AuthService.API.Entities;
using Online.Travel.AuthService.API.Entities.Repository;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Online.Travel.AuthService.API.Test
{
    public class RepositoryTest
    {
        private IRepository repository;

        private Mock<AuthServiceDbContext> movieCruiserDbContext;

        [Fact]
        public void QueryMethodCallRetrunsExeExpectedResult()
        {
            // Arrange
            movieCruiserDbContext = new Mock<AuthServiceDbContext>();
            movieCruiserDbContext.Setup(r => r.Set<UserDetail>()).Returns(GetMockWatchList());
            repository = new Repository(movieCruiserDbContext.Object);

            // Act            
            var result = repository.Query<UserDetail>();

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public void GetMethodCallRetrunsExeExpectedResult()
        {
            // Arrange
            movieCruiserDbContext = new Mock<AuthServiceDbContext>();
            movieCruiserDbContext.Setup(r => r.Set<UserDetail>()).Returns(GetMockWatchList());
            repository = new Repository(movieCruiserDbContext.Object);

            // Act            
            var result = repository.Get<UserDetail>(1);

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void SaveMethodCallRetrunsExeExpectedResult()
        {
            // Arrange
            movieCruiserDbContext = new Mock<AuthServiceDbContext>();
            movieCruiserDbContext.Setup(r => r.Set<UserDetail>()).Returns(GetMockWatchList());
            repository = new Repository(movieCruiserDbContext.Object);
            var bookingModel = new UserDetail
            {
                Id = 1,
                FirstName = "Thiru"
            };

            // Act            
            var result = repository.Save<UserDetail>(bookingModel);

            // Assert  
            Assert.NotNull(result);
        }

        private static DbSet<UserDetail> GetMockWatchList()
        {
            var bookingList = new List<UserDetail>
            {
               new UserDetail
               {
                    Id = 1,
                    FirstName = "Thiru"
               },
               new UserDetail
               {
                     Id = 1,
                    FirstName = "Thiru"
               }
            };

            DbSet<UserDetail> mockBookingList = GetQueryableMockDbSet(bookingList);

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
