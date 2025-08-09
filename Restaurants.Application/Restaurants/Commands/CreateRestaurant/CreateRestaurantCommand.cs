using MediatR;
using Restaurants.Application.Common.Attributes;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

[AuditLog]
public class CreateRestaurantCommand : IRequest<int>
{
	public string Name { get; set; } = default!;
	public string Description { get; set; } = default!;
	public string Category { get; set; } = default!;
	public bool HasDelivery { get; set; } = default!;

	public string? ContactEmail { get; set; }
	public string? ContactNumber { get; set; }

	public string? City { get; set; }
	public string? Street { get; set; }
	public string? PostalCode { get; set; }
}
