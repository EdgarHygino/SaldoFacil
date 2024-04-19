using Microsoft.EntityFrameworkCore;
using SaldoFacil.Model.Models.Entities;

namespace SaldoFacil.Transacao.API.Dados.Context
{
    public class SaldoFacilDbContext : DbContext
    {
        public SaldoFacilDbContext(DbContextOptions<SaldoFacilDbContext> opcoes) : base(opcoes)
        {
        }

        public DbSet<Clientes> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).ValueGeneratedOnAdd();
            });
        }
    }
}
