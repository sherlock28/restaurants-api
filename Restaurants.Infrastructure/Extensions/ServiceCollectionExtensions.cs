using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Authorization.Constants;
using Restaurants.Infrastructure.Authorization.Requirements;

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
			.AddClaimsPrincipalFactory<RestaurantUserClaimsPrincipalFactory>()
			.AddEntityFrameworkStores<RestaurantsDbContext>();

		services.AddAuthorizationBuilder()
			.AddPolicy(PolicyNames.HasNationality, builder =>
			builder.RequireClaim(AppClaimTypes.Nationality, "Argentinian", "Brazilian"))
			.AddPolicy(PolicyNames.AtLeast20, builder =>
			builder.AddRequirements(new MinimumAgeRequirement(20)));

		services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();

		services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();

		services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
		services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
		services.AddScoped<IDishRepository, DishRepository>();
	}
}
