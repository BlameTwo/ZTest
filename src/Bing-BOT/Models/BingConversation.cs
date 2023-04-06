using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Bing_BOT.Models;

public class BingConversation
{
    public string conversationId { get; set; }

    public string clientId { get; set; }

    public string conversationSignature { get; set; }

    [JsonPropertyName("result")]
    public JsonNode Result { get; set; }
}

