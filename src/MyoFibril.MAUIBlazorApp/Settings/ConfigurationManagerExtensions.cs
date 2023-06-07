using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace MyoFibril.MAUIBlazorApp.Settings;
public static class ConfigurationManagerExtensions
{
    public static void AddAppSettings(this ConfigurationManager configurationManager)
    {

        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("MyoFibril.MAUIBlazorApp.appsettings.json");

        var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();


        configurationManager.AddConfiguration(config);
    }
}