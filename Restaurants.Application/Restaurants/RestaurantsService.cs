using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantsService(IRestaurantsRepository restaurantsRepository, 
        ILogger<RestaurantsService> logger,
        IMapper mapper) : IRestaurantsService
    {
        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            logger.LogInformation("Getting all restaurants");
            var restaurants = await restaurantsRepository.GetAllAsync();
            var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            return restaurantsDto!;
        }

        public async Task<RestaurantDto?> GetById(int id)
        {
            logger.LogInformation($"Getting {id} restaurant");
            var restaurant = await restaurantsRepository.GetById(id);
            var restaurantsDto = mapper.Map<RestaurantDto>(restaurant);

            return restaurantsDto;
        }

        public async Task<int> Create(CreateRestaurantDto dto)
        {
            logger.LogInformation($"Getting a new restaurant");
            var restuarant = mapper.Map<Restaurant>(dto);
            int id = await restaurantsRepository.Create(restuarant);

            return id;
        }

    }
}
