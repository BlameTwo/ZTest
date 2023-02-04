using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SimpleUI.Interface.AppInterfaces;

public interface IToastLitterMessage
{
    /// <summary>
    /// 他的Parent元素
    /// </summary>
    public Panel ShowOwner { get; set; }

    /// <summary>
    /// 显示时间
    /// </summary>

    public TimeSpan ShowTime { get; set; }

    /// <summary>
    /// 显示方法
    /// </summary>
    /// <param name="message"></param>
    public void Show(string message);
}
