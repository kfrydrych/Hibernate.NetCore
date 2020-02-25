using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWork.NetCore
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();

        T Get<T>(int id) where T : class;

        Task<T> GetAsync<T>(int id) where T : class;

        void SaveOrUpdate<T>(T entity);

        void Delete<T>(T entity);

        IQueryable<T> Query<T>();
    }
}