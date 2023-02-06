using Accounting.Common;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;

namespace Accounting.Infrastructure.Repositories;

public class MasterCompanyRepository : IMasterCompanyRepository
{
    public Task CreateAsync(string name, bool active, string initialUserEmail, string initialUserPassword)
    {
        throw new NotImplementedException();
    }

    public Task AddChildUsers(Guid masterCompanyId, List<string> childUserIds)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResult<MasterCompany>> GetPagedAsync(Guid masterCompanyId, PagingModel pagingModel)
    {
        throw new NotImplementedException();
    }

    public Task<MasterCompany> FindByIdAsync(Guid masterCompanyId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid masterCompanyId, string name, bool active)
    {
        throw new NotImplementedException();
    }
}