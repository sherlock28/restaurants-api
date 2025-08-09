using MediatR;
using Restaurants.Application.Common.Attributes;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes;

[AuditLog]
public class DeleteDishesForRestaurantCommand(int restaurantId) : IRequest
{
	public int RestaurantId { get; } = restaurantId;
}
