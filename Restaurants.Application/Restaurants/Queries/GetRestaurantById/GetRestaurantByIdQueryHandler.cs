using MediatR;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(IRestaurantsRepository restaurantsRepository,
	IMapper mapper,
	ILogger<GetRestaurantByIdQueryHandler> logger
	) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
	public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting restaurant by id : {RestaurantId}", request.Id);
		var restaurant = await restaurantsRepository.GetByIdAsync(request.Id)
			?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
		var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

		return restaurantDto;
	}
}
