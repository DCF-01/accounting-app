namespace Accounting.Infrastructure.Models;

public class Bank
{
    public int BankId { get; set; }
    public string Name { get; set; }
    public string SWIFT { get; set; }
    public ICollection<BankAccount> BankAccounts { get; set; }
    public Guid MasterCompanyId { get; set; }
    public MasterCompany MasterCompany { get; set; }
}