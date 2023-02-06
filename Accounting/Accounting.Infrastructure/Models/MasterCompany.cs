namespace Accounting.Infrastructure.Models;

public class MasterCompany
{
    public Guid MasterCompanyId { get; set; }
    public string Name { get; set; }
    public ICollection<User>? Users { get; set; }
    public bool Active { get; set; }
}