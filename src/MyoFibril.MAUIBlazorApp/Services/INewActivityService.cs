using MyoFibril.Contracts.WebAPI.CreateActivity;

namespace MyoFibril.MAUIBlazorApp.Services;
public interface INewActivityService
{
    Task<CreateActivityResponse> CreateActivity(CreateActivityRequest createActivityRequest);
}