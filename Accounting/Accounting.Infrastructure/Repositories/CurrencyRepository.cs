using Accounting.Common;
using Accounting.Infrastructure.Data;
using Accounting.Infrastructure.Extensions;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly ApplicationDbContext _ctx;

    public CurrencyRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task CreateAsync(Guid masterCompanyId, Currency currency)
    {
        currency.MasterCompanyId = masterCompanyId;
        _ctx.Currencies.Add(currency);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid masterCompanyId, Currency currency)
    {
        currency.MasterCompanyId = masterCompanyId;
        _ctx.Currencies.Update(currency);
        await _ctx.SaveChangesAsync();
    }

    public async Task<Currency> GetAsync(Guid masterCompanyId, int id)
    {
        return
            await _ctx.Currencies
                .Where(currency => currency.CurrencyId == id)
                .Where(currency => currency.MasterCompanyId == masterCompanyId)
                .SingleOrDefaultAsync();
    }

    public async Task DeleteAsync(Guid masterCompanyId, int id)
    {
        _ctx.Currencies.Remove(new Currency
        {
            CurrencyId = id,
            MasterCompanyId = masterCompanyId
        });
        await _ctx.SaveChangesAsync();
    }

    public async Task<PagedResult<Currency>> GetPagedAsync(Guid masterCompanyId, PagingModel pagingModel)
    {
        var query =
            _ctx.Currencies
                .Include(currency => currency.Products)
                .Where(currency =>
                    currency.Name.Contains(pagingModel.SearchTerm) &&
                    currency.MasterCompanyId == masterCompanyId
                );

        var result =
            await query
                .Select(currency => new Currency
                {
                    CurrencyId = currency.CurrencyId,
                    Name = currency.Name,
                    Value = currency.Value,
                    Products = currency.Products
                })
                .Order(pagingModel.SortOrder, pagingModel.SortColumn)
                .Skip(pagingModel.PageSize * pagingModel.Page)
                .Take(pagingModel.PageSize)
                .ToListAsync();

        return new PagedResult<Currency>(result, await query.CountAsync());
    }
}