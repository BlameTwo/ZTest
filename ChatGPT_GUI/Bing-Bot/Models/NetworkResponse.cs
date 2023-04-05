using ChatGPT_GUI.Bing_Bot.Models;

namespace ChatGPT_GUI.Bing_Bot.Models;

public class NetworkResponse
{
    public int type { get; set; }
    
    public string target { get; set; }
    
    public NetworkResponseItem item { get; set; }
}