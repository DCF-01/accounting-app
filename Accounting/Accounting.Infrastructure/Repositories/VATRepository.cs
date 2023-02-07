using Accounting.Common;
using Accounting.Infrastructure.Data;
using Accounting.Infrastructure.Extensions;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Repositories;

public class VATRepository : IVATRepository
{
    private readonly ApplicationDbContext _ctx;

    public VATRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task CreateAsync(Guid MasterCompanyId, VAT vat)
    {
        vat.MasterCompanyId = MasterCompanyId;
        _ctx.VATs.Add(vat);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid MasterCompanyId, VAT vat)
    {
        vat.MasterCompanyId = MasterCompanyId;
        _ctx.VATs.Update(vat);
        await _ctx.SaveChangesAsync();
    }

    public async Task<VAT> GetAsync(Guid MasterCompanyId, int id)
    {
        var vat =
            await _ctx.VATs
                .Where(vat => vat.MasterCompanyId == MasterCompanyId)
                .Where(vat => vat.VATId == id)
                .SingleOrDefaultAsync();

        return vat;
    }

    public async Task DeleteAsync(int id)
    {
        var vat = new VAT
        {
            VATId = id
        };

        _ctx.VATs.Remove(vat);
        await _ctx.SaveChangesAsync();
    }

    public async Task<PagedResult<VAT>> GetPagedAsync(Guid MasterCompanyId, PagingModel pagingModel)
    {
        var query =
            _ctx.VATs
                .Where(vats =>
                    vats.Name.Contains(pagingModel.SearchTerm) &&
                    vats.MasterCompanyId == MasterCompanyId
                );

        var result =
            await query
                .Select(vats => new VAT
                {
                    VATId = vats.VATId,
                    Name = vats.Name,
                    Value = vats.Value
                })
                .Order(pagingModel.SortOrder, pagingModel.SortColumn)
                .Skip(pagingModel.PageSize * pagingModel.Page)
                .Take(pagingModel.PageSize)
                .ToListAsync();

        return new PagedResult<VAT>(result, await query.CountAsync());
    }
}