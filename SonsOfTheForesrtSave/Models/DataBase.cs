using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SonsOfTheForesrtSaveLib.Models
{
    public class PlayerStateDataItem
    {
        [JsonPropertyName("Name")]public string Name { get; set; }

        [JsonPropertyName("SettingType")]public int SettingType { get; set; }

        [JsonPropertyName("Version")]public int Version { get; set; }

        [JsonPropertyName("BoolValue")]public bool BoolValue { get; set; }

        [JsonPropertyName("IntValue")]public int IntValue { get; set; }

        [JsonPropertyName("FloatValue")]public float FloatValue { get; set; }

        [JsonPropertyName("StringValue")]public string StringValue { get; set; }

        [JsonPropertyName("Protected")]public bool Protected { get; set; }

        [JsonPropertyName("IsSet")]public bool IsSet { get; set; }

        [JsonPropertyName("FloatArrayValue")]public IEnumerable<float> FloatArrayValue { get; set; }
    }
}
