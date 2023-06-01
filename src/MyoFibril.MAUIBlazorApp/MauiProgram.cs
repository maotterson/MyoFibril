using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyoFibril.MAUIBlazorApp.Components.CreateActivity;
using MyoFibril.MAUIBlazorApp.Services;
using MyoFibril.SharedData.Services;
using MyoFibril.SharedData;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using MyoFibril.MAUIBlazorApp.Services.Local;
using MyoFibril.MAUIBlazorApp.Services.Api;
using MyoFibril.MAUIBlazorApp.Services.UI;

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

        // add http client (ignores ssl for use with localhost env)
        builder.Services.AddHttpClient("MyClient", client => {
            client.Timeout = TimeSpan.FromMinutes(2);
           })
           .ConfigurePrimaryHttpMessageHandler(() => {
               HttpClientHandler clientHandler = new HttpClientHandler();
               clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

               return clientHandler;
           });

        // add options-related services
        builder.Services.AddScoped<ILocalOptionsService, LocalOptionsService>();

        // add api-oriented services
        builder.Services.AddScoped<CreateActivityViewModel>();
        builder.Services.AddScoped<INewActivityService, NewActivityService>();

        // add load data services
        builder.Services.AddSharedData();
        builder.Services.AddScoped<IFileSystemService, StreamFileSystemService>();

        // add services to manage local state
        builder.Services.AddScoped<IAddExerciseService, AddExerciseService>();
        builder.Services.AddScoped<IBuildActivityService, BuildActivityService>();

        // add ui-related services
        builder.Services.AddScoped<IAppBarService, AppBarService>();

        return builder.Build();
    }
}
