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

        public DbSet<Cuenta> Cuentas { get; set; }

        public DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>()
                .Property(e => e.SaldoInicial)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Movimiento>()
                .Property(p => p.Saldo)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Movimiento>()
                .Property(p => p.Valor)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
