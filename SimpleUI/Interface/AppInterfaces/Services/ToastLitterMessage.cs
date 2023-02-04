using SimpleUI.Controls;
using SimpleUI.Services;
using System;
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
            control.OwnerPanel = ShowOwner;
            control.Message = message;
            control.Show(ShowTime);
        }));
    }
}
