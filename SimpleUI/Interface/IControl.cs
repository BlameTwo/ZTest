using System.Windows;

namespace SimpleUI.Interface
{
    /// <summary>
    /// SimpleUI的控件的基础接口
    /// </summary>
    public interface IControl
    {
        /// <summary>
        /// 控件 的圆角属性
        /// </summary>
        CornerRadius CornerRadius { get; set; }
    }
}
