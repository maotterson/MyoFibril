using MyoFibril.Contracts.WebAPI.CreateActivity;
using System.Net.Http;
using System.Text;

namespace MyoFibril.MAUIBlazorApp.Services;
public class NewActivityService : INewActivityService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public NewActivityService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<CreateActivityResponse> CreateActivity(CreateActivityRequest createActivityRequest)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var requestUri = "https://localhost:7152/Activity";
        var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(createActivityRequest), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(requestUri, jsonContent);

        // Check if the response is successful
        response.EnsureSuccessStatusCode();

        // Deserialize the response content to the desired type
        var responseBody = await response.Content.ReadAsStringAsync();
        var createActivityResponse = System.Text.Json.JsonSerializer.Deserialize<CreateActivityResponse>(responseBody);

        // Return the deserialized response
        return createActivityResponse;
    }
}