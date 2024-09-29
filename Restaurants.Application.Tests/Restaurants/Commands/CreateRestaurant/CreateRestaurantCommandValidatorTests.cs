using FluentValidation.TestHelper;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        var command = new CreateRestaurantCommand()
        {
            Name = "Test",
            Description = "Test",
            Category = "Italian",
            ContactEmail = "test@test.com",
            PostalCode = "13-345",

        };

        var validator = new CreateRestaurantCommandValidator();

        var result = validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();

    }

    [Fact()]
    public void Validator_ForValidCommand_ShouldHaveValidationErrors()
    {
        var command = new CreateRestaurantCommand()
        {
            Name = "Te",
            Description = "Test",
            Category = "Itali",
            ContactEmail = "@test.com",
            PostalCode = "13345",

        };

        var validator = new CreateRestaurantCommandValidator();

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);

    }

}