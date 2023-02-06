using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Accounting.Infrastructure.Models;

public class User : IdentityUser
{
    [NotMapped]
    public List<string> Claims { get; set; }
    public Guid MasterCompanyId { get; set; }
    public MasterCompany MasterCompany { get; set; }
}