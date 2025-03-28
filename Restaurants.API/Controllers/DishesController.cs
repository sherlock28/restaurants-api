using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDishes;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurant/{restaurantId}/dishes")]
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
