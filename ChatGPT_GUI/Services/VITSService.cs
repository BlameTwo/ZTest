using Microsoft.Win32;
using SkiaSharp;
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

    public async Task<Stream> GetStream(string text)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("http://vits.natapp1.cc/sovits/genByText"),
            Content = JsonContent.Create(new
            { uId = "3014085426", token = "a6fbd666-1e71-4e32-85f4-f16fc389b02e", mId = "3", rId = "119", text = $"[ZH]{text}[ZH]" }
            ),
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var stream = JsonObject.Parse(body);
            var url = stream!["data"]!.ToString();
            if(url.StartsWith("http://") ||url.StartsWith("https://"))
            {
                var result = await client.GetAsync(url);
                var resultstream = await result.Content.ReadAsStreamAsync();
                return resultstream;
            }
        }
        return null;
    }

    public async void SaveWAV(bool isopen, Stream Base64)
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
        MemoryStream ms = new MemoryStream(); //创建一个空的MemoryStream对象
        byte[] buffer = new byte[Base64.Length]; //创建一个和stream长度相同的字节数组
        ms.Write(buffer, 0, buffer.Length); //将字节数组写入MemoryStream对象中
        ms.Close(); //关闭MemoryStream

        FileStream fs = new FileStream(filename, FileMode.Create);
        await ms.CopyToAsync(fs);
        fs.Close();
        Base64.Close();
    }
}
