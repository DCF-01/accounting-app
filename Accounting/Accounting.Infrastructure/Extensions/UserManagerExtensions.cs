using System.Security.Claims;
using Accounting.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Extensions;

public static class UserManagerExtensions
{
    public static async Task UpdateClaimsAsync(this UserManager<User> userManager, User user,
        List<Claim> newClaims)
    {
        try
        {
            var existingClaims = await userManager.GetClaimsAsync(user);

            await userManager.RemoveClaimsAsync(user, existingClaims);
            await userManager.AddClaimsAsync(user, newClaims);
        }
        catch (Exception e)
        {
            throw new DbUpdateException($"There was an error while updating user claims. {e.Message}");
        }
    }
}