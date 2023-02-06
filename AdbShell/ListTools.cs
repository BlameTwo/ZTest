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


    public static Dictionary<string, string> GetDefaultUsePermissons(this Dictionary<string,string> dict)
    {
        dict.Add("android.permission.ACCESS_CHECKIN_PROPERTIES", "允许读写访问”properties");
        dict.Add("android.permission.ACCESS_COARSE_LOCATION", "允许一个程序访问CellID或WiFi热点来获取粗略的位置");
        dict.Add("android.permission.ACCESS_FINE_LOCATION", "允许一个程序访问精良位置(如GPS) ");
        dict.Add("android.permission.ACCESS_LOCATION_EXTRA_COMMANDS", "允许应用程序访问额外的位置提供命令");
        dict.Add("android.permission.ACCESS_MOCK_LOCATION", "允许程序创建模拟位置提供用于测试");
        dict.Add("android.permission.ACCESS_NETWORK_STATE", "允许程序访问有关GSM网络信息");
        dict.Add("android.permission.ACCESS_SURFACE_FLINGER", "允许程序使用SurfaceFlinger底层特性");
        return dict;

    }
}
