using MediatR;
using Mapster;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Users;
using Restaurants.Application.Common.Behaviors;
using MapsterMapper;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddApplication(this IServiceCollection services)
	{
		var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

		// MediatR
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

		// Mapster
		var config = TypeAdapterConfig.GlobalSettings;
		config.Scan(applicationAssembly);
		services.AddSingleton(config);
		services.AddScoped<IMapper, Mapper>();
		//services.AddAutoMapper(applicationAssembly);

		// FluentValidation
		services.AddValidatorsFromAssembly(applicationAssembly)
			.AddFluentValidationAutoValidation();

		// UserContext
		services.AddScoped<IUserContext, UserContext>();
		services.AddHttpContextAccessor();

		// Pipeline Behavior
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuditBehavior<,>));
	}
}
