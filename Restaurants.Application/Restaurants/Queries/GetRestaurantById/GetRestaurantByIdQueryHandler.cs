
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
    {
        public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting {RestaurantId} restaurant", request);
            var restaurant = await restaurantsRepository.GetById(request.Id) 
                ?? throw new NotFoundException($"Restaurant", request.Id.ToString());
            var restaurantsDto = mapper.Map<RestaurantDto>(restaurant);

            return restaurantsDto;
        }
    }
}
