using ClaimFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace ClaimFlow.Infrastructure.Persistence;

public sealed class ClaimFlowDbContext(DbContextOptions<ClaimFlowDbContext> options)
    : DbContext(options)
{
    public DbSet<Policy> Policies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ClaimFlowDbContext).Assembly );
    }
}
