using Accounting.Common;
using Accounting.Core.Requests;

namespace Accounting.Core.Interfaces;

public interface IBankService
{
    Task CreateAsync(BankPost bank);
    Task UpdateAsync(BankPut bank);
    Task<BankGet> GetAsync(int id = -1);
    Task DeleteAsync(int id);
    Task<PagedResult<BankGet>> GetPagedAsync(PagingModel pagingModel);
}