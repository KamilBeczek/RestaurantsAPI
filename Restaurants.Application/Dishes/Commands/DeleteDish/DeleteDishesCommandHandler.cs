using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Repositories;


namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishesCommandHandler(ILogger<DeleteDishesCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IDishesRepostiory dishesRepostiory,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteDishesCommand>
    {
        public async Task Handle(DeleteDishesCommand request, CancellationToken cancellationToken)
        {
            logger.LogWarning("removing all dishes from restaurant: {RestuarantId}", request.Restuarantid);

            var restaurant = await restaurantsRepository.GetById(request.Restuarantid);

            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.Restuarantid.ToString());

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
                throw new ForbidExcpetion();

            await dishesRepostiory.Delete(restaurant.Dishes);
        }
    }
}
