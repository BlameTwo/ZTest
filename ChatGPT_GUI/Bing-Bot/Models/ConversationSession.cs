namespace ChatGPT_GUI.Bing_Bot.Models;

public record ConversationSession(int InvocationId, string ConversationId, string ClientId, string Signature);