using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var restaurants = await restaurantsService.GetAllRestaurants();
		return Ok(restaurants);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById([FromRoute] int id)
	{
		var restaurant = await restaurantsService.GetRestaurantById(id);
		if (restaurant is null)
			return NotFound();

		return Ok(restaurant);
	}

	[HttpPost]
	public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto dto)
	{
		int id = await restaurantsService.Create(dto);

		return CreatedAtAction(nameof(GetById), new { id }, null);
	}
}
