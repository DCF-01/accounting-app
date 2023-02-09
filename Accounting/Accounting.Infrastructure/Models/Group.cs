using System.ComponentModel.DataAnnotations.Schema;

namespace Accounting.Infrastructure.Models;

public class Group
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    [ForeignKey("ProductId")] public int ProductId { get; set; }
    public ICollection<Product> Products { get; set; }
    [NotMapped] public int ProductCount { get; set; }
    public Guid MasterCompanyId { get; set; }
    public MasterCompany MasterCompany { get; set; }
}