using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDishes;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Infrastructure.Authorization.Constants;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurant/{restaurantId}/dishes")]
[Authorize]
public class DishesController(IMediator mediator) : ControllerBase
{
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, [FromBody] CreateDishCommand command)
	{
		command.RestaurantId = restaurantId;
		int dishId = await mediator.Send(command);

		return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, dishId }, null);
	}

	[HttpGet]
	[Authorize(Policy = PolicyNames.AtLeast20)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
	{
		var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));

		return Ok(dishes);
	}

	[HttpGet("{dishId}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
	{
		var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));

		return Ok(dish);
	}

	[HttpDelete]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> DeleteDishesForRestaurant([FromRoute] int restaurantId)
	{
		await mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));

		return NoContent();
	}
}
