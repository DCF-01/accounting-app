using Accounting.Common;
using Accounting.Core.Requests;

namespace Accounting.Core.Interfaces;

public interface IVATService
{
    Task CreateAsync(VATPost vatPost);
    Task UpdateAsync(VATPut vatPut);
    Task<VATGet> GetAsync(int id);
    Task DeleteAsync(int id);
    Task<PagedResult<VATGet>> GetPagedAsync(PagingModel pagingModel);
}