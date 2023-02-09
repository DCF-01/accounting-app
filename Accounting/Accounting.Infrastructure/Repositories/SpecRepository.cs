using Accounting.Common;
using Accounting.Infrastructure.Data;
using Accounting.Infrastructure.Extensions;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Repositories;

public class SpecRepository : ISpecRepository
{
    private readonly ApplicationDbContext _ctx;

    public SpecRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }
    
    public async Task<PagedResult<Spec>> GetPagedAsync(Guid masterCompanyId, PagingModel pagingModel)
    {
        var query =
            _ctx.Specs
                .Where(spec =>
                    spec.Name.Contains(pagingModel.SearchTerm)
                )
                .Where(spec => spec.MasterCompanyId == masterCompanyId);
                

        var result =
            await query
                .Select(spec => new Spec
                {
                    SpecId = spec.SpecId,
                    Name = spec.Name,
                })
                .Order(pagingModel.SortOrder, pagingModel.SortColumn)
                .Skip(pagingModel.PageSize * pagingModel.Page)
                .Take(pagingModel.PageSize)
                .ToListAsync();

        return new PagedResult<Spec>(result, await query.CountAsync());
    }
}