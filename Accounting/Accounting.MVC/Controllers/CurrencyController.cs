using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.MVC.Controllers;

[Authorize(Policy = "Users")]
public class CurrencyController : BaseController
{
    private readonly ICurrencyService _currencyService;

    public CurrencyController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
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
    public async Task<IActionResult> Create(CurrencyPost currencyPost)
    {
        await _currencyService.CreateAsync(currencyPost);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Manage(int id)
    {
        var result = await _currencyService.GetAsync(id);
        return View(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Manage(CurrencyPut currencyPut)
    {
        await _currencyService.UpdateAsync(currencyPut);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        await _currencyService.DeleteAsync(id);
        return RedirectToAction("Index");
    }

}