# MyoFibril

### Required Settings

 - Ensure that the following variable is added to appsettings
```
 "StravaApp": {
    "RequestUri": "https://www.strava.com/api/v3"
  }
```

- Add the following secrets to your application once obtained from the strava developer dashboard
```
 "StravaApp:ClientId":"YOUR_CLIENT_ID"
 "StravaApp:ClientSecret":"YOUR_CLIENT_SECRET"
 "StravaApp:RefreshToken":"YOUR_OBTAINED_REFRESH_TOKEN"