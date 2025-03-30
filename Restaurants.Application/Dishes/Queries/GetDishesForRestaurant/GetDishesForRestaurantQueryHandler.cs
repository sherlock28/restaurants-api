using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restaurants.Application.Dishes.Dtos;
using AutoMapper;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQueryHandler(IRestaurantsRepository restaurantsRepository,
	IMapper mapper,
	ILogger<GetDishesForRestaurantQueryHandler> logger) : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
{
	public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting dishes for restaurant with id {RestaurantId}", request.RestaurantId);

		var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

		if (restaurant == null)
		{
			throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
		}

		var dishes = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);

		return dishes;
	}
}
