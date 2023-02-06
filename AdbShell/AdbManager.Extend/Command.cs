using AdbShell.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace AdbShell;

public partial class AdbManager
{
    

    private async Task<AdbCommandResult> connect(Process process, CancellationToken token, string connectname)
    {
        return await Task.Run(async () =>
        {
            if (token.IsCancellationRequested) return new AdbCommandResult() { Success = false, Message = "已经取消操作" };
            if (process == null) return new AdbCommandResult() { Success = false, Message = "已经取消操作" };
            string errormessage = null;
            process.Start();
            while (process.StandardOutput.Peek() > -1)
            {
                if (token.IsCancellationRequested) return new() { Success = false, Message = "已经取消操作" };
                var str = await process.StandardOutput.ReadLineAsync();
                
                if (str.StartsWith("failed to authenticate")
                    || str.StartsWith("connect to"))
                {
                    return new AdbCommandResult() { Success = false, Message = "未检测到WSA在运行，请通过开始菜单启动WSA" };
                }
                else if (str.StartsWith("already connected"))
                {
                    _isConnect = true;
                    _connectname = connectname;
                    return new AdbCommandResult() { Success = true, Message = "WSA已经连接" };
                }
                else
                {
                    errormessage = str;
                }
            }
            return new() { Success = false, Message = $"Error:   {errormessage}" };
        });
    }
}
