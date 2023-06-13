using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
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

// Auth related services
builder.Services.AddScoped<IAuthorizeService, AuthorizeService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<ICredentialsRepository, CredentialsRepository>();

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

// mongodb client
builder.Services.AddSingleton<IMongoClient>(s =>
    {
        var settings = MongoClientSettings.FromConnectionString(builder.Configuration["Database:ConnectionString"]);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        return new MongoClient(settings);
    }
    
);

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
