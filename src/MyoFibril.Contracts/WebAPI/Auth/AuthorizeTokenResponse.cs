﻿using MyoFibril.Contracts.WebAPI.Auth.Models;
using System.Text.Json.Serialization;

namespace MyoFibril.Contracts.WebAPI.Auth;
public class AuthorizeTokenResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("user_info")]
    public UserInfo? UserInfo { get; set; }
    [JsonPropertyName("token_info")]
    public GetAccessTokenResponse? TokenInfo { get; set; }
}