using Microsoft.EntityFrameworkCore;
using ProjetoColeta.Models;
using ProjetoColetaModels;

namespace ProjetoColeta.Data.Contexts;

public class DatabaseContext : DbContext
{
    public virtual DbSet<ClienteModel> Clientes { get; set; }
    public virtual DbSet<ColetorModel> Coletas { get; set; }
    public virtual DbSet<ColetorModel> Coletores { get; set; }
    public virtual DbSet<PontoModel> Pontos { get; set; }
    public virtual DbSet<ResiduoModel> Residuos { get; set; }
    
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    protected DatabaseContext()
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClienteModel>(entity =>
        {
            entity.ToTable("Clientes");
            entity.HasKey(e => e.IdCliente);
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.Nome).HasMaxLength(100);
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.Cpf).IsRequired();
            entity.HasIndex(e=>e.Cpf).IsUnique();
            entity.Property(e=>e.NumeroCasa).IsRequired();
        });

        modelBuilder.Entity<ColetaModel>(entity =>
        {
            entity.ToTable("Coletas");
            entity.HasKey(e => e.IdColeta);
            entity.Property(e => e.DataColeta)
                .IsRequired()
                .HasColumnType("date");
            entity.HasOne(e => e.Ponto).WithMany().HasForeignKey(e => e.IdPonto);
        });

        modelBuilder.Entity<PontoModel>(entity =>
        {
            entity.ToTable("Pontos");
            entity.HasKey(e => e.IdPonto);
            entity.Property(e => e.Local).IsRequired();
            entity.Property(e => e.CapacidadeMaxima).IsRequired();
            entity.Property(e => e.Status).IsRequired();
            
        });

        modelBuilder.Entity<ColetorModel>(entity =>
        {
            entity.ToTable("Coletores");
            entity.HasKey(e => e.IdColetor);
            entity.Property(e=>e.NomeColetor).IsRequired();
        });

        modelBuilder.Entity<ResiduoModel>(entity =>
        {
            entity.ToTable("Residuos");
            entity.HasKey(e => e.IdResiduo);
            entity.Property(e=>e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e=>e.Peso).IsRequired();
            entity.Property(e=>e.EhReciclavel).IsRequired();
            // entity.HasOne(e=>e.Cliente).WithMany().HasForeignKey(e=>e.IdCliente);
        });
        base.OnModelCreating(modelBuilder);
    }
}