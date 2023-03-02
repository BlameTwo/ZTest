using SimpleUI.Interface;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SimpleUI.Controls;

[TemplatePart(Name = "UpButton",Type =typeof(System.Windows.Controls.Button))]
[TemplatePart(Name = "DownButton",Type =typeof(System.Windows.Controls.Button))]
[TemplatePart(Name = "OpenPopupButton",Type =typeof(System.Windows.Controls.Button))]
[TemplatePart(Name = "Popup",Type =typeof(Popup))]
public class NumberBox : Control,IControl {

    private System.Windows.Controls.Button upbutton = null, downbutton = null,opnpopupbutton=null;
    private Popup popup = null;
    public NumberBox()
    {
    }

    public override void OnApplyTemplate() {
        base.OnApplyTemplate();
        upbutton = GetTemplateChild("UpButton") as System.Windows.Controls.Button;
        downbutton = GetTemplateChild("DownButton") as System.Windows.Controls.Button;
        opnpopupbutton = GetTemplateChild("OpenPopupButton") as System.Windows.Controls.Button;
        popup = GetTemplateChild("Popup") as Popup;
        if(popup != null && opnpopupbutton!= null) {
            opnpopupbutton.Click += (s, e) => {
                popup.IsOpen = !popup.IsOpen;
            };
        }
        upbutton.Click += (s, e) => {
            var intvalue = string.IsNullOrWhiteSpace(Number) ? 0 : int.Parse(Number);
            if (intvalue < MaxNumber) {
                intvalue += NumberSplit;
                this.Number = intvalue.ToString();
            }
        };

        downbutton.Click += (s, e) => {
            var intvalue = string.IsNullOrWhiteSpace(Number) ? 0 : int.Parse(Number);
            if (intvalue > MinHeight) {
                intvalue -= NumberSplit;
                this.Number = intvalue.ToString();
            }
        };
    }



    public string Number {
        get { return (string)GetValue(NumberProperty); }
        set { SetValue(NumberProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Number.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty NumberProperty =
        DependencyProperty.Register("Number", typeof(string), typeof(NumberBox), new PropertyMetadata("0"));



    /// <summary>
    /// 判断是否是数字
    /// </summary>
    internal bool IsNumber {
        get { return (bool)GetValue(IsNumberProperty); }
        set { SetValue(IsNumberProperty, value); }
    }

    // Using a DependencyProperty as the backing store for IsNumber.  This enables animation, styling, binding, etc...
    internal static readonly DependencyProperty IsNumberProperty =
        DependencyProperty.Register("IsNumber", typeof(bool), typeof(NumberBox), new PropertyMetadata(true));



    public CornerRadius CornerRadius {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NumberBox), new PropertyMetadata(default(CornerRadius)));



    public int NumberSplit {
        get { return (int)GetValue(NumberSplitProperty); }
        set { SetValue(NumberSplitProperty, value); }
    }

    // Using a DependencyProperty as the backing store for NumberSplit.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty NumberSplitProperty =
        DependencyProperty.Register("NumberSplit", typeof(int), typeof(NumberBox), new PropertyMetadata(1));





    public int MaxNumber {
        get { return (int)GetValue(MaxNumberProperty); }
        set { SetValue(MaxNumberProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MaxNumber.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MaxNumberProperty =
        DependencyProperty.Register("MaxNumber", typeof(int), typeof(NumberBox), new PropertyMetadata(0));




    public int MinNumber {
        get { return (int)GetValue(MinNumberProperty); }
        set { SetValue(MinNumberProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MinNumber.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MinNumberProperty =
        DependencyProperty.Register("MinNumber", typeof(int), typeof(NumberBox), new PropertyMetadata(0));



}
