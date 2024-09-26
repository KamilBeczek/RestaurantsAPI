using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requriments;

internal class MinimumAgeRequrimentHandler(ILogger<MinimumAgeRequriment> logger,
    IUserContext userContext) : AuthorizationHandler<MinimumAgeRequriment>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequriment requirement)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("User: {Email}, date of birth {DoB} - Handling MinimAgeReuqirment",
            currentUser.Email,
            currentUser.DateOfBirth);

        if(currentUser.DateOfBirth == null)
        {
            logger.LogWarning("User date of birth is null");
            context.Fail();
            return Task.CompletedTask;
        }

        if(currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("Authorization succeded");
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
