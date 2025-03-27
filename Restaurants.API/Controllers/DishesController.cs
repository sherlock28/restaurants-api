using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;

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
		int id = await mediator.Send(command);

		//return CreatedAtAction(nameof(GetById), new { id }, null);

		return Created();
	}
}
