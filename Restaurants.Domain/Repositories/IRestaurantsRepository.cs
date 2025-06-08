using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantsRepository
{
	Task<IEnumerable<Restaurant>> GetAllAsync();
	Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPhrase);
	Task<Restaurant?> GetByIdAsync(int id);
	Task<int> CreateAsync(Restaurant restaurant);
	Task DeleteAsync(Restaurant restaurant);
	Task SaveChangesAsync(Restaurant restaurant);
}
