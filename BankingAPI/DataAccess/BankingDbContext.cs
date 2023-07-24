using BankingAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.DataAccess
{
    public class BankingDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
    }
}
