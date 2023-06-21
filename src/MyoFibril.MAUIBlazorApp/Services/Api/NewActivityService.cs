using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using MyoFibril.Contracts.WebAPI.Auth.Models;
using MyoFibril.Contracts.WebAPI.CreateActivity;
using MyoFibril.MAUIBlazorApp.Services.Local;
using MyoFibril.MAUIBlazorApp.Storage.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MyoFibril.MAUIBlazorApp.Services.Api;
public class NewActivityService : INewActivityService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly IStorageService _storageService;
    public NewActivityService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IStorageService storageService)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _storageService = storageService;
    }
    public async Task<CreateActivityResponse> CreateActivity(CreateActivityRequest createActivityRequest)
    {
        // todo attach username to request via storage service in separate method (userservice?)
        var userInfo = await _storageService.GetItemAsync<UserInfo>("user_info");
        var username = userInfo.Username;
        createActivityRequest.Username = username;

        var httpClient = _httpClientFactory.CreateClient("MyClient");
        var baseUri = _configuration["Settings:API:BaseUri"];
        var requestUri = $"{baseUri}/Activity";

        // todo move retrieval/attachment of bearer header to utils
        var tokenInfo = await _storageService.GetItemAsync<TokenInfo>("token_info");
        if (tokenInfo is null) throw new Exception(); // todo better exception
        var accessToken = tokenInfo.AccessToken;
        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

        var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(createActivityRequest), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(requestUri, jsonContent);

        // Check if the response is successful
        response.EnsureSuccessStatusCode();

        // Deserialize the response content to the desired type
        var responseBody = await response.Content.ReadAsStringAsync();
        var createActivityResponse = JsonSerializer.Deserialize<CreateActivityResponse>(responseBody);

        // Return the deserialized response
        return createActivityResponse;
    }
}