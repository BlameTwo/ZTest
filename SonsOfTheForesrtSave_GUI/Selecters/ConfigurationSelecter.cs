using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SonsOfTheForesrtSave_GUI.Selecters;

public class ConfigurationSelecter : DataTemplateSelector
{
    public DataTemplate DefaultDataTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container) {
       

        return base.SelectTemplate(item, container);
    }

    public DataTemplate FloatDataTemplate { get; set; }

    public DataTemplate StringDataTemplate { get; set; }
}
