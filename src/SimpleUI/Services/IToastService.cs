using System;
using System.Windows;
using System.Windows.Controls;

namespace SimpleUI.Services;

public interface IToastService
{
    /// <summary>
    /// 弹出在哪里弹出
    /// </summary>
    public Panel OwnerPanel { get; set; }

    /// <summary>
    /// 弹出方法
    /// </summary>
    /// <param name="time"></param>
    public void Show(TimeSpan time);

    /// <summary>
    /// 弹出的消息
    /// </summary>
    public string Message { get; set; }
}
