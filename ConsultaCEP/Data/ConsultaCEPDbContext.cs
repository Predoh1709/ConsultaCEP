using ConsultaCEP.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsultaCEP.Data
{
    public class ConsultaCEPDbContext : DbContext
    {
        public ConsultaCEPDbContext(DbContextOptions<ConsultaCEPDbContext> options) : base(options){    }
        
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Contatos)
                .WithOne(c => c.Cliente)
                .HasForeignKey(c => c.ClienteId);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Enderecos)
                .WithOne(e => e.Cliente)
                .HasForeignKey(e => e.ClienteId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
