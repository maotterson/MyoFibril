using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyoFibril.MAUIBlazorApp.Services;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

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

        builder.Services.AddScoped<INewActivityService, NewActivityService>();

        return builder.Build();
    }
}
