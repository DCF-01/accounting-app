using Accounting.Common;
using Accounting.Core.Requests;

namespace Accounting.Core.Interfaces;

public interface IVariationService
{
    Task<PagedResult<VariationGet>> GetPagedAsync(PagingModel pagingModel);
}