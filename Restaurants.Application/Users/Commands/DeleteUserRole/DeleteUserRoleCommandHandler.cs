﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restaurants.Domain.Entities;
using Restaurants.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.Commands.DeleteUserRole
{
    public class DeleteUserRoleCommandHandler
        (ILogger<AssignUserRoleCommandHandler> logger,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager) : IRequestHandler<DeleteUserRoleCommand>
    {
        public async Task Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Delete user role: {@Request}", request);
            var user = await userManager.FindByEmailAsync(request.UserEmail)
                ?? throw new NotFoundException(nameof(User), request.UserEmail);

            var role = await roleManager.FindByNameAsync(request.RoleName)
                ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

            await userManager.RemoveFromRoleAsync(user, role.Name!);
        }
    
    }
}