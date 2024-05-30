using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Repositories;


namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishesCommandHandler(ILogger<DeleteDishesCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IDishesRepostiory dishesRepostiory) : IRequestHandler<DeleteDishesCommand>
    {
        public async Task Handle(DeleteDishesCommand request, CancellationToken cancellationToken)
        {
            logger.LogWarning("removing all dishes from restaurant: {RestuarantId}", request.Restuarantid);

            var restaurant = await restaurantsRepository.GetById(request.Restuarantid);
            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.Restuarantid.ToString());

            await dishesRepostiory.Delete(restaurant.Dishes);
        }
    }
}
