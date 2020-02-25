using NHibernate;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWork.NetCore.NHibernate
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;
        private bool _isAlive = true;

        public UnitOfWork(ISession session)
        {
            _session = session;
            _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
        }
        public void Commit()
        {
            if (!_isAlive)
                return;

            try
            {
                _transaction.Commit();
            }
            finally
            {
                _isAlive = false;
                _transaction.Dispose();
                _session.Dispose();
            }
        }

        public async Task CommitAsync()
        {
            if (!_isAlive)
                await Task.CompletedTask;

            try
            {
                await _transaction.CommitAsync();
            }
            finally
            {
                _isAlive = false;
                _transaction.Dispose();
                _session.Dispose();
            }
        }

        public T Get<T>(int id) where T : class
        {
            return _session.Get<T>(id);
        }

        public async Task<T> GetAsync<T>(int id) where T : class
        {
            return await _session.GetAsync<T>(id);
        }

        public void SaveOrUpdate<T>(T entity)
        {
            _session.SaveOrUpdate(entity); ;
        }

        public void Delete<T>(T entity)
        {
            _session.Delete(entity);
        }

        public IQueryable<T> Query<T>()
        {
            return _session.Query<T>();
        }
    }
}
