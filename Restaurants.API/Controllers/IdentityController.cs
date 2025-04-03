using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Restaurants.Domain.Constants;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restaurants.Application.Users.Commands.UnassignUserRole;
using Restaurants.Application.Users.Commands.UpdateUserDetails;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController(IMediator mediator) : ControllerBase
{
	[HttpPatch("user")]
	[Authorize]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
	{
		await mediator.Send(command);

		return NoContent();
	}

	[HttpPost("userRole")]
	[Authorize(Roles = UserRoles.Admin)]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
	{
		await mediator.Send(command);

		return NoContent();
	}

	[HttpDelete("userRole")]
	[Authorize(Roles = UserRoles.Admin)]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command)
	{
		await mediator.Send(command);

		return NoContent();
	}
}
