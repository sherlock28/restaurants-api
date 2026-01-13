using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommandHandler(IUserContext userContext,
	IUserStore<User> userStore,
	ILogger<UpdateUserDetailsCommandHandler> logger) : IRequestHandler<UpdateUserDetailsCommand>
{
	public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
	{
		var user = userContext.GetCurrentUser();

		logger.LogInformation("Updating user with id '{UserId}' with { @UpdatedUser }", user!.Id, request);

		var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);

		if (dbUser == null)
		{
			throw new NotFoundException(nameof(User), user!.Id);
		}

		dbUser.DateOfBirth = request.DateOfBirth;
		dbUser.Nationality = request.Nationality;

		await userStore.UpdateAsync(dbUser, cancellationToken);
	}
}
