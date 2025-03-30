using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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
}
