using Accounting.Common;
using Accounting.Infrastructure.Models;

namespace Accounting.Infrastructure.Repositories.Interfaces;

public interface IVATRepository
{
    Task CreateAsync(Guid masterCompanyId, VAT vat);
    Task UpdateAsync(Guid MasterCompanyId, VAT vat);
    Task<VAT> GetAsync(Guid MasterCompanyId, int id);
    Task DeleteAsync(int id);
    Task<PagedResult<VAT>> GetPagedAsync(Guid MasterCompanyId,  PagingModel pagingModel);
}