using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommandHandler(
	UserManager<User> userManager,
	RoleManager<IdentityRole> roleManager,
	ILogger<AssignUserRoleCommandHandler> logger) : IRequestHandler<AssignUserRoleCommand>
{
	public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Assigning user role {@Request}", request);

		var dbUser = await userManager.FindByEmailAsync(request.UserEmail);

		if (dbUser == null)
		{
			throw new NotFoundException(nameof(User), request.UserEmail);
		}

		var role = await roleManager.FindByNameAsync(request.RoleName);

		if (role == null)
		{
			throw new NotFoundException(nameof(IdentityRole), request.RoleName);
		}

		await userManager.AddToRoleAsync(dbUser, role.Name!);
	}
}
