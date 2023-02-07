using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.MVC.Controllers;

[Authorize(Policy = "Users")]
public class BankController : BaseController
{
    private readonly IBankService _bankService;

    public BankController(IBankService bankService)
    {
        _bankService = bankService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async  Task<IActionResult> Create()
    {
        var bank = await _bankService.GetAsync();
        return View(bank);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(BankPost bankPost)
    {
        await _bankService.CreateAsync(bankPost);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Manage(int id)
    {
        return View(await _bankService.GetAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Manage(BankPut bankPut)
    {
        await _bankService.UpdateAsync(bankPut);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        await _bankService.DeleteAsync(id);
        return RedirectToAction("Index");
    }


}