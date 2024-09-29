using FluentAssertions;
using Restaurants.Domain.Constants;
using Xunit;

namespace Restaurants.Application.Users.Tests;

public class CurrentUserTests
{
    [Theory()]

    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRoleTest_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        var isInRole = currentUser.IsInRole(roleName);

        isInRole.Should().BeTrue();
    }

    [Fact()]
    public void IsInRoleTest_WithMatchingRole_ShouldReturnFalse()
    {
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        var isInRole = currentUser.IsInRole(UserRoles.Owner);

        isInRole.Should().BeFalse();
    }

    [Fact()]
    public void IsInRoleTest_WithMatchingRoleCase_ShouldReturnFalse()
    {
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());

        isInRole.Should().BeFalse();
    }
}