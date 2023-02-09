namespace Accounting.Core.Requests;

public class SpecGet
{
    public int SpecId { get; set; }
    public string Name { get; set; }
    public int ProductId { get; set; }
}

public class SpecPost
{
    public string Name { get; set; }
    public string[] First { get; set; }
    public string[] Rest { get; set; }
    public int ItemsPerRow { get; set; }
}

public class SpecPut
{
    public string Name { get; set; }
    public string[] First { get; set; }
    public string[] Rest { get; set; }
    public int ItemsPerRow { get; set; }
}
