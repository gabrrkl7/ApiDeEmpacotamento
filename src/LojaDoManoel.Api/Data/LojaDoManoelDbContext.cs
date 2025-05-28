using LojaDoManoel.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaDoManoel.Api.Data
{
    public class LojaDoManoelDbContext : DbContext
    {
        public LojaDoManoelDbContext(DbContextOptions<LojaDoManoelDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Caixa> Caixas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definir chave primária para Produto e Pedido
            modelBuilder.Entity<Pedido>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Produto>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Caixa>()
                .HasKey(c => c.Nome);

            // Relacionamento: Pedido tem muitos Produtos
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Produtos)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
