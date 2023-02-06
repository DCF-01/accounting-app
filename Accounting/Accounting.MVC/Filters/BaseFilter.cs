using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Accounting.MVC.Filters;

public class BaseFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var controller = filterContext.Controller as Controller;
        var controllerDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;

        controller.ViewBag.ControllerName = controllerDescriptor.ControllerName;
        controller.ViewBag.ActionName = controllerDescriptor.ActionName;
    }
}