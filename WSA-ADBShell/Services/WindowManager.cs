using System;
using System.Windows;
using WSA_ADBShell.Services.Interfaces;

namespace WSA_ADBShell.Services;

public class WindowManager : IWindowManager
{
    private Window _window;

    public Window Window { get => _window;set=> _window = value; }

    public void DispatcherChanged(Action action)
    {
        if (this._window == null)
            new Exception("未找到当前Window主窗口");
        _window.Dispatcher.Invoke(action);
    }

    public void SetTitleBarText(string text) =>
        _window.Title = text;
}
