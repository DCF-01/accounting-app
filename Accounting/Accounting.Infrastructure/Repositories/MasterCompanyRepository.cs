using Accounting.Common;
using Accounting.Infrastructure.Data;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Accounting.Infrastructure.Extensions;

namespace Accounting.Infrastructure.Repositories;

public class MasterCompanyRepository : IMasterCompanyRepository
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _ctx;

    public MasterCompanyRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
        ApplicationDbContext ctx)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _ctx = ctx;
    }

    public async Task CreateAsync(string name, bool active, string initialUserEmail, string initialUserPassword)
    {
        var newMasterCompany = new MasterCompany
        {
            Active = active,
            Name = name
        };

        _ctx.MasterCompanies.Add(newMasterCompany);
        await _ctx.SaveChangesAsync();

        var initialUser = new User()
        {
            UserName = initialUserEmail.Split('@')[0],
            Email = initialUserEmail,
            MasterCompany = newMasterCompany
        };

        await _userManager.CreateAsync(initialUser, initialUserPassword);
    }

    public async Task AddChildUsers(Guid masterCompanyId, List<string> childUserIds)
    {
        var updatedMasterCompany = new MasterCompany
        {
            MasterCompanyId = masterCompanyId,
            Users = await _ctx.Users.Where(u => childUserIds.Contains(u.Id)).ToListAsync()
        };

        _ctx.MasterCompanies
            .Attach(updatedMasterCompany);

        await _ctx.SaveChangesAsync();
    }

    public async Task<PagedResult<MasterCompany>> GetPagedAsync(Guid MasterCompanyId, PagingModel pagingModel)
    {
        var MasterCompanysQueryFilter =
            _ctx.MasterCompanies
                .Include(mu => mu.Users)
                .Where(mu =>
                    mu.Name.Contains(pagingModel.SearchTerm) &&
                    mu.MasterCompanyId == MasterCompanyId
                );

        var result =
            await MasterCompanysQueryFilter
                .Order(pagingModel.SortOrder, pagingModel.SortColumn)
                .Skip(pagingModel.Page * pagingModel.PageSize)
                .Take(pagingModel.PageSize)
                .ToListAsync();

        return new PagedResult<MasterCompany>(result, await MasterCompanysQueryFilter.CountAsync());
    }

    public async Task<MasterCompany> FindByIdAsync(Guid MasterCompanyId)
    {
        return await _ctx.MasterCompanies.FindAsync(MasterCompanyId);
    }

    public async Task UpdateAsync(Guid MasterCompanyId, string name, bool active)
    {
        var MasterCompany = await _ctx.MasterCompanies.FindAsync(MasterCompanyId);

        if (MasterCompany != null)
        {
            MasterCompany.Name = name;
            MasterCompany.Active = active;

            await _ctx.SaveChangesAsync();
        }
    }
}