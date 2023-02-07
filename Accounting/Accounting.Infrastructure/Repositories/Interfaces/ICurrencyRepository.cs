using Accounting.Common;
using Accounting.Infrastructure.Models;

namespace Accounting.Infrastructure.Repositories.Interfaces;

public interface ICurrencyRepository
{
    Task CreateAsync(Guid masterCompanyId, Currency currency);
    Task UpdateAsync(Guid masterCompanyId, Currency currency);
    Task<Currency> GetAsync(Guid masterCompanyId, int id);
    Task DeleteAsync(Guid masterCompanyId, int id);
    Task<PagedResult<Currency>> GetPagedAsync(Guid masterCompanyId, PagingModel pagingModel);
}