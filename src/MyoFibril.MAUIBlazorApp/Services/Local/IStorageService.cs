namespace MyoFibril.MAUIBlazorApp.Services.Local;
public interface IStorageService
{
    Task<T> GetItemAsync<T>(string path);
}