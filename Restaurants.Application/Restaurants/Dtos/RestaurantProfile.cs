using AutoMapper;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantProfile : Profile
{
	public RestaurantProfile()
	{
		CreateMap<Restaurant, RestaurantDto>()
			.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
			.ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
			.ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
			.ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.Dishes));
	}
}
