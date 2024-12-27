using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepository restaurantsRepository, IMapper mapper, ILogger<RestaurantsService> logger) : IRestaurantsService
{
	public async Task<int> Create(CreateRestaurantDto dto)
	{
		logger.LogInformation("Creating a new restaurant");
		var restaurant = mapper.Map<Restaurant>(dto);

		int id = await restaurantsRepository.CreateAsync(restaurant);

		return id;
	}

	public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
	{
		logger.LogInformation("Getting all restaurants");
		var restaurants = await restaurantsRepository.GetAllAsync();

		var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

		return restaurantsDto!;
	}

	public async Task<RestaurantDto?> GetRestaurantById(int id)
	{
		logger.LogInformation("Getting restaurant by id {id}", id);
		var restaurant = await restaurantsRepository.GetByIdAsync(id);
		var restaurantDto = mapper.Map<RestaurantDto?>(restaurant);

		return restaurantDto;
	}
}

