using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requriments;

public class MinimumRestaurantsRequriment(int NumberOfRestaurants) : IAuthorizationRequirement
{
    public int MinimumNumberOfRestaurants { get; } = NumberOfRestaurants;
}
