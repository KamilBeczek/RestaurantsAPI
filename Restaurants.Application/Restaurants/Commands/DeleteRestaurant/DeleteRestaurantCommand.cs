

using MediatR;
using System.Windows.Input;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurantCommand
{
    public class DeleteRestaurantCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
