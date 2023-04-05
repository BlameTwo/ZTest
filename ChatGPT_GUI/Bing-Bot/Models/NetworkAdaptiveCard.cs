
using System.Collections.Generic;

namespace ChatGPT_GUI.Bing_Bot.Models;

public class NetworkAdaptiveCard
{
    public string Type { get; set; }
    
    public string Version { get; set; }
    
    public List<NetworkTextblock> Body { get; set; }
}