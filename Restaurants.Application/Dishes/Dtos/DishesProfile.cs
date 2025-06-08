using AutoMapper;
using Restaurants.Domain.Entities;
using Restaurants.Application.Dishes.Commands.CreateDish;

namespace Restaurants.Application.Dishes.Dtos;

public class DishesProfile : Profile
{
	public DishesProfile()
	{
		CreateMap<Dish, DishDto>();
		CreateMap<CreateDishCommand, Dish>();
	}
}
