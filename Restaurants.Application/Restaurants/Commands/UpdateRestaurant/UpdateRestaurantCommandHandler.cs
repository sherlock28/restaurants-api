using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(
	IRestaurantsRepository restaurantsRepository,
	IMapper mapper,
	ILogger<UpdateRestaurantCommand> logger) : IRequestHandler<UpdateRestaurantCommand, bool>
{
	public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Updating restaurant with id : {id}", request.Id);
		var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

		if (restaurant is null)
		{
			logger.LogWarning("Restaurant with id : {id} not found", request.Id);
			return false;
		}

		mapper.Map(request, restaurant);

		await restaurantsRepository.SaveChangesAsync(restaurant);

		return true;
	}
}
