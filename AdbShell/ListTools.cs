using AdbShell.Models;
using System.Collections.Generic;

namespace AdbShell;

public static class ListTools
{
    /// <summary>
    /// 检查设备是否存在,不存在返回真，存在返回假
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool IsContainsKey(this List<DeviceStateData> values,string name)
    {
        List<DeviceStateData> data = new();
        foreach (var item in values)
        {
            //如果数据存在，也就是说新数据中也有
            if (item.DeviceName == name) continue;
            else
            {
                //新数据中不存在添加
                data.Add(item);
            }
        }
        if (data.Count == 0)
            return true;
        else
            return false;
    }
}
