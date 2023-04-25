using SimpleUI.Controls;
using SimpleUI.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SimpleUI.Interface.AppInterfaces.Services;

public class ToastLitterMessage: IToastLitterMessage
{
    public Panel ShowOwner { get; set; }
    public TimeSpan ShowTime { get; set; }

    [STAThread]
    public async void Show(string message)
    {
        await ShowOwner.Dispatcher.InvokeAsync(new Action(() =>
        {
            IToastService control = new ToastControl();
            if (ShowOwner as Grid != null)
            {
                Grid.SetRow(control as ToastControl, (ShowOwner as Grid).RowDefinitions.Count - 1);
            }
            control.OwnerPanel = ShowOwner;
            control.Message = message;
            control.Show(ShowTime);
        }));
    }
}
