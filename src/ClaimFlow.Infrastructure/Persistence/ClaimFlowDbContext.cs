using Microsoft.EntityFrameworkCore;

namespace ClaimFlow.Infrastructure.Persistence;

public sealed class ClaimFlowDbContext(DbContextOptions<ClaimFlowDbContext> options)
    : DbContext(options)
{
}
