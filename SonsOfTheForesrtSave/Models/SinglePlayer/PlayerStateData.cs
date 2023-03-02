using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SonsOfTheForesrtSaveLib.Models.SinglePlayer;

public class PlayerStateDataModel
{
    [JsonPropertyName("PlayerState")] public string StateData { get; set; }
}

public class PlayerStateDataEntries
{
    [JsonPropertyName("_entries")]public List<PlayerStateDataItem> Data { get; set; }
}

