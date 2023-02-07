using Accounting.Common;
using Accounting.Infrastructure.Models;

namespace Accounting.Infrastructure.Repositories.Interfaces;

public interface IBankRepository
{
    Task CreateAsync(Guid masterCompanyId, Bank bank);
    Task UpdateAsync(Guid masterCompanyId, Bank bank);
    Task<Bank> GetAsync(Guid masterCompanyId, int id);
    Task<IEnumerable<Bank>> GetAllAsync(Guid masterCompanyId);
    Task DeleteAsync(Guid masterCompanyId, int id);
    Task<PagedResult<Bank>> GetPagedAsync(Guid masterCompanyId,  PagingModel pagingModel);
}