using Accounting.Common;
using Accounting.Infrastructure.Models;

namespace Accounting.Infrastructure.Repositories.Interfaces;

public interface IMasterCompanyRepository
{
    Task CreateAsync(string name, bool active, string initialUserEmail, string initialUserPassword);
    Task AddChildUsers(Guid masterCompanyId, List<string> childUserIds);
    Task<PagedResult<MasterCompany>> GetPagedAsync(Guid masterCompanyId, PagingModel pagingModel);
    Task<MasterCompany> FindByIdAsync(Guid masterCompanyId);
    Task UpdateAsync(Guid masterCompanyId, string name, bool active);
}