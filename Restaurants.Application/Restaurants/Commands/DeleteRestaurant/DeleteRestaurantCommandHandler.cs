using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository,
	ILogger<DeleteRestaurantCommandHandler> logger) : IRequestHandler<DeleteRestaurantCommand, bool>
{
	public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Deleting restaurant with id : {id}", request.Id);
		var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

		if (restaurant is null)
		{
			logger.LogWarning("Restaurant with id : {id} not found", request.Id);
			return false;
		}

		await restaurantsRepository.DeleteAsync(restaurant);
		return true;
	}
}
