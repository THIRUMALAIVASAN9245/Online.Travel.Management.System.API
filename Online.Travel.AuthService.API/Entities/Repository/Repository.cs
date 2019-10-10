using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Online.Travel.AuthService.API.Entities.Repository
{
    public class Repository : IRepository
    {
        private readonly AuthServiceDbContext movieCruiserDbContext;

        public Repository(AuthServiceDbContext movieCruiserDbContext)
        {
            this.movieCruiserDbContext = movieCruiserDbContext;
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return movieCruiserDbContext.Set<T>().AsQueryable();
        }

        public T Get<T>(int key) where T : class
        {
            return this.movieCruiserDbContext.Set<T>().Find(key);
        }

        public T Save<T>(T entity) where T : class
        {
            var entityResult =  movieCruiserDbContext.Set<T>().Add(entity).Entity;
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
