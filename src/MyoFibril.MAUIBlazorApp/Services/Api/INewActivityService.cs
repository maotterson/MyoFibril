using MyoFibril.Contracts.WebAPI.CreateActivity;

namespace MyoFibril.MAUIBlazorApp.Services.Api;
public interface INewActivityService
{
    Task<CreateActivityResponse> CreateActivity(CreateActivityRequest createActivityRequest);
}