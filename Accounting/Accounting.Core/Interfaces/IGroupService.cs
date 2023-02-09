using Accounting.Common;
using Accounting.Core.Requests;

namespace Accounting.Core.Interfaces;

public interface IGroupService
{
    Task<PagedResult<GroupGet>> GetPagedAsync(PagingModel pagingModel);
    Task CreateAsync(GroupPost groupPost);
    Task DeleteAsync(int groupId);
}