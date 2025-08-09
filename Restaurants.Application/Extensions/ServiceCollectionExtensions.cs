using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Users;
using Restaurants.Application.Common.Behaviors;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddApplication(this IServiceCollection services)
	{
		var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
		services.AddAutoMapper(applicationAssembly);
		services.AddValidatorsFromAssembly(applicationAssembly)
			.AddFluentValidationAutoValidation();
		services.AddScoped<IUserContext, UserContext>();
		services.AddHttpContextAccessor();

		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuditBehavior<,>));
	}
}
