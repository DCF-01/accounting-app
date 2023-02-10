using Accounting.Common;
using Accounting.Infrastructure.Data;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using Accounting.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _ctx;

    public CompanyRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task CreateAsync(Guid MasterCompanyId, Company company)
    {
        var banks = await _ctx.Banks
            .Where(b => b.MasterCompanyId == MasterCompanyId)
            .ToListAsync();

        company.MasterCompanyId = MasterCompanyId;

        _ctx.Companies.Add(company);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid MasterCompanyId, Company company)
    {
        var existingCompany =
            await _ctx.Companies
                .Where(c =>
                    c.MasterCompanyId == MasterCompanyId &&
                    c.CompanyId == company.CompanyId
                )
                .SingleOrDefaultAsync();

        if (existingCompany == null)
        {
            throw new ArgumentException("Company does not exist.");
        }

        var banks = await _ctx.Banks
            .Where(b => b.MasterCompanyId == MasterCompanyId)
            .ToListAsync();

        existingCompany.BankId = company.BankId;
        _ctx.Companies.Update(existingCompany);
        await _ctx.SaveChangesAsync();
    }

    public async Task<Company> GetAsync(Guid MasterCompanyId, int id)
    {
        var company =
            await _ctx.Companies
                .Include(c => c.Bank)
                .Where(c =>
                    c.MasterCompanyId == MasterCompanyId &&
                    c.CompanyId == id)
                .SingleOrDefaultAsync();

        return company;
    }

    public async Task DeleteAsync(Guid MasterCompanyId, int id)
    {
        _ctx.Remove(new Company
        {
            CompanyId = id,
            MasterCompanyId = MasterCompanyId
        });
        await _ctx.SaveChangesAsync();
    }

    public async Task<PagedResult<Company>> GetPagedAsync(Guid MasterCompanyId, PagingModel pagingModel)
    {
        var query =
            _ctx.Companies
                .Where(cmp =>
                    EF.Functions.ILike(cmp.Name, $"%{pagingModel.SearchTerm}%") ||
                    EF.Functions.ILike(cmp.Address, $"%{pagingModel.SearchTerm}%") ||
                    EF.Functions.ILike(cmp.EDB, $"%{pagingModel.SearchTerm}%") ||
                    EF.Functions.ILike(cmp.City, $"%{pagingModel.SearchTerm}%")
                )
                .Where(cmp => cmp.MasterCompanyId == MasterCompanyId);

        var result = await query
            .Order(pagingModel.SortOrder, pagingModel.SortColumn)
            .Skip(pagingModel.PageSize * pagingModel.Page)
            .Take(pagingModel.PageSize)
            .ToListAsync();

        return new PagedResult<Company>(result, await query.CountAsync());
    }
}