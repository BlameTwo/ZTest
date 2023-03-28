using System.IO;
using System.Threading.Tasks;

namespace ChatGPT_GUI.Services;

public interface IVITSService
{
    /// <summary>
    /// 获得语音Base64
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public  Task<string> GetBase64(string text);


    /// <summary>
    /// 默认保存位置
    /// </summary>
    public string SavePath { get; set; }

    /// <summary>
    /// 保存语音
    /// </summary>
    /// <param name="isopen">是否自主选择</param>
    public void SaveWAV(bool isopen,MemoryStream Base64);
}
