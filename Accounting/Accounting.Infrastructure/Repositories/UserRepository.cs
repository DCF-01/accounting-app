using System.Security.Claims;
using Accounting.Common;
using Accounting.Infrastructure.Data;
using Accounting.Infrastructure.Extensions;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _ctx;
    private readonly UserManager<User> _userManager;

    public UserRepository(ApplicationDbContext ctx, UserManager<User> userManager)
    {
        _ctx = ctx;
        _userManager = userManager;
    }


    public async Task<PagedResult<User>> GetPagedAsync(Guid masterCompanyId, PagingModel pagingModel)
    {
        //_ctx.UserClaims
        var users = _ctx.Users
            .Where(user =>
                user.UserName.ToLower().StartsWith(pagingModel.SearchTerm) ||
                user.Email.ToLower().StartsWith(pagingModel.SearchTerm)
            )
            .Where(user => user.MasterCompanyId == masterCompanyId);

        var result =
            await users
                .Select(user => new User()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Claims = _ctx.UserClaims
                        .Where
                        (c =>
                            c.UserId == user.Id &&
                            c.ClaimType == ClaimTypes.Role
                        )
                        .Select(c => c.ClaimValue).ToList()
                })
                .Order(pagingModel.SortOrder, pagingModel.SortColumn)
                .Skip(pagingModel.Page)
                .Take(pagingModel.PageSize)
                .ToListAsync();

        return new PagedResult<User>(result, await users.CountAsync());
    }

    public async Task CreateAsync(Guid masterCompanyId, string password, User applicationUser)
    {
        var user =
            await _ctx.Users
                .Where(u =>
                    u.Email == applicationUser.Email &&
                    u.MasterCompanyId == masterCompanyId
                )
                .FirstOrDefaultAsync();
        
        var userClaims = applicationUser.Claims.Select(claim => new Claim(ClaimTypes.Role, claim)).ToList();

        if (user != default) throw new ArgumentException("User already exists.");

        //set MasterCompanyId
        await _userManager.CreateAsync(applicationUser, password);
        await _userManager.AddClaimsAsync(applicationUser, userClaims);
    }

    public async Task UpdateAsync(Guid masterCompanyId, User applicationUser)
    {
        var user = await _ctx.Users
            .Where(u =>
                u.Id == applicationUser.Id && 
                u.MasterCompanyId == masterCompanyId
            ).FirstOrDefaultAsync();

        if (user == default) throw new ArgumentException($"User does not exist. UserId: {applicationUser.Id}");

        var newClaims =
            applicationUser.Claims
                .Select(claim => new Claim(ClaimTypes.Role, claim)).ToList();

        newClaims
            .Add(new Claim(ClaimTypes.Sid, masterCompanyId.ToString()));

        await _userManager.UpdateClaimsAsync(user, newClaims);
    }

    public async Task<User> GetAsync(Guid masterCompanyId, string userId)
    {
        var user =
            await _ctx.Users
                .Where(u =>
                    u.Id == userId &&
                    u.MasterCompanyId == masterCompanyId
                )
                .Select(u =>
                    new User()
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        Email = u.Email,
                        EmailConfirmed = u.EmailConfirmed,
                        Claims = _ctx.UserClaims
                            .Where(c => c.UserId == u.Id)
                            .Select(c => c.ClaimValue)
                            .ToList()
                    })
                .FirstOrDefaultAsync();

        return user;
    }

    public async Task DeleteAsync(Guid masterCompanyId, string userId)
    {
        var user =
            await _ctx.Users
                .Where(u =>
                    u.Id == userId &&
                    u.MasterCompanyId == masterCompanyId)
                .FirstOrDefaultAsync();

        if (user != default)
            await _userManager.DeleteAsync(user);
    }
}