﻿using Restaurants.Application.Dishes.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Restaurants.Application.Restaurants.Dtos
{
    public class CreateRestaurantDto
    {
            public string Name { get; set; } = default!;
            public string Description { get; set; } = default!;
            public string Category { get; set; } = default!;
            public bool HasDelivery { get; set; }
            public string? ContactEmail { get; set; }
            public string? ContactNumber { get; set; }
            public string? City { get; set; }
            public string? Street { get; set; }
            public string? PostalCode { get; set; }
    }
}
