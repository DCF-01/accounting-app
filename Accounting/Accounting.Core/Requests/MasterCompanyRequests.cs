namespace Accounting.Core.Requests;

public class MasterCompanyPost
{
    public string Name { get; set; }
    public bool Active { get; set; }
    public string InitialUserEmail { get; set; }
    public string InitialUserPassword { get; set; }
}

public class MasterCompanyPut
{
    public Guid MasterCompanyId { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
}

public class MasterCompanyGet
{
    public Guid MasterCompanyId { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
    public int UserCount => Users?.Count ?? 0;
    public List<UserGet>? Users { get; set; }
}
