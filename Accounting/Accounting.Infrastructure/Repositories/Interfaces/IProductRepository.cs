using Accounting.Common;
using Accounting.Infrastructure.Models;

namespace Accounting.Infrastructure.Repositories.Interfaces;

public interface IProductRepository
{
    Task<PagedResult<Product>> GetPagedAsync(Guid masterCompanyId, PagingModel pagingModel);
    Task CreateAsync(Guid masterCompanyId, Product product);
    Task UpdateAsync(Guid masterCompanyId, Product product);
    Task<Product> GetAsync(Guid masterCompanyId, int productId);
}