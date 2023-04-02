using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPT_GUI.Bing_Bot.Interfaces;

public interface IBingChatConversation
{

    public Task<string> AskAsync(string message);
}
