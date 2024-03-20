using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Models;
using WebAPI.Models.Repositories;

namespace WebAPI.Filters;

public class Shirt_ValidateCreateShirtFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        Shirt? shirt = context.ActionArguments["shirt"] as Shirt;
        if (shirt == null)
        {
            //Ici on identifie la propriété de notre modèle qui est n'est pas valide ainsi que le message que l'on souhaite retourner
            context.ModelState.AddModelError("Shirt", "Shirt is null");
            
            //Nous définissons la réponse à la requête pour ce filtre
            ValidationProblemDetails problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetails);
        }
        else
        {
            //On vérifie s'il n'existe pas dans notre base de donnée pour éviter le doublon
            Shirt? existingShirt = ShirtRepository.GetShirtProperties
                (shirt.Brand, shirt.Color, shirt.Gender, shirt.Size);
            if (existingShirt != null)
            {
                //Ici on identifie la propriété de notre modèle qui est n'est pas valide ainsi que le message que l'on souhaite retourner
                context.ModelState.AddModelError("Shirt", "Shirt already exist");
            
                //Nous définissons la réponse à la requête pour ce filtre
                ValidationProblemDetails problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
        
    }
}