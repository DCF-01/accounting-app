using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Accounting.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.MVC.Controllers
{
    [Authorize(Policy = "Users")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ClaimsService _claimsService;

        public UserController(IUserService userService, ClaimsService claimsService)
        {
            _userService = userService;
            _claimsService = claimsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteAsync(id);
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Manage(string id)
        {
            ViewBag.Claims = _claimsService.GetClaims;
            var user = await _userService.GetAsync(id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(string id, UserPut userPut)
        {
            try
            {
                await _userService.UpdateAsync(userPut);
                
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Create", "Users", new { error = $"{e.Message}", trace = e });
            }
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.Claims = _claimsService.GetClaims;
                return View();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserPost userPost)
        {
            try
            {
                await _userService.CreateAsync(userPost);
                
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Create", "Users", new { error = $"{e.Message}", trace = e });
            }
        }
    }
}