using Accounting.Common;
using Accounting.Core.Requests;

namespace Accounting.Core.Interfaces;

public interface IMasterCompanyService
{
    Task CreateAsync(MasterCompanyPost user);
    Task<PagedResult<MasterCompanyGet>> GetPagedAsync(PagingModel pagingModel);
    Task<MasterCompanyGet> FindByIdAsync(Guid MasterCompanyId);
    Task UpdateAsync(MasterCompanyPut user);
}