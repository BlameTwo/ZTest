using AdbShell.Interfaces;
using AdbShell.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdbShell;
//此代码是为管理多个设备准备


public partial class AdbManager
{
    public async Task<AdbDataResult<List<DeviceStateData>>> GetDevicesList()
    {
        return await Task.Run(async () =>
        {
            AdbDataResult<List<DeviceStateData>> listData = new() { Data=new() };
            var process = ProcessManager.GetProcess("devices");
            process.Start();
            process.WaitForExit();
            while (!process.StandardOutput.EndOfStream)
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
            if (listData.Data.Count>0) listData.Success= true;
            return listData;
        });
        
    }
}
