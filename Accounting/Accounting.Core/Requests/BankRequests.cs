namespace Accounting.Core.Requests;

public class BankGet
{
    public int BankId { get; set; }
    public string Name { get; set; }
    public string SWIFT { get; set; }
    public int BankAccountsCount { get; set; }
    public int CompanyId { get; set; }
    public List<int> BankAccountIds { get; set; }
    public List<BankAccountGet> BankAccounts { get; set; }
}

public class BankPost
{
    public string Name { get; set; }
    public string SWIFT { get; set; }
    public List<int> BankAccountIds { get; set; }
}

public class BankPut
{
    public int BankId { get; set; }
    public string Name { get; set; }
    public string SWIFT { get; set; }
    public List<int> BankAccountIds { get; set; }
}