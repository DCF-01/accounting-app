using Accounting.Common;
using Accounting.Infrastructure.Models;

namespace Accounting.Infrastructure.Repositories.Interfaces;

public interface IVariationRepository
{
    Task<PagedResult<Variation>> GetPagedAsync(Guid masterCompanyId, PagingModel pagingModel);
}