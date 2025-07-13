using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(IRestaurantsRepository restaurantsRepository,
	IMapper mapper,
	ILogger<GetAllRestaurantsQueryHandler> logger
	) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
{
	public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting all restaurants");

		var (restaurants, totalCount) = await restaurantsRepository.GetAllMatchingAsync(
			request.SearchPhrase,
			request.PageSize,
			request.PageNumber,
			request.SortBy,
			request.SortDirection);

		var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

		var result = new PagedResult<RestaurantDto>(restaurantsDto, totalCount, request.PageSize, request.PageNumber);

		return result!;
	}
}
