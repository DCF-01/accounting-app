using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.MVC.Controllers;
[Authorize(Policy = "Users")]
public class VATController : BaseController
{
    private readonly IVATService _vatService;

    public VATController(IVATService vatService)
    {
        _vatService = vatService;
    }

    public IActionResult Index()
    {
        return View("Index");
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(VATPost vatPost)
    {
        await _vatService.CreateAsync(vatPost);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Manage(int id)
    {
        return View(await _vatService.GetAsync(id));
    }


    [HttpPost]
    public async Task<IActionResult> Manage(VATPut vatPut)
    {
        await _vatService.UpdateAsync(vatPut);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _vatService.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}