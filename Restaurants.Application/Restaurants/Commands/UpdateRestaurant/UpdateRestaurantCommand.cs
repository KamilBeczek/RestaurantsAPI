

using MediatR;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommand() : IRequest<bool>
    {
        public int Id { get; set;  }
        public string Name { get; } = default!;
        public string Description { get; } = default!;
        public bool HasDelivery { get; }
    }
}
