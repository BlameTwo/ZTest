﻿
using System.Collections.Generic;

namespace ChatGPT_GUI.Bing_Bot.Models;

internal class NetworkRequest
{
    public string invocationId { get; set; }

    public string target { get; set; } = "chat";

    public int type { get; set; } = 4;

    public List<NetworkRequestArgument> arguments { get; set; } = new();
}