using Microsoft.EntityFrameworkCore;
using WebAplicationPessoa.WebAPI.DTOs;
using WebAplicationPessoa.WebAPI.Model;

namespace WebAplicationPessoa.WebAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<PessoaModel> Pessoas { get; set; }

    public DbSet<UserModel> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PessoaModel>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .UseIdentityByDefaultColumn();
            
            entity.HasIndex(e => e.Cpf)
                .IsUnique();

            entity.Property(e => e.Cpf)
                .IsRequired()
                .HasMaxLength(11);
            
            entity.Property(e => e.Nome)    
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Telefone)
                .IsRequired()
                .HasMaxLength(16);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .UseIdentityByDefaultColumn();
            
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasIndex(e => e.Nome)
                .IsUnique();
                
            entity.Property(e => e.Senha)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.Rule)
                .HasMaxLength(20);

            entity.Property(e => e.PasswordHash)
                .IsRequired();
            
            entity.Property(e => e.PasswordSalt)
                .IsRequired();
            
            entity.HasIndex(e => e.Email)
                .IsUnique();
            
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);
        });
    }
}