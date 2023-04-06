namespace SimpleUI.Interface;

/// <summary>
/// Page附加继承接口
/// </summary>
public interface IPage
{
    public void NavigationLoaded(object data);

    public void NavigationLeaved(object data);
}
