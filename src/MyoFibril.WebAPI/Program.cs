using MyoFibril.Contracts.Strava.Static;
using MyoFibril.WebAPI.Strava.Api;
using MyoFibril.WebAPI.Strava.Extensions;
using MyoFibril.WebAPI.Strava.OAuth;
using Refit;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

// Register Strava OAuth Services
builder.Services.AddStravaOAuthServices(configuration);

// Attach Strava External API Client
builder.Services
  .AddRefitClient<IStravaApi>()
  .ConfigureHttpClient(client => client.BaseAddress = new Uri(StravaEndpoints.API_BASE_URL))
  .AddHttpMessageHandler<AuthHeaderHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
