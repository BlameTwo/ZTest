using System.Diagnostics;
using System.IO;

namespace AdbShell;

public class ProcessManager
{
    public string Adbpath { get; set; }

    public Process GetProcess(string parame)
    {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo()
        {
            FileName = Adbpath,
            Arguments = parame,
            UseShellExecute = false,
            StandardErrorEncoding = System.Text.Encoding.UTF8,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow=true, WorkingDirectory = Path.GetDirectoryName(Adbpath)
        };
        return process;
    }
}
