
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaruant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Domain;

namespace Restaurants.Application.Dishes.Queries.GetDishForRestaurant
{
    public class GetDishByIdForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQueryHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IMapper mapper) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retriving dishe {dishId} for restuarant with id: {RestaurantId}", 
                request.DishId,
                request.Restuarantid);

            var restuarant = await restaurantsRepository.GetById(request.Restuarantid);

            if (restuarant == null) throw new NotFoundException(nameof(Restaurant), request.Restuarantid.ToString());

            var dish = restuarant.Dishes.FirstOrDefault(d => d.Id == request.Restuarantid);
            if (dish == null) throw new NotFoundException(nameof(Dish), request.DishId.ToString());

            var restuls = mapper.Map<DishDto>(dish);

            return restuls;

        }
    }
}

