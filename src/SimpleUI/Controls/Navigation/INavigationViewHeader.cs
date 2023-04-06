using SimpleUI.Services;

namespace SimpleUI.Controls;

public interface INavigationViewHeader: INavigationItemServices
{
    public string Header { get; set; }
}
