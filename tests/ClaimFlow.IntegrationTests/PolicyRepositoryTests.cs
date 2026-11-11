using ClaimFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ClaimFlow.Infrastructure.Persistence;
using ClaimFlow.Infrastructure.Repositories;

namespace ClaimFlow.IntegrationTests;

public class PolicyRepositoryTests
{
    [Fact]
    public async Task AddPolicy_WhenSaved_ShouldBeRetrievableById()
    {
        //Arrange
        var policy = new Policy(
            $"POL-{Guid.NewGuid():N}",
            Guid.NewGuid(),
            100000m,
            1200m,
            DateTime.Today,
            DateTime.Today.AddYears(1)
        );

        
        var optionsBuilder = new DbContextOptionsBuilder<ClaimFlowDbContext>();

        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__ClaimFlowTests")
            ?? throw new InvalidOperationException("Test database connection string is missing");

        optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)));

        
        //Act
        await using var context = new ClaimFlowDbContext(optionsBuilder.Options);

        var repository = new PolicyRepository(context);

        try
        {
            await repository.AddAsync(policy);
            await context.SaveChangesAsync();

            await using var readContext = new ClaimFlowDbContext(optionsBuilder.Options);

            var readRepository = new PolicyRepository(readContext);

            var retreivedPolicy = await readRepository.GetByIdAsync(policy.Id);

            //Assert
            Assert.NotNull(retreivedPolicy);
            Assert.Equal(policy.Id, retreivedPolicy.Id);
            Assert.Equal(policy.PolicyNumber, retreivedPolicy.PolicyNumber);
            Assert.Equal(PolicyStatus.Draft, retreivedPolicy.Status);

        }

        finally
        {
            context.Policies.Remove(policy);
            await context.SaveChangesAsync();
        }
    }

}