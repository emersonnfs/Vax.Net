using Microsoft.EntityFrameworkCore;
using Vax.Models;

namespace Vax.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vacina> Vacinas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<StatusVacina> StatusVacinas { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatusVacina>()
                .HasKey(sv => sv.Id);

            modelBuilder.Entity<StatusVacina>()
                .Property(sv => sv.Status)
                .IsRequired();

            modelBuilder.Entity<StatusVacina>()
                .HasOne(sv => sv.Usuario)
                .WithMany(u => u.StatusVacinas)
                .HasForeignKey(sv => sv.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);  // Adicionando exclusão em cascata

            modelBuilder.Entity<StatusVacina>()
                .HasOne(sv => sv.Vacina)
                .WithMany(v => v.StatusVacinas)
                .HasForeignKey(sv => sv.VacinaId)
                .OnDelete(DeleteBehavior.Cascade);  // Adicionando exclusão em cascata

            base.OnModelCreating(modelBuilder);
        }
    }
}
