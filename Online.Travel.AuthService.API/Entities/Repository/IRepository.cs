using System.Linq;

namespace Online.Travel.AuthService.API.Entities.Repository
{
    public interface IRepository
    {
        IQueryable<T> Query<T>() where T : class;

        T Get<T> (int key) where T : class;

        T Save<T>(T entity) where T : class;

        T Update<T>(T entity) where T : class;        
    }
}
