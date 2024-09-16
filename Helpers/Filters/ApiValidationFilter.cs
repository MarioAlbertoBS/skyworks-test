using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SkyworksTest.Helpers.Filters;

public class ApiValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context) {
        var response = new
        {
            Message = "Miau"
        };

        context.Result = new BadRequestObjectResult(response);
    }

    public void OnActionExecuted(ActionExecutedContext context) {

    }
}