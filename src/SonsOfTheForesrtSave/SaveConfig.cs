using System.IO;

namespace SonsOfTheForesrtSaveLib;

public static class SaveConfig
{
    public static string DirPath { get; set; }

    public static string IconPath
    {
        get
        {
            return Path.Combine(DirPath, "SaveDataThumbnail.png");
        }
    }
}
