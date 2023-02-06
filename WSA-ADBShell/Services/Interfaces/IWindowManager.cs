using System;
using System.Windows;

namespace WSA_ADBShell.Services.Interfaces;

public interface IWindowManager
{
    void SetTitleBarText(string text);

    void DispatcherChanged(Action action);

    Window Window { get; set; }
}
