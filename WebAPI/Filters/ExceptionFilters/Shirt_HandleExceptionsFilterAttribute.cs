using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Models.Repositories;

namespace WebAPI.Filters.ExceptionFilters;

public class Shirt_HandleExceptionsFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        //on utilise le context pour récupérer les paramètre
        //ici on utilise route data pour récupérer les arguments de la requête
        string? strShirtId = context.RouteData.Values["id"] as string;
        //On le passe en int pour effectuer nos tests
        if (int.TryParse(strShirtId, out int shirtId))
        {
            //on court circuit notre traitement si on rentre dans la condition
            if (!ShirtRepository.ShirtExist(shirtId))
            {
                context.ModelState.AddModelError("ShirtId", "Shirt is doesn't exist anymore");
                //Nous définissons la réponse à la requête pour ce filtre
                ValidationProblemDetails problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status404NotFound
                };
                context.Result = new NotFoundObjectResult(problemDetails);
            }
        }
    }
}