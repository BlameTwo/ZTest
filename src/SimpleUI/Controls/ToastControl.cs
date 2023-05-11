using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;
using SimpleUI.Services;
using System.Windows.Media;

namespace SimpleUI.Controls;

public class ToastControl : Control, IToastService
{
    static ToastControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ToastControl), new System.Windows.FrameworkPropertyMetadata(typeof(ToastControl)));
    }

    



    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ToastControl), new PropertyMetadata(default(CornerRadius)));




    public object Icon
    {
        get { return (object)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(object), typeof(ToastControl), new PropertyMetadata(null));



    public string Message
    {
        get { return (string)GetValue(MessageProperty); }
        set { SetValue(MessageProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Message.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register("Message", typeof(string), typeof(ToastControl), new PropertyMetadata(""));



    public Panel OwnerPanel { get; set; }

    public void Show(TimeSpan time)
    {
        OwnerPanel.Dispatcher.Invoke(new Action(async () =>
        {
            OwnerPanel.Children.Add(this);
            Storyboard open = GetBoard(0);
            open.Begin();
            await Task.Delay(time);
            Storyboard close = GetBoard(1);
            close.FillBehavior = FillBehavior.Stop;
            close.Begin();
            await Task.Delay(TimeSpan.FromSeconds(2));
            OwnerPanel.Children.Remove(this);
        }));
    }

    /// <summary>
    /// 获得故事板
    /// </summary>
    /// <param name="state">状态，1为正在显示，0为准备关闭</param>
    /// <returns></returns>
    private Storyboard GetBoard(int state)
    {
        TransformGroup group = new TransformGroup();
        TranslateTransform translate = new TranslateTransform() { Y=this.ActualHeight };
        group.Children.Add(translate);
        this.RenderTransform = group;
        Storyboard story = new();
        if (state == 1)
        {
            DoubleAnimation opac = new DoubleAnimation() { From = 1,To = 0,Duration = TimeSpan.FromSeconds(0.5) };
            Storyboard.SetTarget(opac, this);
            Storyboard.SetTargetProperty(opac, new("Opacity"));

            DoubleAnimation trananimation = new() { EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut },From=-5,To=-100 , Duration = TimeSpan.FromSeconds(0.8) };
            Storyboard.SetTargetProperty(trananimation, new("RenderTransform.(TransformGroup.Children)[0].(TranslateTransform.Y)"));
            Storyboard.SetTarget(trananimation, this);
            story.Children.Add(opac);
            story.Children.Add(trananimation);
        }
        else  if(state ==0)
        {
            DoubleAnimation opac = new DoubleAnimation() { From = 0, To = 1, Duration = TimeSpan.FromSeconds(0.5) };
            Storyboard.SetTarget(opac, this);
            Storyboard.SetTargetProperty(opac, new("Opacity"));
            DoubleAnimation trananimation = new() { EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut }, From = -100, To = -5,Duration = TimeSpan.FromSeconds(0.8) };
            Storyboard.SetTargetProperty(trananimation, new("RenderTransform.(TransformGroup.Children)[0].(TranslateTransform.Y)"));
            Storyboard.SetTarget(trananimation, this);
            story.Children.Add(opac);
            story.Children.Add(trananimation);
        }
        return story;
    }

}
