using MediatR;
using Audit.Core;
using System.Reflection;
using Restaurants.Application.Users;
using Restaurants.Application.Common.Attributes;

namespace Restaurants.Application.Common.Behaviors;

public class AuditBehavior<TRequest, TResponse>(IUserContext userContext) : IPipelineBehavior<TRequest, TResponse>
	where TRequest : notnull
{
	public async Task<TResponse> Handle(
		TRequest request,
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		var shouldAudit = request?.GetType().GetCustomAttribute<AuditLogAttribute>() != null;

		if (!shouldAudit)
			return await next();

		var userId = userContext.GetCurrentUser()?.Id;

		var response = await next();

		using (var scope = AuditScope.Create(new AuditScopeOptions
		{
			EventType = request!.GetType().Name,
			CreationPolicy = EventCreationPolicy.InsertOnEnd,
			ExtraFields = new
			{
				UserId = userId,
				Request = request,
				Response = response,
				Success = true,
				Timestamp = DateTime.UtcNow
			}
		}))
		{
			return response;
		}
	}
}
