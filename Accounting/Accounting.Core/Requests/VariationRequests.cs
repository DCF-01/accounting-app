namespace Accounting.Core.Requests;

public class VariationGet
{
    public int VariationId { get; set; }
    public string Name { get; set; }
    public string[] Options { get; set; }
    public int ProductId { get; set; }
}

public class VariationPost
{
    public string Name { get; set; }
    public string[] Options { get; set; }
}

public class VariationPut
{
    public int VariationId { get; set; }
    public string Name { get; set; }
    public string[] Options { get; set; }
}