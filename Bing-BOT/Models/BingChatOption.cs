using Bing_BOT.Enum;

namespace Bing_BOT.Models;

public class BingChatOption
{
    public CharStyle ChatStyle { get; set; }

    public string Token { get; set; }

    public double TimeQuit { get; set; } = 60;

    public BingChatOption(CharStyle chatStyle, string token, double timeQuit)
    {
        ChatStyle = chatStyle;
        Token = token;
        TimeQuit = timeQuit;
    }
}
