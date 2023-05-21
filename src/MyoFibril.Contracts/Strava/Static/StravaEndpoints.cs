using static System.Net.WebRequestMethods;

namespace MyoFibril.Contracts.Strava.Static;
public static class StravaEndpoints
{
    public const string API_BASE_URL = "https://www.strava.com/api/v3";
    public const string GET_ACCESS_TOKEN = "/oauth/token";
    public const string ACTIVITIES = "/activities";
}