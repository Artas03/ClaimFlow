using ClaimFlow.Application.Interfaces;
using ClaimFlow.Domain.Entities;
using ClaimFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClaimFlow.Infrastructure.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly ClaimFlowDbContext _claimFlowDbContext;

        public PolicyRepository(ClaimFlowDbContext claimFlowDbContext)
        {
            _claimFlowDbContext = claimFlowDbContext;
        }


        public async Task AddAsync(Policy policy)
        {
            await _claimFlowDbContext.Policies.AddAsync(policy);
        }

        public async Task<Policy?> GetByIdAsync(Guid id)
        {
            return await _claimFlowDbContext.Policies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Policy?> GetByPolicyNumberAsync(string policyNumber)
        {
            return await _claimFlowDbContext.Policies.FirstOrDefaultAsync(x => x.PolicyNumber == policyNumber);
        }
    }
}
