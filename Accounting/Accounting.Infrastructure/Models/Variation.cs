namespace Accounting.Infrastructure.Models;

public class Variation
{
    public int VariationId { get; set; }
    public string Name { get; set; }
    public string[] Options { get; set; }
    public int ProductId { get; set; }
    public ICollection<Product> Products { get; set; }
    public Guid MasterCompanyId { get; set; }
    public MasterCompany MasterCompany { get; set; }
}