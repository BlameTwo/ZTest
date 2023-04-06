using RestSharp;
using SonsOfTheForesrtSaveLib;
using RestSharp.Authenticators;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Buffers.Text;
using System.Text.Json.Nodes;
using System.IO;

var client = new HttpClient();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Post,
    RequestUri = new Uri("http://vits.natapp1.cc/sovits/genByText"),
    Content = JsonContent.Create(new
    { uId = "3014085426", token = "a6fbd666-1e71-4e32-85f4-f16fc389b02e", mId = "3", rId = "119", text = $"[ZH]你好，我是胡桃,你喜欢我吗？？[ZH]" }
    ),
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    var stream = JsonObject.Parse(body);
    var url = stream!["data"]!.ToString();
    var result =  await client.GetAsync(url);
    var resultstream = await result.Content.ReadAsStreamAsync();
    var fileStream = File.Create("D:\\test.wav");
    resultstream.Seek(0, SeekOrigin.Begin);
    resultstream.CopyTo(fileStream);
    fileStream.Close();
}