namespace MyoFibril.MAUIBlazorApp.Services.Local;
public interface IStorageService
{
    Task StoreItemAsync<T>(string key, T item);
    Task<T> GetItemAsync<T>(string key);
    void RemoveItem(string key);
}