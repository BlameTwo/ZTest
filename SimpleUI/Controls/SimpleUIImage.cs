using SimpleUI.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace SimpleUI.Controls;

public class SimpleUIImage:Control
{
    public BitmapSource Source
    {
        get { return (BitmapSource)GetValue(SourceProperty); }
        set { SetValue(SourceProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register("Source", typeof(BitmapSource), typeof(SimpleUIImage), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public void OpenUri(Uri uri)
    {
        //OpenUri之后可使用动画
        BitmapImage bitmapimage = new(uri);
        bitmapimage.DownloadCompleted += (s, e) =>
        {
            switch (this.Animation)
            {
                case ImageAnimationType.Opacity:
                    this.Source = bitmapimage;
                    DoubleAnimation da = new DoubleAnimation() { From = 0, To = 1 };
                    this!.BeginAnimation(Border.OpacityProperty, da);
                    break;
                case ImageAnimationType.Default:
                    break;
                case ImageAnimationType.XYO:
                    break;
            }
        };
    }


    public void OpenLocal(string path)
    {
        BitmapImage bitmap = new(new(path));
        this.Source = bitmap;   
    }





    public ImageAnimationType Animation
    {
        get { return (ImageAnimationType)GetValue(AnimationProperty); }
        set { SetValue(AnimationProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Animation.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty AnimationProperty =
        DependencyProperty.Register("Animation", typeof(ImageAnimationType), typeof(SimpleUIImage), new PropertyMetadata(ImageAnimationType.Default));





    public SimpleUIImage()
    {

    }


    public Stretch Stretch
    {
        get { return (Stretch)GetValue(StretchProperty); }
        set { SetValue(StretchProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Stretch.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty StretchProperty =
        DependencyProperty.Register("Stretch", typeof(Stretch), typeof(Stretch), new FrameworkPropertyMetadata(Stretch.Uniform, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));



    public StretchDirection StretchDirection
    {
        get { return (StretchDirection)GetValue(StretchDirectionProperty); }
        set { SetValue(StretchDirectionProperty, value); }
    }

    // Using a DependencyProperty as the backing store for StretchDirection.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty StretchDirectionProperty =
        DependencyProperty.Register("StretchDirection", typeof(StretchDirection), typeof(SimpleUIImage), new FrameworkPropertyMetadata(Stretch.Uniform, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));





    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(SimpleUIImage), new PropertyMetadata(new CornerRadius(0),(s,e)=>OnCornerRadiusChanged(s,e)));


    private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var thickness = (Thickness)d.GetValue(BorderThicknessProperty);
        var outerRarius = (CornerRadius)e.NewValue;

        //Inner radius = Outer radius - thickenss/2
        d.SetValue(CornerRadiusProperty,
            new CornerRadius(
                topLeft: Math.Max(0, (int)Math.Round(outerRarius.TopLeft - thickness.Left / 2, 0)),
                topRight: Math.Max(0, (int)Math.Round(outerRarius.TopRight - thickness.Top / 2, 0)),
                bottomRight: Math.Max(0, (int)Math.Round(outerRarius.BottomRight - thickness.Right / 2, 0)),
                bottomLeft: Math.Max(0, (int)Math.Round(outerRarius.BottomLeft - thickness.Bottom / 2, 0)))
        );
    }

}
