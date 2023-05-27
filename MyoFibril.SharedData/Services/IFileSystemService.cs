namespace MyoFibril.SharedData.Services;
public interface IFileSystemService
{
    Task<string> LoadFileAsStringAsync(string path);
}