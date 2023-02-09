using Accounting.Common;
using Accounting.Infrastructure.Models;

namespace Accounting.Infrastructure.Repositories.Interfaces;

public interface IGroupRepository
{
    Task<PagedResult<Group>> GetPagedAsync(Guid masterCompanyId, PagingModel pagingModel);
    Task CreateAsync(Guid masterCompanyId, Group group);
    Task DeleteAsync(Guid masterCompanyId, int groupId);
}