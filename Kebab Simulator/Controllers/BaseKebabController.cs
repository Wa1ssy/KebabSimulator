using Kebab_Simulator.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kebab_Simulator.Controllers
{
    public abstract class BaseKebabController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            bool showHealthInspection = HealthInspectionService.ShouldShowHealthInspection(0.2);

            ViewBag.ShowHealthInspection = showHealthInspection;
        }
    }
}
