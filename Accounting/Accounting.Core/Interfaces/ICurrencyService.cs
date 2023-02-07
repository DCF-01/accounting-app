using Accounting.Common;
using Accounting.Core.Requests;

namespace Accounting.Core.Interfaces;

public interface ICurrencyService
{
    Task CreateAsync(CurrencyPost currencyPost);
    Task UpdateAsync(CurrencyPut currencyPut);
    Task<CurrencyGet> GetAsync(int id);
    Task DeleteAsync(int id);
    Task<PagedResult<CurrencyGet>> GetPagedAsync(PagingModel pagingModel);
}