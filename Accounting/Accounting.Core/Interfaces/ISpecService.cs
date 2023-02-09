using Accounting.Common;
using Accounting.Core.Requests;

namespace Accounting.Core.Interfaces;

public interface ISpecService
{
    Task<PagedResult<SpecGet>> GetPagedAsync(PagingModel pagingModel);
}