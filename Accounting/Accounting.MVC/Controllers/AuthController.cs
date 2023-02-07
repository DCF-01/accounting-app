using System.Security.Claims;
using Accounting.Infrastructure.Data;
using Accounting.Infrastructure.Models;
using Accounting.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.MVC.Controllers;

public class AuthController : BaseController
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _ctx;

    public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext ctx)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _ctx = ctx;
    }
    
    
        [HttpGet]
        public IActionResult Register() {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel) {
            
            var newUser = new User {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email
            };
            
            var res = await _userManager.CreateAsync(newUser, registerViewModel.Password);
            await _userManager.AddToRoleAsync(newUser, "User");

            var user = await _userManager.FindByEmailAsync(newUser.Email);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Sid, user.MasterCompany.MasterCompanyId.ToString()));

            if (res.Succeeded) {

                return RedirectToAction("Index", "Home");
            }
            
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public async Task<IActionResult> Login() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, [FromQuery] string returnUrl) {
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);
            
            if(result.Succeeded)
                return Redirect($"~{returnUrl}");
            return RedirectToAction("Login", "Auth", new { ReturnUrl = returnUrl});
        }

        public async Task<IActionResult> Logout() {

            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ResetPassword()
        {
            return RedirectToAction("ResetPassword", "Auth");
        }
}