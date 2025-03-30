using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(IRestaurantsRepository restaurantsRepository,
	IMapper mapper,
	ILogger<GetAllRestaurantsQueryHandler> logger
	) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
{
	public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting all restaurants");
		var restaurants = await restaurantsRepository.GetAllAsync();

		var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

		return restaurantsDto!;
	}
}
