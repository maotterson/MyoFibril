using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace MyoFibril.MAUIBlazorApp.Settings;
public static class ConfigurationManagerExtensions
{
    public static void AddAppSettings(this ConfigurationManager configurationManager)
    {

        var a = Assembly.GetExecutingAssembly();
        var resourcePath = $"{a.GetName().Name}.Resources.Raw.appsettings.development.json"; // swap out in production
        using var stream = a.GetManifestResourceStream(resourcePath);

        var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();


        configurationManager.AddConfiguration(config);
    }
}