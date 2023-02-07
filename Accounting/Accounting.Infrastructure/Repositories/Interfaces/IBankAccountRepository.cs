using Accounting.Common;
using Accounting.Infrastructure.Models;

namespace Accounting.Infrastructure.Repositories.Interfaces;

public interface IBankAccountRepository
{
    Task CreateAsync(Guid masterCompanyId, BankAccount bankAccount);
    Task UpdateAsync(Guid masterCompanyId, BankAccount bankAccount);
    Task<BankAccount> GetAsync(Guid masterCompanyId, int id);
    Task DeleteAsync(Guid masterCompanyId, int id);
    Task<PagedResult<BankAccount>> GetPagedAsync(Guid masterCompanyId,  PagingModel pagingModel);
}