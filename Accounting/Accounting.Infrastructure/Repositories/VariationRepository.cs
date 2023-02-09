using Accounting.Common;
using Accounting.Infrastructure.Data;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Repositories;

public class VariationRepository : IVariationRepository
{
    private readonly ApplicationDbContext _ctx;

    public VariationRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }
    public async Task<PagedResult<Variation>> GetPagedAsync(Guid masterCompanyId, PagingModel pagingModel)
    {
        var query =
            _ctx.Variations
                .Where(variation =>
                    variation.Name.Contains(pagingModel.SearchTerm)
                )
                .Where(variation => variation.MasterCompanyId == masterCompanyId)
                .Select(variation => new Variation()
                {
                    VariationId = variation.VariationId,
                    Name = variation.Name,
                });

        var result =
            await query
                .Order(pagingModel.SortOrder, pagingModel.SortColumn)
                .Skip(pagingModel.PageSize * pagingModel.Page)
                .Take(pagingModel.PageSize)
                .ToListAsync();

        return new PagedResult<Variation>(result, await query.CountAsync());
    }
}