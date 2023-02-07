namespace Accounting.Infrastructure.Models;

public class Product
{
    public int ProductId { get; set; }
    public Guid MasterCompanyId { get; set; }
    public MasterCompany MasterCompany { get; set; }
    public string SKU { get; set; }
    public string Name { get; set; }
}