using MyoFibril.SharedData.Services;
using System.Reflection;

namespace MyoFibril.MAUIBlazorApp.Services;
public class StreamFileSystemService : IFileSystemService
{
    public async Task<string> LoadFileAsStringAsync(string path)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourcePath = $"{assembly.GetName().Name}.resources.raw.{path}";

        using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
        {
            if (stream == null)
            {
                throw new FileNotFoundException($"Resource '{path}' not found.");
            }

            using (StreamReader reader = new StreamReader(stream))
            {
                string fileContent = await reader.ReadToEndAsync();
                return fileContent;
            }
        }
    }
}