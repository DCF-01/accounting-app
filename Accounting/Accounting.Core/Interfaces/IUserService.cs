using Accounting.Common;
using Accounting.Core.Requests;

namespace Accounting.Core.Interfaces;

public interface IUserService
{
    Task<PagedResult<UserGet>> GetPagedAsync(PagingModel pagingModel);
    Task UpdateAsync(UserPut userPost);
    Task DeleteAsync(string userId);
    Task CreateAsync(UserPost userPost);
    Task<UserGet> GetAsync(string userId);
}