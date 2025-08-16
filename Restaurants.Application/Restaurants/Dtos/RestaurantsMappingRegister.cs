using Mapster;
using Restaurants.Domain.Entities;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantsMappingRegister : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CreateRestaurantCommand, Restaurant>()
			.Map(dest => dest.Address, src => new Address
			{
				City = src.City,
				Street = src.Street,
				PostalCode = src.PostalCode
			});

		config.NewConfig<UpdateRestaurantCommand, Restaurant>();

		config.NewConfig<Restaurant, RestaurantDto>()
			.Map(dest => dest.City, src => src.Address == null ? null : src.Address.City)
			.Map(dest => dest.Street, src => src.Address == null ? null : src.Address.Street)
			.Map(dest => dest.PostalCode, src => src.Address == null ? null : src.Address.PostalCode)
			.Map(dest => dest.Dishes, src => src.Dishes);
	}
}
