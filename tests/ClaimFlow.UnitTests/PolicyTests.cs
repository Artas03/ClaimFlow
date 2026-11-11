using System.Reflection;
using System.Security.Authentication.ExtendedProtection;
using ClaimFlow.Domain.Entities;

namespace ClaimFlow.UnitTests;

public class PolicyTests
{
    [Fact]
    public void Activate_WhenPolicyIsDraft_ShouldChangeStatusToActive()
    {
        //Arrange
        var policy = new Policy(
            "POL-001",
            Guid.NewGuid(),
            100000m,
            1200m,
            DateTime.Today,
            DateTime.Today.AddYears(1)
        );

        //Act
        policy.Activate();
        var actualStatus = policy.Status;
        

        //Assert
        Assert.Equal(PolicyStatus.Active, actualStatus);

    }

    [Fact]
    public void Activate_WhenPolicyIsAlreadyActive_ShouldThrowInvalidOperationException()
    {
        //Arrange
        var policy = new Policy(
            "POL-001",
            Guid.NewGuid(),
            100000m,
            1200m,
            DateTime.Today,
            DateTime.Today.AddYears(1)
        );

        //Activate policy
        policy.Activate();

        //Assert
        Assert.Throws<InvalidOperationException>(() => policy.Activate());
        Assert.Equal(PolicyStatus.Active, policy.Status);

    }

    [Fact]
    public void Cancel_WhenPolicyIsActive_ShouldChangeStatusToCancelled()
    {
        //Arrange
        var policy = new Policy(
            "POL-001",
            Guid.NewGuid(),
            100000m,
            1200m,
            DateTime.Today,
            DateTime.Today.AddYears(1)
        );
        
        //Act
        policy.Activate();
        policy.Cancel();

        //Assert
        Assert.Equal(PolicyStatus.Cancelled, policy.Status);

    }

    [Fact]
    public void Cancel_WhenPolicyIsDraft_ShouldThrowInvalidOperationException()
    {
        //Arrange
        var policy = new Policy(
            "POL-001",
            Guid.NewGuid(),
            100000m,
            1200m,
            DateTime.Today,
            DateTime.Today.AddYears(1)
        );

        //Assert
        Assert.Throws<InvalidOperationException>(() => policy.Cancel());
        Assert.Equal(PolicyStatus.Draft, policy.Status);
    }

    [Fact]
    public void Constructor_WhenPremiumIsNegative_ShouldThrowArgumentOutOfRangeException()
    {
        //Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Policy(
              "POL-001",
                Guid.NewGuid(),
                100000m,
                -100m,
                DateTime.Today,
                DateTime.Today.AddYears(1)  
            )
        );

        Assert.Equal("premium", exception.ParamName);
    }

    [Fact]
    public void Constructor_WhenEndDateIsBeforeStartDate_ShouldThrowArgumentException()
    {
        //Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            new Policy(
              "POL-001",
                Guid.NewGuid(),
                100000m,
                1200m,
                DateTime.Today.AddDays(5),
                DateTime.Today  
            )
        );

        Assert.Equal("endDate", exception.ParamName);
        
    }

    [Fact]
    public void Constructor_WhenCustomerIdIsEmpty_ShouldThrowArgumentException()
    {
        //Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            new Policy(
              "POL-001",
                Guid.Empty,
                100000m,
                1200m,
                DateTime.Today,
                DateTime.Today.AddDays(5)  
            )
        );

        Assert.Equal("customerId", exception.ParamName);
    }

    [Fact]
    public void Constructor_WhenCoverageAmountIsZero_ShouldThrowArgumentOutOfRangeException()
    {
        //Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Policy(
              "POL-001",
                Guid.NewGuid(),
                0m,
                1200m,
                DateTime.Today,
                DateTime.Today.AddDays(5)  
            )
        );

        Assert.Equal("coverageAmount", exception.ParamName);
    }

    [Fact]
    public void Constructor_WhenPolicyNumberIsWhitespace_ShouldThrowArgumentException()
    {
        //Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            new Policy(
              "",
                Guid.NewGuid(),
                100000m,
                1200m,
                DateTime.Today,
                DateTime.Today.AddDays(5)  
            )
        );

        Assert.Equal("policyNumber", exception.ParamName);
    }

    [Fact]
    public void Constructor_WhenPremiumIsZero_ShouldThrowArgumentOutOfRangeException()
    {
        //Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Policy(
              "POL-001",
                Guid.NewGuid(),
                100000m,
                0m,
                DateTime.Today,
                DateTime.Today.AddDays(5)  
            )
        );

        Assert.Equal("premium", exception.ParamName);
    }

    [Fact]
    public void Constructor_WhenEndDateEqualsStartDate_ShouldThrowArgumentException()
    {
        //Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            new Policy(
              "POL-001",
                Guid.NewGuid(),
                100000m,
                1200m,
                DateTime.Today,
                DateTime.Today  
            )
        );

        Assert.Equal("endDate", exception.ParamName);
    }
}