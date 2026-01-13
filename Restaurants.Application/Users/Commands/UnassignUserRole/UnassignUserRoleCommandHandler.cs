using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UnassignUserRole;

public class UnassignUserRoleCommandHandler(
	UserManager<User> userManager,
	RoleManager<IdentityRole> roleManager,
	ILogger<UnassignUserRoleCommandHandler> logger) : IRequestHandler<UnassignUserRoleCommand>
{
	public async Task Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Unassigning user role {@Request}", request);

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

		await userManager.RemoveFromRoleAsync(dbUser, role.Name!);
	}
}
