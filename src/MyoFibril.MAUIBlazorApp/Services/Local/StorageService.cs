using System.Text.Json;

namespace MyoFibril.MAUIBlazorApp.Services.Local;
public class StorageService : IStorageService
{
    public async Task StoreItemAsync<T>(string key, T item)
    {
        var json = JsonSerializer.Serialize(item);
        await SecureStorage.SetAsync(key, json);
    }

    public async Task<T> GetItemAsync<T>(string key)
    {
        try
        {
            var json = await SecureStorage.GetAsync(key);
            if (json is null) return default(T);
            return JsonSerializer.Deserialize<T>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public void RemoveItem(string key)
    {
        SecureStorage.Remove(key);
    }
}

