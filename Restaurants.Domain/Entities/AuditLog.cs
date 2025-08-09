using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.Domain.Entities;

public class AuditLog
{
	public Guid Id { get; set; } = Guid.NewGuid();

	public string EventType { get; set; } = null!;
	public string? UserId { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	[Column(TypeName = "jsonb")]
	public string Data { get; set; } = null!;
}
