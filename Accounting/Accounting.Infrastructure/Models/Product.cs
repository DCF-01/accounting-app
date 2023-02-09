using System.ComponentModel.DataAnnotations.Schema;

namespace Accounting.Infrastructure.Models
{
    public class Product {
        public int ProductId { get; set; }
        public Guid MasterCompanyId { get; set; }
        public MasterCompany MasterCompany { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal WholesalePrice { get; set; }
        public int EAN { get; set; }
        public bool OnSale { get; set; }
        public bool InStock { get; set; }
        public byte[]? Img { get; set; }
        public VAT VAT { get; set; }
        public Currency Currency { get; set; }
        public Spec Spec { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Image>? GalleryImages { get; set; }
        public ICollection<Variation> Variations { get; set; }
        // Helper properties not part of the dto
        [NotMapped]
        public IEnumerable<Spec> Specs { get; set; }
        [NotMapped]
        public ICollection<VAT> VATs { get; set; }
        [NotMapped]
        public IEnumerable<Currency> Currencies { get; set; }
    }
}
