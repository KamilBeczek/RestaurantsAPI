using AutoMapper;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Dtos
{
    public class DishesProfile : Profile
    {
        public DishesProfile() 
        {
            CreateMap<Dish, DishDto>();
        }
        
    }
}
