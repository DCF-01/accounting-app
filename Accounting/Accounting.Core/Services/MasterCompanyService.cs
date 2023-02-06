using Accounting.Common;
using Accounting.Core.Interfaces;
using Accounting.Core.Requests;

namespace Accounting.Core.Services;

public class MasterCompanyService : IMasterCompanyService
{
    public Task CreateAsync(MasterCompanyPost user)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResult<MasterCompanyGet>> GetPagedAsync(PagingModel pagingModel)
    {
        throw new NotImplementedException();
    }

    public Task<MasterCompanyGet> FindByIdAsync(Guid MasterCompanyId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(MasterCompanyPut user)
    {
        throw new NotImplementedException();
    }
}