using Mapster;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Dtos;

public class DishesMappingRegister : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<Dish, DishDto>();

		config.NewConfig<CreateDishCommand, Dish>();
	}
}
