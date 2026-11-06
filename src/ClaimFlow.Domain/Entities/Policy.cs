public class Policy
{
    public Guid Id { get; private set; }
    public string PolicyNumber { get; private set; } = null!;
    public Guid CustomerId { get; private set; }
    public decimal CoverageAmount { get; private set; }
    public decimal Premium { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public PolicyStatus Status { get; private set; }

    public Policy(string policyNumber, Guid customerId, decimal coverageAmount, decimal premium, DateTime startDate, DateTime endDate)
{
    if (customerId == Guid.Empty)
    {
        throw new ArgumentException("CustomerId cannot be empty.", nameof(customerId));
    }

    if (string.IsNullOrWhiteSpace(policyNumber))
    {
        throw new ArgumentException("PolicyNumber cannot be null.", nameof(policyNumber));
    }

    if (coverageAmount <= 0)
    {
        throw new ArgumentOutOfRangeException(nameof(coverageAmount), "CoverageAmount must be greater than zero.");
    }

    if (premium <= 0)
    {
        throw new ArgumentOutOfRangeException(nameof(premium), "Premium cannot be negative.");
    }

    if (endDate <= startDate)
    {
        throw new ArgumentException("StartDate must be earlier than EndDate.", nameof(endDate));
    }

    Id = Guid.NewGuid();
    PolicyNumber = policyNumber;
    CustomerId = customerId;
    CoverageAmount = coverageAmount;
    Premium = premium;
    StartDate = startDate;
    EndDate = endDate;
    Status = PolicyStatus.Draft;
}

public void Activate()
    {
        if (Status == PolicyStatus.Draft)
        {
            Status = PolicyStatus.Active;
                
        }
        else throw new InvalidOperationException(
                "Only draft policies can be activated"
                );
        
    }

public void Cancel()
    {
        if (Status == PolicyStatus.Active)
        {
            Status = PolicyStatus.Cancelled;
        }
        else throw new InvalidOperationException(
            "Only active policies can be cancelled"
            );
    }

}





