namespace Accounting.MVC.ViewModels;

public class RegisterViewModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsValid { get; }
}