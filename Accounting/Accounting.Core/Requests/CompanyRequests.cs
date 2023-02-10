namespace Accounting.Core.Requests;

public class CompanyGet
{
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string EDB { get; set; }
    public string EMBS { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public int BankId { get; set; }
    public IEnumerable<BankGet> Banks { get; set; }
}

public class CompanyPost
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string EDB { get; set; }
    public string EMBS { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public int BankId { get; set; }
}

public class CompanyPut
{
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string EDB { get; set; }
    public string EMBS { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public int BankId { get; set; }
}