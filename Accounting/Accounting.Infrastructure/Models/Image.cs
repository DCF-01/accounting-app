namespace Accounting.Infrastructure.Models;

public class Image
{
    public int ImageId { get; set; }
    public int ProductId { get; set; }
    public byte[] Img { get; set; }
    public Guid MasterCompanyId { get; set; }
    public MasterCompany MasterCompany { get; set; }
}