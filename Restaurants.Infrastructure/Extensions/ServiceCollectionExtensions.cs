using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("RestaurantsDB");
		services.AddDbContext<RestaurantsDbContext>(options => options.UseNpgsql(connectionString, pgOptions => pgOptions.MigrationsAssembly("Restaurants.Infrastructure")));
	}
}
