using Microsoft.AspNetCore.Components;

namespace MyoFibril.MAUIBlazorApp.Services.UI;
public class AppBarService : IAppBarService
{
    private NavigationManager _navigationManager;
    private Dictionary<string, string> _pageTitles = new Dictionary<string, string>()
    {
        { "/", "" },
        { "/NewActivity", "New Activity" },
        { "/Activities", "View Activities" }
    };
    public AppBarService(NavigationManager navManager)
    {
        _navigationManager = navManager;
    }
    public string GetAppBarTitle()
    {
        var uri = _navigationManager.Uri;
        return _pageTitles.GetValueOrDefault(uri);
    }

    public bool IsAppBarVisible()
    {
        return !string.IsNullOrEmpty(GetAppBarTitle());
    }
}