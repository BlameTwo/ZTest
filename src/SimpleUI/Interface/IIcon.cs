using static SimpleUI.Utils.Controls.IconResources;

namespace SimpleUI.Interface
{
    public interface IIcon
    {
        /// <summary>
        /// Unicode字符
        /// </summary>
        string Glyph { get; set; }

        object IconImage { get; set; }
    }
}
