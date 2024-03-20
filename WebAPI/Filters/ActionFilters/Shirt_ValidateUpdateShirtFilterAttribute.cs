using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Models;

namespace WebAPI.Filters;

public class Shirt_ValidateUpdateShirtFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        //base.OnActionExecuting(context);
        int? id = context.ActionArguments["id"] as int?;
        Shirt? shirt = context.ActionArguments["shirt"] as Shirt;
        if (id.HasValue && shirt != null && id.Value != shirt.ShirtId)
        {
            context.ModelState.AddModelError("ShirtId", "ShirtId is not the same as id");
            //Nous définissons la réponse à la requête pour ce filtre
            ValidationProblemDetails problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetails);
        }
    }
}