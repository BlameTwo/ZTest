using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SimpleUI.Controls;

[TemplatePart(Name ="PART_Image",Type =typeof(Image))]
public class PersonPicture : ContentControl
{

    public PersonPicture()
    {
        StyleProperty.OverrideMetadata(typeof(PersonPicture), new FrameworkPropertyMetadata());
    }
    
	public string Size
	{
		get { return (string)GetValue(SizeProperty); }
		set { SetValue(SizeProperty, value); }
	}

	public static readonly DependencyProperty SizeProperty =
		DependencyProperty.Register("Size", typeof(string), typeof(PersonPicture), new PropertyMetadata(null,SizeChanged));

	private Image _image;
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
		_image = (GetTemplateChild("PART_Image") as Image)!;
		if (_image == null) return;
        _image.ImageFailed += _image_ImageFailed;
    }

    private void _image_ImageFailed(object? sender, ExceptionRoutedEventArgs e)
    {
        this.RaiseEvent(new RoutedExceptionEventArgs(ErrorImageEvent, this, e.ErrorException));
    }




    public ImageSource Source
    {
        get { return (ImageSource)GetValue(SourceProperty); }
        set { SetValue(SourceProperty, value); }
    }

    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register("Source", typeof(ImageSource), typeof(PersonPicture), new FrameworkPropertyMetadata(null));



    public static readonly RoutedEvent ErrorImageEvent = EventManager.RegisterRoutedEvent("ErrorImage", RoutingStrategy.Bubble, typeof(EventHandler<RoutedExceptionEventArgs>),typeof(PersonPicture));

    private static void SizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PersonPicture avatar)
        {
            if (double.TryParse(e.NewValue?.ToString(), out double size))
            {
                avatar.Width = avatar.Height = size;
            }
        }
    }


    public Stretch Stretch
	{
		get { return (Stretch)GetValue(StretchProperty); }
		set { SetValue(StretchProperty, value); }
	}

	// Using a DependencyProperty as the backing store for Stretch.  This enables animation, styling, binding, etc...
	public static readonly DependencyProperty StretchProperty =
		DependencyProperty.Register("Stretch", typeof(Stretch), typeof(PersonPicture), new FrameworkPropertyMetadata(Stretch.Uniform));



	public CornerRadius CornerRadius
	{
		get { return (CornerRadius)GetValue(CornerRadiusProperty); }
		set { SetValue(CornerRadiusProperty, value); }
	}

	// Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
	public static readonly DependencyProperty CornerRadiusProperty =
		DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(PersonPicture), new FrameworkPropertyMetadata(new CornerRadius(0)));


	public Shape Shape
    {
		get { return (Shape)GetValue(ShapeProperty); }
		set { SetValue(ShapeProperty, value); }
	}

	// Using a DependencyProperty as the backing store for Share.  This enables animation, styling, binding, etc...
	public static readonly DependencyProperty ShapeProperty =
		DependencyProperty.Register("Shape", typeof(Shape), typeof(PersonPicture), new FrameworkPropertyMetadata(Shape.Circle));




}


public enum Shape
{
    Circle,
    Square,
}

public class RoutedExceptionEventArgs : RoutedEventArgs
{
    public RoutedExceptionEventArgs(
        RoutedEvent routedEvent,
        object sender,
        Exception errorException
        ) : base(routedEvent, sender)
    {
        if (errorException == null)
        {
            throw new ArgumentNullException("errorException");
        }

        ErrorException = errorException;
    }

    /// <summary>
    /// The exception that describes the media failure.
    /// </summary>
    public Exception ErrorException { get; private set; }
}