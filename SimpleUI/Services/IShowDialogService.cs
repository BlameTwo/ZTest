using SimpleUI.Interface;

namespace SimpleUI.Services;

/// <summary>
/// 对对话框进行扩展的服务
/// </summary>
public interface IShowDialogService
{
    /// <summary>
    /// 扩展，用来显示对话框,如果返回false则当前已经有一个对话框了
    /// </summary>
    /// <typeparam name="T">继承于IDialogHost的一个类</typeparam>
    bool Show<T,T1>(T arg,T1 data)
        where T : IDialogHost;

    /// <summary>
    /// 此时是否可弹出对话框
    /// </summary>
    bool IsShow { get; }

    /// <summary>
    /// 关闭活动对话框，解耦VM中的关闭方法
    /// </summary>
    /// <returns></returns>
    bool CloseDialog();
}
