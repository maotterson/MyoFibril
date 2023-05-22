﻿using MyoFibril.Contracts.WebAPI.CreateActivity;
using MyoFibril.Contracts.WebAPI.GetActivity;

namespace MyoFibril.WebAPI.Services.Interfaces;

public interface IActivityService
{
    Task<CreateActivityResponse> CreateActivity(CreateActivityRequest request);
    Task<GetActivityResponse> GetActivityById(string id);
}
