namespace Accounting.Core.Requests;

public class UserGet
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public List<string> Claims { get; set; }
}

public class UserPost
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<string> Claims { get; set; }
}

public class UserPut
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<string> Claims { get; set; }
}