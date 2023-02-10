using Accounting.Core.Enums;
using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.MVC.Controllers;
 
[Authorize(Policy = "Users")]
public class CompanyController : BaseController
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var result = await _companyService.GetAsync(-1, ViewType.Create);
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CompanyPost companyPost)
    {
        await _companyService.CreateAsync(companyPost);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Manage(int id)
    {
        var result = await _companyService.GetAsync(id, ViewType.Update);
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Manage(CompanyPut companyPut)
    {
        await _companyService.UpdateAsync(companyPut);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        await _companyService.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}