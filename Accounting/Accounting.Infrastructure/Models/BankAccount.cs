namespace Accounting.Infrastructure.Models;

public class BankAccount
{
    public int BankAccountId { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string IBAN { get; set; }
    public int? BankId { get; set; }
    public Bank? Bank { get; set; }
    public Guid MasterCompanyId { get; set; }
    public MasterCompany MasterCompany { get; set; }
}