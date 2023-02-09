using Accounting.Common;
using Accounting.Core.Requests;

namespace Accounting.Core.Interfaces;

public interface IProductService
{
    Task<PagedResult<ProductGet>> GetPagedAsync(PagingModel pagingModel);
    Task<ProductGet> GetAsync(int id = -1);
    Task CreateAsync(ProductPost productPost);
    Task UpdateAsync(ProductPut productPut);
}