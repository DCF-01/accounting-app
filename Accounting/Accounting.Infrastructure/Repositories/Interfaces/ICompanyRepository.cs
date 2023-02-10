using Accounting.Common;
using Accounting.Infrastructure.Models;

namespace Accounting.Infrastructure.Repositories.Interfaces;

public interface ICompanyRepository
{
    Task CreateAsync(Guid masterCompanyId, Company company);
    Task UpdateAsync(Guid masterCompanyId, Company company);
    Task<Company> GetAsync(Guid masterCompanyId, int id);
    Task DeleteAsync(Guid masterCompanyId, int id);
    Task<PagedResult<Company>> GetPagedAsync(Guid masterCompanyId,  PagingModel pagingModel);
}