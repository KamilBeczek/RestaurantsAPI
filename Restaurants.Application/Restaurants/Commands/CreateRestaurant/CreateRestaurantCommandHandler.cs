using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository,
        IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();
            logger.LogInformation("{UserEmail} [{UserId}] Getting a new {@Restaurant}", 
                currentUser.Email,
                currentUser.Id,
                request);
            var restuarant = mapper.Map<Restaurant>(request);
            restuarant.OwnerId = currentUser.Id;
            int id = await restaurantsRepository.Create(restuarant);

            return id;
        }
    }
}
