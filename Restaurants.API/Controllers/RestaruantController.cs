using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using System.Reflection.Metadata.Ecma335;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaruantController(IRestaurantsService restaurantsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaruants = await restaurantsService.GetAllRestaurants();
            return Ok(restaruants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var restaruant = await restaurantsService.GetById(id);

            if (restaruant is null)
            {
                return NotFound();
            }

            return Ok(restaruant);

        }
    }
}
