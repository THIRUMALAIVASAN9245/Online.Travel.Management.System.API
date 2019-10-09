using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Online.Travel.Management.System.API.Entities.Repository
{
    public class Repository : IRepository
    {
        private readonly BookingDbContext movieCruiserDbContext;

        public Repository(BookingDbContext movieCruiserDbContext)
        {
            this.movieCruiserDbContext = movieCruiserDbContext;
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return movieCruiserDbContext.Set<T>().AsQueryable();
        }

        public T Save<T>(T entity) where T : class
        {
            var entityResult = movieCruiserDbContext.Set<T>().Add(entity).Entity;
            movieCruiserDbContext.SaveChanges();
            return entityResult;
        }

        public T Update<T>(T entity) where T : class
        {
            EntityEntry<T> entityEntry = movieCruiserDbContext.Entry(entity);
            movieCruiserDbContext.Set<T>().Attach(entity);
            entityEntry.State = EntityState.Modified;
            movieCruiserDbContext.SaveChanges();
            return entity;
        }
    }
}
