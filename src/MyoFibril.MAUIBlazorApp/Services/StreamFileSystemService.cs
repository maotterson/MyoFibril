using MyoFibril.SharedData.Services;
using System.Reflection;

namespace MyoFibril.MAUIBlazorApp.Services;
public class StreamFileSystemService : IFileSystemService
{
    public async Task<string> LoadFileAsStringAsync(string path)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using (Stream stream = assembly.GetManifestResourceStream(path))
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