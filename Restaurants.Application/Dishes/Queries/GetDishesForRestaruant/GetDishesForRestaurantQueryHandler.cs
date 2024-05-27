using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaruant
{
    public class GetDishesForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQueryHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IMapper mapper): IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
    {
        public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retriving dishes for restuarant with id: {RestaurantId}", request.RestaurantId);
            var restuarant = await restaurantsRepository.GetById(request.RestaurantId);

            if (restuarant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var restuls = mapper.Map<IEnumerable<DishDto>>(restuarant.Dishes);

            return restuls;

        }
    }
}
