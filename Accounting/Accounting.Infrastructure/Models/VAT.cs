namespace Accounting.Infrastructure.Models
{
    public class VAT
    {
        public int VATId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public ICollection<Product> Products { get; set; }
        public Guid MasterCompanyId { get; set; }
        public MasterCompany MasterCompany { get; set; }
    }
}