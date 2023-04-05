using ChatGPT_GUI.Bing_Bot.Models;

namespace ChatGPT_GUI.Bing.Models;

public record BingRequest(string request)
{
    public ConversationSession Session { get; set; }
};