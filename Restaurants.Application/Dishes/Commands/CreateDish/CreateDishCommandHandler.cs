using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(IRestaurantsRepository restaurantsRepository,
	IDishRepository dishRepository,
	IMapper mapper,
	ILogger<CreateDishCommandHandler> logger) : IRequestHandler<CreateDishCommand, int>
{
	public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Creating a new dish {@dish}", request);

		var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

		if (restaurant is null)
		{
			throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
		}

		var dish = mapper.Map<Dish>(request);

		var id = await dishRepository.CreateAsync(dish);

		return id;
	}
}
