using SimpleUI.Interface;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SimpleUI.Services
{
    public interface INavigationViewService: IControl
    {

        /// <summary>
        /// 菜单的图片颜色，默认为null
        /// </summary>
        ImageSource MenuBackImage { get; set; }

        /// <summary>
        /// 默认的导航Items
        /// </summary>
        List<INavigationItemServices> MenuItems { get; set; }

        /// <summary>
        /// 底部的导航Items
        /// </summary>
        List<INavigationItemServices> FooterMenuItems { get; }

        /// <summary>
        /// 当前选中的SelectionItem
        /// </summary>
        INavigationItemServices SelectionItem { get; set; }

        /// <summary>
        /// 头标
        /// </summary>
        object NavigationHeader { get; set; }

        /// <summary>
        /// 布局头标
        /// </summary>
        string PanelTitle { get; set; }

    }
}