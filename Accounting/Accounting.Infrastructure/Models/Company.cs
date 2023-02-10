﻿namespace Accounting.Infrastructure.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string EDB { get; set; }
        public string EMBS { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int BankId { get; set; }
        public Bank Bank { get; set; }
        public Guid MasterCompanyId { get; set; }
        public MasterCompany MasterCompany { get; set; }
    }
}