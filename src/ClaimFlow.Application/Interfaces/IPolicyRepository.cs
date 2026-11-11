using ClaimFlow.Domain.Entities;

namespace ClaimFlow.Application.Interfaces
{
    public interface IPolicyRepository
    {
        Task AddAsync(Policy policy);

        Task<Policy?> GetByIdAsync(Guid id);

        Task<Policy?> GetByPolicyNumberAsync(string policyNumber);

    }
}
