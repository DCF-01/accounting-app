namespace Accounting.Infrastructure.Models;

public class Spec {
    public int SpecId { get; set; }
    public string Name { get; set; }
    public string[] First { get; set; }
    public string[] Rest { get; set; }
    public int ItemsPerRow { get; set; }
    public int ProductId { get; set; }
    public ICollection<Product> Products { get; set; }
    public Guid MasterCompanyId { get; set; }
    public MasterCompany MasterCompany { get; set; }
}

