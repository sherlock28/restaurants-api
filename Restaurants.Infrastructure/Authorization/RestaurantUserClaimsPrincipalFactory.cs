using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authorization.Constants;

namespace Restaurants.Infrastructure.Authorization;

public class RestaurantUserClaimsPrincipalFactory(
	UserManager<User> userManager,
	RoleManager<IdentityRole> roleManager,
	IOptions<IdentityOptions> options) : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
{
	public override async Task<ClaimsPrincipal> CreateAsync(User user)
	{
		var id = await GenerateClaimsAsync(user);

		if (user.Nationality != null)
		{
			id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));
		}

		if (user.DateOfBirth != null)
		{
			id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.ToString()!));
		}

		return new ClaimsPrincipal(id);
	}
}
