using BankingAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.DataAccess
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
