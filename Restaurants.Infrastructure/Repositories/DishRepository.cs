using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class DishRepository(RestaurantsDbContext dbContext) : IDishRepository
{
	public async Task<int> CreateAsync(Dish dish)
	{
		dbContext.Dishes.Add(dish);
		await dbContext.SaveChangesAsync();
		return dish.Id;
	}
}
