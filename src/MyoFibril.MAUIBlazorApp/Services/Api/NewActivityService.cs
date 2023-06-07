using MyoFibril.Contracts.WebAPI.CreateActivity;
using System.Text;
using System.Text.Json;

namespace MyoFibril.MAUIBlazorApp.Services.Api;
public class NewActivityService : INewActivityService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public NewActivityService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<CreateActivityResponse> CreateActivity(CreateActivityRequest createActivityRequest)
    {
        var httpClient = _httpClientFactory.CreateClient("MyClient");
        var requestUri = "http://localhost:5230/Activity"; // todo move to config
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