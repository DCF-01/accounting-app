namespace Accounting.Core.Requests;

public class BankAccountGet
{
    public int BankAccountId { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string IBAN { get; set; }
    public int BankId { get; set; }
    public BankGet Bank { get; set; }
}

public class BankAccountPut
{
    public int BankAccountId { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string IBAN { get; set; }
}

public class BankAccountPost
{
    public string Name { get; set; }
    public string Number { get; set; }
    public string IBAN { get; set; }
}


