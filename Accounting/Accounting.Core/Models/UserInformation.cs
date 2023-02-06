using System.Security.Claims;
using Accounting.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Accounting.Core.Models;

public class UserInformation : IUserInformation
{
    private ClaimsPrincipal Principal;

    public UserInformation(IHttpContextAccessor httpContextAccessor)
    {
        Principal = httpContextAccessor?.HttpContext?.User;

        MasterCompanyId =
            Guid.TryParse
            (
                Principal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value, out var result
            )
                ? result
                : Guid.Empty;
        IsDebug = Principal?.Claims.Any(x => x.Value.ToLower() == "debug") ?? false;
    }

    public Guid MasterCompanyId { get; set; }
    public bool IsDebug { get; set; }
}