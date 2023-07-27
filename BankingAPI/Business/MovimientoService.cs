using BankingAPI.Business.IServices;
using BankingAPI.DataAccess.Repositories.IRepositories;
using BankingAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BankingAPI.Business
{
    public class MovimientoService : IService<Movimiento>
    {
        private readonly IRepository<Movimiento> _repo;

        public MovimientoService(IRepository<Movimiento> repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(Movimiento entity)
        {
            await _repo.CreateAsync(entity);
        }

        public async Task<IEnumerable<Movimiento>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Movimiento> GetAsync(int id)
        {
            return await _repo.GetAsync(i => i.MovimientoID == id, tracked: false);
        }

        public async Task RemoveAsync(Movimiento entity)
        {
            await _repo.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Movimiento entity)
        {
            await _repo.UpdateAsync(entity);
        }
    }
}
