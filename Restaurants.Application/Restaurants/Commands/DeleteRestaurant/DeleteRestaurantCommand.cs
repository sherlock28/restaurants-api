using MediatR;
using Restaurants.Application.Common.Attributes;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

[AuditLog]
public class DeleteRestaurantCommand(int id) : IRequest
{
	public int Id { get; } = id;
}
