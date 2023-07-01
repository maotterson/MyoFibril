using Microsoft.Extensions.Configuration;
using MyoFibril.Contracts.WebAPI.CreateActivity;
using MyoFibril.Contracts.WebAPI.GetActivity;
using MyoFibril.Domain.Entities;
using MyoFibril.MAUIBlazorApp.Services.Local;
using MyoFibril.MAUIBlazorApp.Storage.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace MyoFibril.MAUIBlazorApp.Data.Activities;
public class ActivitiesRepository : IActivitiesRepository
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IStorageService _storageService;
    public ActivitiesRepository(HttpClient http, IConfiguration configuration, IStorageService storageService)
    {
        _httpClient = http;
        _configuration = configuration;
        _storageService = storageService;
    }
    public async Task<List<ActivityEntity>> GetActivitiesByUsernameAsync(string username)
    {
        var baseUri = _configuration["Settings:API:BaseUri"];
        var requestUri = $"{baseUri}/Activity/by-user/{username}";

        // get and attach bearer token to httpclient
        var tokenInfo = await _storageService.GetItemAsync<TokenInfo>("token_info");
        if (tokenInfo is null) throw new Exception(); // todo better exception
        var accessToken = tokenInfo.AccessToken;
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

        // send request
        var response = await _httpClient.GetAsync(requestUri);

        // Check if the response is successful
        response.EnsureSuccessStatusCode();

        // Deserialize the response content to the desired type
        var responseBody = await response.Content.ReadAsStringAsync();
        var getActivitiesResponse = JsonSerializer.Deserialize<List<GetActivityResponse>>(responseBody);

        return getActivitiesResponse.Select(dto => new ActivityEntity
        {
            StravaActivityId = dto.StravaActivity.Id,
            Name = dto.Name,
            PerformedExercises = dto.PerformedExercises,
            DateCreated = dto.DatePerformed,
            Username = dto.Username
        }).ToList();
    }
}
