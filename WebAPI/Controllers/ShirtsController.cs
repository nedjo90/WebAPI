using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;
using WebAPI.Filters.ExceptionFilters;
using WebAPI.Models;
using WebAPI.Models.Repositories;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShirtsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetShirts()
    {
        return Ok(ShirtRepository.GetShirts());
    }
    
    [HttpGet("{id}")]
    [Shirt_ValidateShirtIdFilter]
    public IActionResult GetShirtById(int id)
    {
        return Ok(ShirtRepository.GetShirtById(id));
    }

    [HttpPost]
    [Shirt_ValidateCreateShirtFilter]
    public IActionResult CreateShirt([FromBody]Shirt? shirt)
    {
        
        //on l'ajoute à notre list
        ShirtRepository.AddShirt(shirt);
        
        //Par convention dans les web api nous retournons un CreatedAtAction
        return CreatedAtAction(nameof(GetShirtById), new {Id = shirt.ShirtId}, shirt);
    }

    [HttpPut("{id}")]
    [Shirt_ValidateShirtIdFilter]
    [Shirt_ValidateUpdateShirtFilter]
    [Shirt_HandleExceptionsFilter]
    public IActionResult UpdateShirt(int id, Shirt shirt)
    {
         // on met à jour notre donnée
         ShirtRepository.UpdateShirt(shirt);
         //Type de retour conventionnel pour une mise à jour de donnée dans la BDD
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Shirt_ValidateShirtIdFilter]
    public IActionResult DeleteShirt(int id)
    {
        //nous le stockons dans une variable pour l'utiliser dans notre réponse
        Shirt? shirt = ShirtRepository.GetShirtById(id);
        ShirtRepository.DeleteShirt(id);
        return Ok(shirt);
    }
}