namespace Accounting.Core.Requests;

public class VATGet
{
    public int VATId { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
}

public class VATPost
{
    public string Name { get; set; }
    public decimal Value { get; set; }
}

public class VATPut
{
    public int VATId { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
}