using MediatR;
using Restaurants.Application.Common.Attributes;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

[AuditLog]
public class CreateDishCommand : IRequest<int>
{
	public string Name { get; set; } = default!;
	public string Description { get; set; } = default!;
	public decimal Price { get; set; }

	public int? KiloCalories { get; set; }

	public int RestaurantId { get; set; }
}
