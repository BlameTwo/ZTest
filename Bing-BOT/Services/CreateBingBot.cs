using Bing_BOT.Models;
using Bing_BOT.Services.Contract;
using RestSharp;
using System.Text.Json;

namespace Bing_BOT.Services
{
    public class CreateBingBot : ICreateBingBot
    {

        public async Task<BingConversation> CreateBingConversation(string cookie, CancellationToken token)
        {
            var client = new RestClient();
            var req = new RestRequest("https://www.bing.com/turing/conversation/create");
            req.AddHeader("sec-ch-ua", "\"Microsoft Edge\";v=\"111\", \"Not(A:Brand\";v=\"8\", \"Chromium\";v=\"111\"");
            req.AddHeader("sec-ch-ua-mobile", "?0");
            req.AddHeader("user-agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36");
            req.AddHeader("content-type", "application/json");
            req.AddHeader("accept", "application/json");
            req.AddHeader("sec-ch-ua-platform-version", "15.0.0");
            req.AddHeader("x-ms-client-request-id", Guid.NewGuid().ToString());
            req.AddHeader("x-ms-useragent", "azsdk-js-api-client-factory/1.0.0-beta.1 core-rest-pipeline/1.10.0 OS/Win32");
            req.AddHeader("referer", "https://www.bing.com/search?q=Bing+AI&qs=n&form=QBRE&sp=-1&lq=0");
            req.AddHeader("x-forwarded-for", "1.1.1.1");
            req.AddHeader("cookie", $"_U={cookie}");
            var res = await client.ExecuteAsync(req, token);
            if (string.IsNullOrEmpty(res.Content))
                return null;
            return JsonSerializer.Deserialize<BingConversation>(res.Content!)!;
        }
    }
}
