using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishesCommand(int restuarantid) : IRequest
    {
        public int Restuarantid { get; } = restuarantid;
    }
}
