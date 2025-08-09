using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Restaurants.Infrastructure.Persistence;

internal class RestaurantsDbContext(
	DbContextOptions<RestaurantsDbContext> options,
	IUserContext userContext) : IdentityDbContext<User>(options)
{
	internal DbSet<Restaurant> Restaurants { get; set; }
	internal DbSet<Dish> Dishes { get; set; }
	internal DbSet<AuditLog> AuditLogs { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<Restaurant>()
			.OwnsOne(r => r.Address);

		builder.Entity<Restaurant>()
			.HasMany(r => r.Dishes)
			.WithOne()
			.HasForeignKey(d => d.RestaurantId);

		builder.Entity<User>()
			.HasMany(u => u.OwnedRestaurants)
			.WithOne(r => r.Owner)
			.HasForeignKey(r => r.OwnerId);
	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		var currentUser = userContext.GetCurrentUser();
		var userId = currentUser?.Id ?? "Anonymous";

		foreach (var entry in ChangeTracker.Entries<BaseEntity>())
		{
			if (entry.State == EntityState.Added)
			{
				entry.Entity.CreatedAt = DateTime.UtcNow;
				entry.Entity.CreatedBy = userId;
			}

			if (entry.State == EntityState.Modified)
			{
				entry.Entity.LastModifiedByAt = DateTime.UtcNow;
				entry.Entity.LastModifiedBy = userId;
			}
		}

		return await base.SaveChangesAsync(cancellationToken);
	}
}
