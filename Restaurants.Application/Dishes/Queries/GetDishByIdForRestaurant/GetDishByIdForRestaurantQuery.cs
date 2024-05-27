using MediatR;
using Restaurants.Application.Dishes.Dtos;


namespace Restaurants.Application.Dishes.Queries.GetDishForRestaurant
{
    public class GetDishByIdForRestaurantQuery(int restuarantid, int dishId) : IRequest<DishDto>
    {
        public int Restuarantid { get; } = restuarantid;
        public int DishId { get; } = dishId;
    }
}
