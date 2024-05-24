
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Repositories
{
    public interface IDishesRepostiory
    {
        Task<int> Create(Dish entity);
    }
}
