using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes;

public class DeleteDishesForRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository,
	IDishRepository dishRepository,
	ILogger<DeleteDishesForRestaurantCommandHandler> logger) : IRequestHandler<DeleteDishesForRestaurantCommand>
{
	public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
	{
		logger.LogWarning("Removing all dishes from restaurant {restaurantId}", request.RestaurantId);

		var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

		if (restaurant == null)
		{
			throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
		}

		await dishRepository.Delete(restaurant.Dishes);
	}
}
