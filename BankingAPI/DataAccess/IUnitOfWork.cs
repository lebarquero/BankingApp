using BankingAPI.DataAccess.Repositories.IRepositories;

namespace BankingAPI.DataAccess
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;
        void Save();
        Task SaveAsync();
    }
}
