using Microsoft.AspNetCore.Components;

namespace MyoFibril.MAUIBlazorApp.Services.UI;
public class AppBarService : IAppBarService
{
    private NavigationManager _navigationManager;
    private Dictionary<string, string> _pageTitles = new Dictionary<string, string>()
    {
        { "", "" },
        { "NewActivity", "New Activity" },
        { "Activities", "View Activities" },
        { "Login", "Login" }
    };
    public AppBarService(NavigationManager navManager)
    {
        _navigationManager = navManager;
    }

    public string GetAppBarTitle()
    {
        var uri = _navigationManager.Uri;
        var relativePath = _navigationManager.ToBaseRelativePath(uri);
        var title = _pageTitles.GetValueOrDefault(relativePath);

        return title;
    }

    public bool IsAppBarVisible()
    {
        return !string.IsNullOrEmpty(GetAppBarTitle());
    }
}