using Microsoft.EntityFrameworkCore;
using ControleDeGastosApi.Models;

namespace ControleDeGastosApi.Data
{
    // O DbContext gerencia a conexão entre o C# e o SQLite
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura a exclusão dos dados da pessoa
            modelBuilder.Entity<Pessoa>()
                .HasMany(p => p.Transacoes)
                .WithOne()
                .HasForeignKey(t => t.PessoaId)
                .OnDelete(DeleteBehavior.Cascade); 
                // Se a pessoa for apagada, limpa os gastos dela automaticamente

            base.OnModelCreating(modelBuilder);
        }
    }
}