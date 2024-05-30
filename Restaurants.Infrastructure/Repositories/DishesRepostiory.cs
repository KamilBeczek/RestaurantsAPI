
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Presistence;

namespace Restaurants.Infrastructure.Repositories
{
    public class DishesRepostiory(RestaurantsDbContext dbContext) : IDishesRepostiory
    {
        public async Task<int> Create(Dish entity)
        {
            dbContext.Dishes.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(IEnumerable<Dish> entities)
        {
            dbContext.Dishes.RemoveRange(entities);
            await dbContext.SaveChangesAsync();
        }
    }
}
