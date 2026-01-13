using MediatR;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Application.Users;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository,
	IMapper mapper,
	IUserContext userContext,
	ILogger<CreateRestaurantCommandHandler> logger
	) : IRequestHandler<CreateRestaurantCommand, int>
{
	public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
	{
		var currentUser = userContext.GetCurrentUser();

		logger.LogInformation("{UserEmail} [{UserId}] is creating a new restaurant {@Restaurant}", currentUser?.Email, currentUser?.Id, request);

		var restaurant = mapper.Map<Restaurant>(request);
		restaurant.OwnerId = currentUser?.Id!;

		int id = await restaurantsRepository.CreateAsync(restaurant);

		return id;
	}
}
