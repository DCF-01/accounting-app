using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.MVC.Controllers;

public class MasterCompanyController : BaseController
{
    private readonly ILogger<MasterCompanyController> _logger;
    private readonly IMasterCompanyService _masterCompanyService;

    public MasterCompanyController(ILogger<MasterCompanyController> logger, IMasterCompanyService masterCompanyService)
    {
        _logger = logger;
        _masterCompanyService = masterCompanyService;
    }
    
    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(MasterCompanyPost MasterCompanyPost)
    {
        await _masterCompanyService.CreateAsync(MasterCompanyPost);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Manage(Guid id)
    {
        var masterCompany = await _masterCompanyService.FindByIdAsync(id);

        return View(new MasterCompanyPut
        {
            MasterCompanyId = masterCompany.MasterCompanyId,
            Active = masterCompany.Active,
            Name = masterCompany.Name
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Manage(MasterCompanyPut MasterCompanyPut)
    {
        await _masterCompanyService.UpdateAsync(MasterCompanyPut);

        return RedirectToAction("Index");
    }
}