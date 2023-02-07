using System.Collections.Generic;
using WSA_ADBShell.ViewModels.ItemsViewModels;

namespace WSA_ADBShell.Services.Interfaces;

public interface ITaskManager
{
    public List<ITaskToken> TaskManagers { get; set; }

    /// <summary>
    /// 创建Token
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    /// <returns></returns>
    public string CraeteToken<T>(T item)
        where T : ITaskToken;

    /// <summary>
    /// 移除Token
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool RemoveToken<T>(T item)
        where T : ITaskToken;
}
