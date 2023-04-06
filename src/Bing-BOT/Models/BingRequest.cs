namespace Bing_BOT.Models;

public record BingRequest(string message)
{
    public ConversationSession Session { get; set; }
}

public record ConversationSession(int InvocationId, string ConversationId, string ClientId, string Signature);
