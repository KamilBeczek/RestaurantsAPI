﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurantCommand;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    [Authorize]
    public class RestaruantController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery] GetAllRestaurantsQuery query)
        {
            var restaruants = await mediator.Send(query);
            return Ok(restaruants);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.HasNationality)]
        public async Task<ActionResult<RestaurantDto>> Get([FromRoute] int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "<id claim type<");
            var restaruant = await mediator.Send(new GetRestaurantByIdQuery(id));
            return Ok(restaruant);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Owner)]
        public async Task<IActionResult> CreateRestaurant([FromBody]CreateRestaurantCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }
    }
}
