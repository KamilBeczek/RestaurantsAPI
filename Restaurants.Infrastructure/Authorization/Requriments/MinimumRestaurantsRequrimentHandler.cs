using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requriments;

internal class MinimumRestaurantsRequrimentHandler(ILogger<MinimumAgeRequriment> logger,
    IUserContext userContext,
    IRestaurantsRepository restaurantsRepository) : AuthorizationHandler<MinimumRestaurantsRequriment>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantsRequriment requirement)
    {
        var currentUser = userContext.GetCurrentUser();
        var restaurants = await restaurantsRepository.GetAllAsync();
        var userRestaurantsCreated = restaurants.Count(r => r.OwnerId == currentUser!.Id);

        if(userRestaurantsCreated >= requirement.MinimumNumberOfRestaurants)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

    }
}
