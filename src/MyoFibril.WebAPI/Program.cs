using MyoFibril.Contracts.Strava.Static;
using MyoFibril.WebAPI.Repositories;
using MyoFibril.WebAPI.Repositories.Interfaces;
using MyoFibril.WebAPI.Services;
using MyoFibril.WebAPI.Services.Interfaces;
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

// Add local api services
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddSingleton<IActivityRepository, ActivityInMemoryRepository>();


// Register Strava services
builder.Services.AddStravaOAuthServices(configuration);
builder.Services.AddStravaServices(configuration);
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
