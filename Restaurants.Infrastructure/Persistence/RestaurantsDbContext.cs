using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence;

internal class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : IdentityDbContext<User>(options)
{
	internal DbSet<Restaurant> Restaurants { get; set; }
	internal DbSet<Dish> Dishes { get; set; }

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
}
