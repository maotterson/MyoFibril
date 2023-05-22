using MyoFibril.Contracts.WebAPI.CreateActivity;
using System.Net.Http;

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
        throw new NotImplementedException();
    }
}