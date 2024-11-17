using BloodBank.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.API.Persistence
{
    public class BloodBankDbContext : DbContext
    {
        public BloodBankDbContext(DbContextOptions<BloodBankDbContext> options) : base(options)
        {

        }

        public DbSet<Doacao> Doacoes { get; set; }
        public DbSet<Doador> Doadores { get; set; }
        public DbSet<Estoque> Estoques { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Doacao>(e =>
            {
                e.HasKey(d => d.Id);

                //e.HasOne(d => d.Doador)
                //    .WithMany(e => e.Doacoes)
                //    .HasForeignKey(d => d.DoadorId);
            });

            builder.Entity<Doador>(e =>
            {
                e.HasKey(d => d.Id);

                e.HasMany(d => d.Doacoes)
                     .WithOne(d => d.Doador)
                     .HasForeignKey(d => d.DoadorId)
                     .OnDelete(DeleteBehavior.Restrict);

                e.OwnsOne(d => d.Endereco, o => //utiliza-se para relacionar uma entidade sem criar uma tabela no banco, exclusiva para o endereço
                {
                    o.Property(o => o.Logradouro).HasColumnName("Logradouro"); //tratativa para não ficar com o nome da coluna Endereco_Logradouro
                    o.Property(o => o.Numero).HasColumnName("Numero");
                    o.Property(o => o.Bairro).HasColumnName("Bairro");
                    o.Property(o => o.Cidade).HasColumnName("Cidade");
                    o.Property(o => o.Estado).HasColumnName("Estado");
                    o.Property(o => o.CEP).HasColumnName("CEP");
                });
            });

            builder.Entity<Estoque>(e =>
            {
                e.HasKey(d => d.Id);
            });

            base.OnModelCreating(builder);
        }
    }
}
