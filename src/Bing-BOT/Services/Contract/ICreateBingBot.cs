using Bing_BOT.Models;

namespace Bing_BOT.Services.Contract
{
    public interface ICreateBingBot
    {
        public Task<BingConversation> CreateBingConversation(string cookie, CancellationToken token);
    }
}
