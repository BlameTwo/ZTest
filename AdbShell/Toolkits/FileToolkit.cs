using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace AdbShell.Toolkits;

public static class FileToolkit
{
    public async static Task<string> ReadString(this ZipArchiveEntry file)
    {
        using StreamReader reader = new(file.Open());
        return await reader.ReadToEndAsync();
    }
}
