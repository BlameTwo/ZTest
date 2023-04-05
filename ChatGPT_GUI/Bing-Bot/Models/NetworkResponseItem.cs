using System.Collections.Generic;

namespace ChatGPT_GUI.Bing_Bot.Models;

public class NetworkResponseItem
{
    public List<NetworkMessage>? messages { get; set; }
    
    public int firstNewMessageIndex { get; set; }
    
    public string conversationId { get; set; }
    
    public string requestId { get; set; }
}