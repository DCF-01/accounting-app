namespace Accounting.Core.Requests
{
    public class GroupBase
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int ProductCount { get; set; }
    }
    
    public class GroupGet : GroupBase
    {
    }
    
    public class GroupPost : GroupBase 
    {
    }
}