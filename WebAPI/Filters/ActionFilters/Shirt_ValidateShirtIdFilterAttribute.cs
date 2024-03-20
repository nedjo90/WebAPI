using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Models;
using WebAPI.Models.Repositories;

namespace WebAPI.Filters;

public class Shirt_ValidateShirtIdFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        //Ici il nous faut les arguments envoyés en paramètre de la méthode pour pouvoir les traiter
        //en l'occurence un entier nullable
        int? shirtId = context.ActionArguments["id"] as int?;
        if (shirtId.HasValue)
        {
            if (shirtId <= 0)
            {
                //Ici on identifie la propriété de notre modèle qui est n'est pas valide ainsi que le message que l'on souhaite retourner
                context.ModelState.AddModelError("ShirtId", "ShirtId is invald");
                //Nous définissons la réponse à la requête pour ce filtre
                ValidationProblemDetails problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
            //Si l'id n'existe pas dans notre liste de donnée nous souhaitons également court circuité la pipeline
            else if (!ShirtRepository.ShirtExist(shirtId.Value))
            {
                //Ici on identifie la propriété de notre modèle qui est n'est pas valide ainsi que le message que l'on souhaite retourner
                context.ModelState.AddModelError("ShirtId", "ShirtId doesn't exist");
                ValidationProblemDetails problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status404NotFound
                };
                //Nous définissons la réponse à la requête pour ce filtre
                context.Result = new NotFoundObjectResult(problemDetails);   
            }
        }
        
    }
}