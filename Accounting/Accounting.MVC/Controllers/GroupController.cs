using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.MVC.Controllers;

[Authorize(Policy = "Users")]
public class GroupController : BaseController
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpGet]
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
    public async Task<IActionResult> Create(GroupPost groupPost)
    {
        await _groupService.CreateAsync(groupPost);
        return RedirectToAction("Index");
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _groupService.DeleteAsync(id);

            return Ok($"Item with Id: {id} deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}