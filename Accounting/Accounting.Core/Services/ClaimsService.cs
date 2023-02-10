using Accounting.Core.Enums;

namespace Accounting.Core.Services;

public class ClaimsService
{
    private List<string> _claims;

    public ClaimsService()
    {
        _claims = Enum.GetNames<RoleClaim>().ToList();
    }

    public List<string> GetClaims => _claims;
}