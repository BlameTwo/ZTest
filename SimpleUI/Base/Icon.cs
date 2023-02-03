using SimpleUI.Interface;
using System.Windows;
using System.Windows.Controls;
using static SimpleUI.Utils.Controls.IconResources;

namespace SimpleUI.Base
{
    public class Icon: Control,IIcon
    {
        static Icon()
        {

        }

        public string Glyph
        {
            get { return (string)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Glyph.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GlyphProperty =
            DependencyProperty.RegisterAttached("Glyph", typeof(string), typeof(Icon), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.Inherits));


    }
}
