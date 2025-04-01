using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("RestaurantsDB");
		services.AddDbContext<RestaurantsDbContext>(options => options.UseNpgsql(connectionString, pgOptions => pgOptions.MigrationsAssembly("Restaurants.Infrastructure"))
		  .EnableSensitiveDataLogging()
		);

		services.AddIdentityApiEndpoints<User>()
			.AddRoles<IdentityRole>()
			.AddEntityFrameworkStores<RestaurantsDbContext>();

		services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
		services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
		services.AddScoped<IDishRepository, DishRepository>();
	}
}
