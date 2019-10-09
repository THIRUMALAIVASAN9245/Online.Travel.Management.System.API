using System.Linq;

namespace Online.Travel.Management.System.API.Entities.Repository
{
    public interface IRepository
    {
        IQueryable<T> Query<T>() where T : class;

        T Save<T>(T entity) where T : class;      
    }
}
