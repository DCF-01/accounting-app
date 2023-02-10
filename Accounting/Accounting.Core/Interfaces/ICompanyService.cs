using Accounting.Common;
using Accounting.Core.Enums;
using Accounting.Core.Requests;

namespace Accounting.Core.Interfaces;

public interface ICompanyService
{
    Task CreateAsync(CompanyPost companyPost);
    Task UpdateAsync(CompanyPut companyPut);
    Task<CompanyGet> GetAsync(int id, ViewType viewType);
    Task DeleteAsync(int id);
    Task<PagedResult<CompanyGet>> GetPagedAsync(PagingModel pagingModel);
}