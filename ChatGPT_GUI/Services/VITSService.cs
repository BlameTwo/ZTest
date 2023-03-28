using Microsoft.Win32;
using System;
using System.IO;
using System.Media;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using ZTest.Tools;
namespace ChatGPT_GUI.Services;

public class VITSService : IVITSService
{
    public string SavePath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

    public async Task<string> GetBase64(string text)
    {
        string txt = text.Length > 35 ? text.Substring(0, 34) : text;
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("http://osu.natapp1.cc/vits/getAI2/"),
            Content = JsonContent.Create(new
            { uId = "3014085426", token = "a6fbd666-1e71-4e32-85f4-f16fc389b02e", mId = "3", rId = "119", text = $"[ZH]{txt}[ZH]" }
            ),
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }
    }

    public void SaveWAV(bool isopen, MemoryStream Base64)
    {
        string filename = "";
        if(isopen)
        {
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.FileName = "Wav File(*.wav)|*.wav";
            if (saveFileDialog.ShowDialog() == false) return;
            filename = saveFileDialog.FileName;
        }
        else 
        {
            filename = SavePath + "\\output.wav";
        }

        FileStream fs = new FileStream(filename, FileMode.Create);
        Base64.WriteTo(fs);
        fs.Close();
        Base64.Close();
    }
}
