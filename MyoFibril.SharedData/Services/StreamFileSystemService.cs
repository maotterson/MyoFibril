namespace MyoFibril.SharedData.Services;
public class StreamFileSystemService : IFileSystemService
{
    public async Task<string> LoadFileAsStringAsync(string path)
    {
        string fileContent;
        using (StreamReader reader = new StreamReader(path))
        {
            fileContent = await reader.ReadToEndAsync();
        }
        return fileContent;
    }
}