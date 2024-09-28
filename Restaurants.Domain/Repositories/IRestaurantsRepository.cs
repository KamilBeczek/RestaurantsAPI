using Restaurants.Domain.Common;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetById(int id);
        Task<int> Create(Restaurant entity);
        Task Delete(Restaurant entity);
        Task Update();
        Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    }
}
