using Accounting.Common;
using Accounting.Core.Requests;

namespace Accounting.Core.Interfaces;

public interface IBankAccountService
{
    Task CreateAsync(BankAccountPost bank);
    Task UpdateAsync(BankAccountPut bank);
    Task<BankAccountGet> GetAsync(int id);
    Task DeleteAsync(int id);
    Task<PagedResult<BankAccountGet>> GetPagedAsync(PagingModel pagingModel);
}