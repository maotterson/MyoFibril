using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Mvc;
using MyoFibril.Contracts.Strava.Models;
using MyoFibril.Contracts.WebAPI.Auth.Exceptions;
using MyoFibril.Contracts.WebAPI.CreateActivity;
using MyoFibril.Contracts.WebAPI.GetActivity;
using MyoFibril.Domain.Entities;
using MyoFibril.WebAPI.Repositories.Interfaces;
using MyoFibril.WebAPI.Services.Interfaces;
using MyoFibril.WebAPI.Strava.Services.Interfaces;
using System.ComponentModel;
using System.Xml.Linq;

namespace MyoFibril.WebAPI.Services;

public class ActivityService : IActivityService
{
    private readonly IStravaActivityService _stravaActivityService;
    private readonly IJwtService _jwtService;
    private readonly IActivityRepository _activityRepository;
    public ActivityService(IStravaActivityService stravaActivityService, IActivityRepository activityRepository, IJwtService jwtService)
    {
        _stravaActivityService = stravaActivityService;
        _activityRepository = activityRepository;
        _jwtService = jwtService;
    }

    public async Task<CreateActivityResponse> CreateActivity(CreateActivityRequest request)
    {
        // verify username matches contents of token
        var isValidUsernameForToken = _jwtService.VerifyTokenAgainstUsername(request.AccessToken!, request.Username);
        if (!isValidUsernameForToken) throw new UsernameTokenMismatchException();

        // persist to strava
        var stravaRequest = request.AsStravaCreateActivityRequest();
        var stravaResponse = await _stravaActivityService.CreateActivity(stravaRequest);

        // persist to repository
        var activityEntity = new ActivityEntity
        {
            Username = request.Username,
            Name = request.Name,
            DateCreated = DateTime.Now,
            StravaActivityId = stravaResponse.Id,
            PerformedExercises = request.PerformedExercises
        };
        var isCreated = await _activityRepository.CreateActivity(activityEntity);
        if (!isCreated) throw new Exception("Error creating activity");

        // return create activity response
        var createActivityResponse = new CreateActivityResponse
        {
            Id = activityEntity.Id,
            Name = activityEntity.Name,
            StravaActivity = stravaResponse,
            PerformedExercises = activityEntity.PerformedExercises
        };
        return createActivityResponse;
    }

    public async Task<GetActivityResponse> GetActivityById(string id, bool includeStrava = false)
    {
        var activityEntity = await _activityRepository.GetActivityById(id);
        StravaDetailedActivity? stravaActivity = null;

        // retrieve connected activity from strava if one exists and is requested
        if (includeStrava)
        {
            stravaActivity = activityEntity.StravaActivityId is not null ?
                await _stravaActivityService.GetActivityById(activityEntity.StravaActivityId.ToString()!) :
                null;
        }

        // return get activity response
        var getActivityResponse = new GetActivityResponse
        {
            Id = activityEntity.Id,
            Name = activityEntity.Name,
            StravaActivity = stravaActivity,
            PerformedExercises = activityEntity.PerformedExercises
        };

        return getActivityResponse;
    }

    public async Task<List<GetActivityResponse>> GetActivitiesForUsername(string username, string accessToken, bool includeStrava = false)
    {
        // verify username matches contents of token
        var isValidUsernameForToken = _jwtService.VerifyTokenAgainstUsername(accessToken, username);
        if (!isValidUsernameForToken) throw new UsernameTokenMismatchException();

        var activityList = await _activityRepository.GetActivitiesForUsername(username);
        // todo: could additionally retrieve strava specific information but would simplify/cut down on external calls to forgo it for all activities

        // map entities to response collection
        var activitiesResponse = activityList.Select(a => new GetActivityResponse
        {
            Id = a.Id,
            Name = a.Name,
            DateCreated = a.DateCreated,
            Username = a.Username,
            PerformedExercises = a.PerformedExercises,
        });

        return activitiesResponse.ToList();
    }
}
