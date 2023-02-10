using Accounting.Common;
using Accounting.Infrastructure.Models;

namespace Accounting.Infrastructure.Repositories.Interfaces;

public interface IUserRepository
{
    Task<PagedResult<User>> GetPagedAsync(Guid masterCompanyId, PagingModel pagingModel);
    Task<User> GetAsync(Guid masterCompanyId, string userId);
    Task CreateAsync(Guid masterCompanyId, string password, User user);
    Task UpdateAsync(Guid masterCompanyId, User user);
    Task DeleteAsync(Guid masterCompanyId, string userId);
}