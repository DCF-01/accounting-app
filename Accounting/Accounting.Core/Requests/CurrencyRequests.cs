namespace Accounting.Core.Requests;

public class CurrencyGet
{
    public int CurrencyId { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
    public int ProductCount { get; set; }
}

public class CurrencyPut
{
    public int CurrencyId { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
}

public class CurrencyPost
{
    public string Name { get; set; }
    public decimal Value { get; set; }
}
