using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Users.Tests;

[TestClass()]
public class UserContextTests
{
	[TestMethod()]
	public void GetCurrentUserTest_WithAuthenticatedUser_ShouldReturnCurrentUser()
	{
		// arrange

		var dateOfBirth = new DateOnly(1993, 3, 8);

		var httpContextAccessorMoq = new Mock<IHttpContextAccessor>();

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, "1"),
			new Claim(ClaimTypes.Email, "test@test.com"),
			new Claim(ClaimTypes.Role, UserRoles.Admin),
			new Claim(ClaimTypes.Role, UserRoles.User),
			new Claim("Nationality", "Argentinian"),
			new Claim("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))
		};

		var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

		httpContextAccessorMoq.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
		{
			User = user
		});

		var userContext = new UserContext(httpContextAccessorMoq.Object);

		// act

		var currentUser = userContext.GetCurrentUser();

		// assert

		currentUser.Should().NotBeNull();
		currentUser!.Id.Should().Be("1");
		currentUser.Email.Should().Be("test@test.com");
		currentUser.Roles.Should().ContainInOrder(new[] { UserRoles.Admin, UserRoles.User });
		currentUser.Nationality.Should().Be("Argentinian");
		currentUser.DateOfBirth.Should().Be(dateOfBirth);
	}

	[TestMethod()]
	public void GetCurrentUserTest_WithUserContextNotPresent_ThrowsInvalidOperationException()
	{
		// arrange
		var httpContextAccessorMoq = new Mock<IHttpContextAccessor>();
		httpContextAccessorMoq.Setup(x => x.HttpContext).Returns((HttpContext?)null);

		var userContext = new UserContext(httpContextAccessorMoq.Object);

		// act

		Action action = () => userContext.GetCurrentUser();

		// assert

		action.Should().Throw<InvalidOperationException>().WithMessage("User context is not present.");
	}
}
