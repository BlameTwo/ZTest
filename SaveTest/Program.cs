using RestSharp;
using SonsOfTheForesrtSaveLib;
using RestSharp.Authenticators;
using System.Net.Http.Headers;

SaveConfig.DirPath = "C:\\Users\\30140\\AppData\\LocalLow\\Endnight\\SonsOfTheForest\\Saves\\76561198956487770\\SinglePlayer\\0304034487";


//PlayerStateData playerStateData = new PlayerStateData();
//var result = await playerStateData.ReadSave();
//var result2 = playerStateData.FormatData(result);
//result2.Data[0].Name = "Testing";
//playerStateData.WriteSave(result,result2);


//GameWorldStateData gameworld = new();
//var json = await gameworld.ReadSave();
//var json2 = gameworld.FormatData(json.Data);
//json2.GameDay = 18;
//gameworld.WriteSave(json, json2);

//var res = await PackageManager.GetPackage();
//PlayerPackageData data = new();
//var result = await data.ReadSave();
//var data2 = data.FormatData(result.Data);

//ConfigurationData config = new();
//var result = await config.GetSave();
//var result2 = config.FormatSave(result);

using (var httpClient = new HttpClient())
{
    using (var request = new HttpRequestMessage(new HttpMethod("Post"), "http://osu.natapp1.cc/vits/getAI/3014085426/a6fbd666-1e71-4e32-85f4-f16fc389b02e/3/119/[ZH]你好，我是胡桃[ZH]"))
    {
        request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
        request.Content = new StringContent("'");
        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

}

