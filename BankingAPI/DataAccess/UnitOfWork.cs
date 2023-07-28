using BankingAPI.DataAccess.Repositories;
using BankingAPI.DataAccess.Repositories.IRepositories;

namespace BankingAPI.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankingDbContext _db;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(BankingDbContext db)
        {
            _db = db;
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
                return (IRepository<T>)_repositories[typeof(T)];

            var repository = new Repository<T>(_db);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
