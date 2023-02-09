using Accounting.Common;
using Accounting.Infrastructure.Models;

namespace Accounting.Infrastructure.Repositories.Interfaces;

public interface ISpecRepository
{
    Task<PagedResult<Spec>> GetPagedAsync(Guid masterCompanyId, PagingModel pagingModel);
}