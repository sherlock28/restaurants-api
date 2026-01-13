using Audit.Core;
using Audit.PostgreSql.Providers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Restaurants.Infrastructure.Providers;

public class MultiDataProvider : AuditDataProvider
{
	private readonly AuditDataProvider PostgresProvider;
	private readonly AuditDataProvider? ElasticProvider;
	private readonly bool ElasticEnabled;
	private readonly string IndexPrefix = "audit-logs";
	private readonly ILogger<MultiDataProvider> Logger;

	public MultiDataProvider(IConfiguration config, ILogger<MultiDataProvider> logger)
	{
		Logger = logger;

		var pgConnString = config.GetConnectionString("RestaurantsDB");
		var esConnString = config.GetConnectionString("AuditLogging:ElasticSearch:Url") ?? "http://localhost:9200";

		ElasticEnabled = config.GetValue<bool>("AuditLogging:ElasticSearch:Enabled");

		PostgresProvider = new PostgreSqlDataProvider(cfg => cfg
			.ConnectionString(pgConnString)
			.TableName("AuditLogs")
			.IdColumnName("Id")
			.CustomColumn("Id", ev => Guid.CreateVersion7())
			.DataColumn("Data", Audit.PostgreSql.Configuration.DataType.JSONB)
			.CustomColumn("EventType", ev => ev.EventType)
			.CustomColumn("UserId", ev => ev.CustomFields["UserId"])
			.CustomColumn("CreatedAt", _ => DateTime.UtcNow));

		if (ElasticEnabled)
		{

			ElasticProvider = new Audit.Elasticsearch.Providers.ElasticsearchDataProvider(cfg => cfg
				.Client(new Elastic.Clients.Elasticsearch.ElasticsearchClient(
					new Elastic.Clients.Elasticsearch.ElasticsearchClientSettings(new Uri(esConnString))
				))
				.Index(ev => $"{IndexPrefix}-{DateTime.UtcNow:yyyy-MM}")
			);
		}
	}

	public override object InsertEvent(AuditEvent auditEvent)
	{
		object? id = null;

		id = PostgresProvider.InsertEvent(auditEvent);
		
		if (ElasticEnabled && ElasticProvider != null)
		{
			try
			{
				ElasticProvider.InsertEvent(auditEvent);
			}
			catch (Exception ex)
			{
				LogElasticFailure(ex, auditEvent);
			}
		}

		return id ?? Guid.CreateVersion7();
	}

	public override async Task<object> InsertEventAsync(AuditEvent auditEvent, CancellationToken cancellationToken = default)
	{
		object? id = null;
					
		id = await PostgresProvider.InsertEventAsync(auditEvent);
		
		if (ElasticEnabled && ElasticProvider != null)
		{
			try
			{
				_ = ElasticProvider.InsertEventAsync(auditEvent);
			}
			catch (Exception ex)
			{
				LogElasticFailure(ex, auditEvent);
			}
		}

		return id ?? Guid.CreateVersion7();
	}

	private void LogElasticFailure(Exception ex, AuditEvent auditEvent)
	{
		var eventType = auditEvent.EventType ?? "UnknownEventType";
		var userId = auditEvent.CustomFields.ContainsKey("UserId")
			? auditEvent.CustomFields["UserId"]?.ToString() ?? "UnknownUser"
			: "UnknownUser";

		Logger.LogWarning(ex,
			"Failed to log audit event '{EventType}' (User: {UserId}) to Elasticsearch. Exception: {ExceptionMessage}. " +
			"This error does not prevent saving to Postgres but should be investigated to avoid loss of audit logs in Elasticsearch.",
			eventType, userId, ex.Message);
	}
}
