using Serilog;
using Serilog.Events;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((context, configuration)
	=> configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseSerilogRequestLogging();

var scope = app.Services.
CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
