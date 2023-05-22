using Microsoft.Extensions.Logging;
using MyoFibril.MAUIBlazorApp.Data;
using MyoFibril.MAUIBlazorApp.Services;

namespace MyoFibril.MAUIBlazorApp;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddScoped<INewActivityService, NewActivityService>();

        return builder.Build();
    }
}
