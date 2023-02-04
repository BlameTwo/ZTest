using System.Windows.Controls;

namespace SimpleUI.Interface.AppInterfaces.Services;

public class AppNavigationViewService : IAppNavigationViewService
{
    private Frame _frame;


    public void Init(Frame frame,bool InNaved)
    {
        if (frame == null) return;
        _frame = frame;
        if (InNaved)
        {
            if (_frame != null)
            {
                _frame.Navigating -= _frame_Navigating;
                _frame.LoadCompleted -= _frame_LoadCompleted;
            }
            _frame.LoadCompleted += _frame_LoadCompleted;
            _frame.Navigating += _frame_Navigating;
        }


    }

    private void _frame_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
    {
        if (e.Content is IPage page)
        {
            page.NavigationLeaved(e.ExtraData);
        }
    }

    public void Navigation<T>(T content, object data)
    {
        if (_frame != null)
        {
            if (content == null) return;
            _frame.Navigate(content, data);
        }
    }

    private void _frame_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
    {
        if (e.Content is IPage page)
        {
            page.NavigationLoaded(e.ExtraData);
        }
    }


}
