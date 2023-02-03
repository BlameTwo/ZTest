using SimpleUI.Interface;
using System;
using static SimpleUI.Utils.Controls.IconResources;

namespace SimpleUI.Services
{
    public interface INavigationItemServices:IControl
    {
        string Tag { get; set; }

        FontIconEnum Icon { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        bool IsActive { get; set; }


        /// <summary>
        /// 对于某个Page的名称
        /// </summary>
        Type TypeUri { get; set; }
    }
}