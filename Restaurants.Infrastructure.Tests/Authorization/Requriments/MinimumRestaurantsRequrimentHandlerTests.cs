using Xunit;
using Restaurants.Infrastructure.Authorization.Requriments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using FluentAssertions;

namespace Restaurants.Infrastructure.Authorization.Requriments.Tests
{
    public class MinimumRestaurantsRequrimentHandlerTests
    {
        [Fact()]
        public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucceed()
        {
            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
            var userContextMock = new Mock<IUserContext>();

            var restaurants = new List<Restaurant>()
            {
                new()
                {
                    OwnerId = currentUser.Id,
                },
                new()
                {
                    OwnerId = currentUser.Id,
                },
                new()
                {
                    OwnerId = "2",
                },
            };

            var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
            restaurantsRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurants);

            var requirment = new MinimumRestaurantsRequriment(2);
            var handler = new MinimumRestaurantsRequrimentHandler(restaurantsRepositoryMock.Object,
                userContextMock.Object);

            var context = new AuthorizationHandlerContext([requirment], null, null);

            await handler.HandleAsync(context);

            context.HasSucceeded.Should().BeTrue();
        }


        public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldFail()
        {
            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
            var userContextMock = new Mock<IUserContext>();

            var restaurants = new List<Restaurant>()
            {
                new()
                {
                    OwnerId = currentUser.Id,
                },
                new()
                {
                    OwnerId = "2",
                },
            };

            var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
            restaurantsRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurants);

            var requirment = new MinimumRestaurantsRequriment(2);
            var handler = new MinimumRestaurantsRequrimentHandler(restaurantsRepositoryMock.Object,
                userContextMock.Object);

            var context = new AuthorizationHandlerContext([requirment], null, null);

            await handler.HandleAsync(context);

            context.HasSucceeded.Should().BeFalse();
        }
    }
}
