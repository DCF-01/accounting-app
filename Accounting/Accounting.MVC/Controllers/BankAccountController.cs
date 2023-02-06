using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.MVC.Controllers;

[Authorize(Policy = "Users")]
public class BankAccountController : BaseController
{
    private readonly IBankAccountService _bankAccountService;

    public BankAccountController(IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(BankAccountPost bankAccountPost)
    {
        await _bankAccountService.CreateAsync(bankAccountPost);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Manage(int id)
    {
        var result = await _bankAccountService.GetAsync(id);
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Manage(BankAccountPut bankAccountPut)
    {
        await _bankAccountService.UpdateAsync(bankAccountPut);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        await _bankAccountService.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}