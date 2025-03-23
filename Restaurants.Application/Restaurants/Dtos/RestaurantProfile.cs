using AutoMapper;
using Restaurants.Domain.Entities;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantProfile : Profile
{
	public RestaurantProfile()
	{
		CreateMap<CreateRestaurantCommand, Restaurant>()
			.ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
			{
				City = src.City,
				Street = src.Street,
				PostalCode = src.PostalCode
			}));

		CreateMap<UpdateRestaurantCommand, Restaurant>();

		CreateMap<Restaurant, RestaurantDto>()
			.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
			.ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
			.ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
			.ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.Dishes));
	}
}
