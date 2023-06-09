﻿using MyoFibril.Domain.Entities;
using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.CreateActivity;
public class CreateActivityRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;
    [JsonPropertyName("date_created")]
    public DateTimeOffset DateCreated { get; set; }
    [JsonPropertyName("performed_exercises")]
    public List<PerformedExerciseEntity>? PerformedExercises { get; set; }
    public string? AccessToken { get; set; }

}