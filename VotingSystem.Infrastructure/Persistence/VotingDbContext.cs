using Microsoft.EntityFrameworkCore;
using VotingSystem.Domain.Entities;

namespace VotingSystem.Infrastructure.Persistence;

public class VotingDbContext : DbContext
{
    public VotingDbContext(DbContextOptions<VotingDbContext> options)
        : base(options) { }

    public DbSet<Candidato> Candidatos => Set<Candidato>();
    public DbSet<Votante> Votantes => Set<Votante>();
    public DbSet<Voto> Votos => Set<Voto>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Votante>()
            .HasIndex(v => v.Codigo)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}