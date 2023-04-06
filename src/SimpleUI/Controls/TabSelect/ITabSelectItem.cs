using System.Windows;
using static SimpleUI.Utils.Controls.IconResources;

namespace SimpleUI.Controls;

public interface ITabSelectItem
{
    public bool IsSelect { get; set; }

    public string Header { get; set; }

    public object Tag { get; set; }

    public FontIconEnum Icon { get; set; }
    
}
