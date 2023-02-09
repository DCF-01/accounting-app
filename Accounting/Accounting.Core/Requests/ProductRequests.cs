namespace Accounting.Core.Requests;

public class ProductGet
{
    public int ProductId { get; set; }
    public string SKU { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public decimal RetailPrice { get; set; }
    public decimal WholesalePrice { get; set; }
    public bool OnSale { get; set; }
    public bool InStock { get; set; }
    public SpecGet Spec { get; set; }
    public VATGet VAT { get; set; }
    public CurrencyGet Currency { get; set; }
    public List<GroupGet> Groups { get; set; } = new();
    public List<VariationGet> Variations { get; set; } = new();
    public List<CurrencyGet> Currencies { get; set; } = new();
    public List<VATGet> VATS { get; set; } = new();
    public List<SpecGet> Specs { get; set; } = new();
    public Guid MasterCompanyId { get; set; }
}

public class ProductPost
{
    public string SKU { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public decimal RetailPrice { get; set; }
    public decimal WholesalePrice { get; set; }
    public bool OnSale { get; set; }
    public bool InStock { get; set; }
    public int SpecId { get; set; }
    public int VATId { get; set; }
    public int CurrencyId { get; set; }
    public List<int> GroupIds { get; set; }
    public List<int> VariationIds { get; set; }
    public Guid MasterCompanyId { get; set; }
    public byte[]? CompressedProductImage { get; set; }
}

public class ProductPut
{
    public int ProductId { get; set; }
    public string SKU { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public decimal RetailPrice { get; set; }
    public decimal WholesalePrice { get; set; }
    public bool OnSale { get; set; }
    public bool InStock { get; set; }
    public int SpecId { get; set; }
    public int VATId { get; set; }
    public int CurrencyId { get; set; }
    public List<int> GroupIds { get; set; }
    public List<int> VariationIds { get; set; }
    public Guid MasterCompanyId { get; set; }
    public byte[]? CompressedProductImage { get; set; }
}