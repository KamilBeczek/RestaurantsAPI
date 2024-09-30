using Xunit;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Interfaces;
using System;
using FluentAssertions;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests;

public class UpdateRestaurantCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
    private readonly Mock<IRestaurantsRepository>_restaurantRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IRestaurantAuthorizationService> _restaurantAuthorizationSerivceMock;

    private readonly UpdateRestaurantCommandHandler _handler;

    public UpdateRestaurantCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        _restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
        _mapperMock = new Mock<IMapper>();
        _restaurantAuthorizationSerivceMock = new Mock<IRestaurantAuthorizationService>();

        _handler = new UpdateRestaurantCommandHandler(
            _loggerMock.Object,
            _restaurantRepositoryMock.Object,
            _mapperMock.Object,
            _restaurantAuthorizationSerivceMock.Object);

    }

    [Fact()]
    public async Task Handle_WithValidRequest_ShouldUpdateRestaurants()
    {
        var restaurantId = 1;
        var command = new UpdateRestaurantCommand()
        {
            Id = restaurantId,
            Name = "New Test",
            Description = "New Description",
            HasDelivery = true,
        };

        var restaurant = new Restaurant()
        {
            Id = restaurantId,
            Name = "Test",
            Description = "Test",
        };

        _restaurantRepositoryMock.Setup(r => r.GetById(restaurantId))
            .ReturnsAsync(restaurant);

        _restaurantAuthorizationSerivceMock.Setup(m => m.Authorize(restaurant, Domain.Constants.ResourceOperation.Update))
            .Returns(true);

        await _handler.Handle(command, CancellationToken.None);

        _restaurantRepositoryMock.Verify(r => r.Update(), Times.Once);
        _mapperMock.Verify(m => m.Map(command, restaurant), Times.Once);

    }

    [Fact()]
    public async Task Handle_WithNonExistingRestaurant_ShouldThrowNotFoundExcpetion()
    {
        var restaurantId = 2;
        var request = new UpdateRestaurantCommand()
        {
            Id = restaurantId,
        };

        _restaurantRepositoryMock.Setup(r => r.GetById(restaurantId))
            .ReturnsAsync((Restaurant?)null);

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Restaurant with id: {restaurantId} doesn't exist");

    }

    public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidExcpetion()
    {
        var restaurantId = 3;
        var request = new UpdateRestaurantCommand()
        {
            Id = restaurantId,
        };

        var existingRestaurant = new Restaurant()
        {
            Id = restaurantId,
        };

        _restaurantRepositoryMock
            .Setup(r => r.GetById(restaurantId))
            .ReturnsAsync(existingRestaurant);

        _restaurantAuthorizationSerivceMock
            .Setup(a => a.Authorize(existingRestaurant, ResourceOperation.Update))
            .Returns(false);

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<ForbidExcpetion>();



    }
}