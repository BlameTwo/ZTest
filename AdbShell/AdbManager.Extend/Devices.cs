using AdbShell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdbShell;


//此代码是为管理多个设备准备

//TODO:多设备监听待修改
public partial class AdbManager
{
    public async Task<AdbDataResult<List<DeviceStateData>>> GetDevicesList()
    {
        return await Task.Run(async () =>
        {
            AdbDataResult<List<DeviceStateData>> listData = new() { Data = new() };
            var process = ProcessManager.GetProcess("devices", ProcessType.Adb);
            process.Start();
            process.WaitForExit();
            while (process.StandardOutput.Peek() > -1)
            {
                DeviceStateData data = new();
                var str = await process.StandardOutput.ReadLineAsync();
                if (str.IndexOf("\t")! == -1) continue;
                string[] strs = str.Split('\t');
                foreach (string s in strs)
                {
                    if (s != "device" && s != "offline" && s != "unknown")
                        data.DeviceName = s;
                    else
                        data.State = s;
                }
                if (data.State == null && data.DeviceName == null) continue;
                listData.Data.Add(data);
            }
            if (listData.Data.Count > 0) listData.Success = true;
            return listData;
        });

    }

    public System.Threading.Timer timer=null;
    public static object deviceslocker=new object();

    Dictionary<string,string> _devicelist=new();
    private void refershdevicecount(object state)
    {
        lock(deviceslocker)
        {
            var datalist = GetDevicesList().Result;
            if (datalist == null) return;
            List<DeviceStateData> ErrorList = new();
            if (datalist.Data.Count == 0 && DevicesChangedHandle!= null)
            {
                DevicesChangedHandle.Invoke(this, new DevicesChangedArg() { State = DevicesChangedEnum.ClearAll });
                _devicelist.Clear();
            }
            foreach (var item in datalist.Data.ToArray())
            {
                if (!_devicelist.ContainsKey(item.DeviceName))
                {
                    _devicelist.Add(item.DeviceName, item.State);
                    if (DevicesChangedHandle == null) continue;
                    DevicesChangedHandle.Invoke(this, new DevicesChangedArg()
                    {
                        State = DevicesChangedEnum.Add,
                        Data = new List<DeviceStateData>() { item }
                    });
                }
                else if (_devicelist.ContainsKey(item.DeviceName))
                {
                    string value = _devicelist[item.DeviceName];
                    if (DevicesChangedHandle == null) continue;
                    if (item.State != value)
                    {
                        _devicelist[item.DeviceName] = item.State;
                        //此设备有变化
                        DevicesChangedHandle.Invoke(this, new DevicesChangedArg()
                        {
                            State = DevicesChangedEnum.Changed,
                            Data = new List<DeviceStateData>() { item }
                        });
                    }
                }

                //如果旧数据中存在
                bool flage = _devicelist.ContainsKey(item.DeviceName)
                    //新数据中不存在
                    && datalist.Data.IsContainsKey(item.DeviceName);

                if (flage)
                {
                    foreach (var item2 in _devicelist.ToArray())
                    {
                        if (DevicesChangedHandle == null) continue;
                        if (item2.Key == item.DeviceName)
                            continue;
                        else
                        {
                            _devicelist.Remove(item2.Key);
                            DevicesChangedHandle.Invoke(this, new DevicesChangedArg()
                            {
                                State = DevicesChangedEnum.Remove,
                                Data = new List<DeviceStateData>() { new DeviceStateData() { DeviceName = item2.Key,State = item2.Value } }
                            });
                        }
                    }
                }
            }
        }
    }
    

    private async void DevicesInit()
    {
        var list = (await GetDevicesList()).Data;
        if (_devicelist == null)
        {
            _devicelist = new();
            list.ForEach((val) => _devicelist.Add(val.DeviceName,val.State));
            return;
        }
        if (DevicesChangedHandle == null) return;
        DevicesChangedHandle.Invoke(this, new DevicesChangedArg() { State = DevicesChangedEnum.Add,Data=list});
    }

    public delegate void DevicesChanged(AdbManager adbManager, DevicesChangedArg deviceStateData);

    public DevicesChanged DevicesChangedHandle { get; private set; }

    /// <summary>
    /// 监听设备数量更改
    /// </summary>
    public event DevicesChanged DevicesCountChanged
    {
        add { DevicesChangedHandle += value; }
        remove { DevicesChangedHandle -= value; }
    }
}

public class DevicesChangedArg:EventArgs
{
    //更改数据
    public List<DeviceStateData> Data { get; set; }

    /// <summary>
    /// 更改状态
    /// </summary>
    public DevicesChangedEnum State { get; set; }
}

public enum DevicesChangedEnum
{
    /// <summary>
    /// 增加
    /// </summary>
    Add,
    /// <summary>
    /// 删除
    /// </summary>
    Remove,
    /// <summary>
    /// 更改
    /// </summary>
    Changed,
    /// <summary>
    /// 移除全部
    /// </summary>
    ClearAll
}