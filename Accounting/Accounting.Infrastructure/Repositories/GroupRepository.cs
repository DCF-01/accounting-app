using Accounting.Common;
using Accounting.Infrastructure.Data;
using Accounting.Infrastructure.Extensions;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly ApplicationDbContext _ctx;

    public GroupRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<PagedResult<Group>> GetPagedAsync(Guid masterCompanyId, PagingModel pagingModel)
    {
        var query =
            _ctx.Groups
                .Where(grp =>
                    grp.MasterCompanyId == masterCompanyId &&
                    grp.Name.Contains(pagingModel.SearchTerm) &&
                    grp.MasterCompanyId == masterCompanyId
                );

        var result =
            await query
                .Select(grp => new Group
                {
                    GroupId = grp.GroupId,
                    Name = grp.Name,
                    ProductCount = grp.Products.Count
                })
                .Order(pagingModel.SortOrder, pagingModel.SortColumn)
                .Skip(pagingModel.PageSize * pagingModel.Page)
                .Take(pagingModel.PageSize)
                .ToListAsync();

        return new PagedResult<Group>(result, await query.CountAsync());
    }

    public async Task CreateAsync(Guid masterCompanyId, Group group)
    {
        group.MasterCompanyId = masterCompanyId;
        _ctx.Groups.Add(group);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid masterCompanyId, int groupId)
    {
        var group =
            await _ctx.Groups
                .Where(x =>
                    x.GroupId == groupId &&
                    x.MasterCompanyId == masterCompanyId
                )
                .FirstOrDefaultAsync();

        if (group != null)
        {
            _ctx.Remove(group);
            await _ctx.SaveChangesAsync();
        }
    }
}