using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence;

internal class RestaurantsDbContext : DbContext
{
	internal DbSet<Restaurant> Restaurants { get; set; }
	internal DbSet<Dish> Dishes { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql("Server=localhost;port=5432;username=postgres;password=pgpass;database=Restaurants");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<Restaurant>()
			.OwnsOne(r => r.Address);
		modelBuilder.Entity<Restaurant>()
		.HasMany(r => r.Dishes)
		.WithOne()
		.HasForeignKey(d => d.RestaurantId);
	}
}

internal static partial class IServiceCollectionExtensions
{
	public static IServiceCollection UseRestaurantsDbContext(this IServiceCollection services, IConfiguration config) => services
		.AddDbContext<RestaurantsDbContext>(options => options
			.UseNpgsql(config.GetConnectionString("DefaultConnection"), pgOptions => pgOptions
				.MigrationsAssembly("Restaurants.Infrastructure")));
}
