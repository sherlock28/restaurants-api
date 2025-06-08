using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(
	IRestaurantsRepository restaurantsRepository,
	IDishRepository dishRepository,
	IRestaurantAuthorizationService restaurantAuthorizationService,
	IMapper mapper,
	ILogger<CreateDishCommandHandler> logger) : IRequestHandler<CreateDishCommand, int>
{
	public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Creating a new dish {@dish}", request);

		var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

		if (restaurant is null)
			throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

		if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
			throw new ForbidException();

		var dish = mapper.Map<Dish>(request);

		var id = await dishRepository.CreateAsync(dish);

		return id;
	}
}
