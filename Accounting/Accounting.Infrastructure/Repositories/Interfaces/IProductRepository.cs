using Accounting.Common;
using Accounting.Infrastructure.Models;

namespace Accounting.Infrastructure.Repositories.Interfaces;

public interface IProductRepository
{
    Task<PagedResult<Product>> GetPagedAsync(Guid MasterCompanyId, PagingModel pagingModel);
    Task CreateAsync(Guid MasterCompanyId, Product product);
    Task UpdateAsync(Guid MasterCompanyId, Product product);
    Task<Product> GetAsync(Guid MasterCompanyId, int productId);
}