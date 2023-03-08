using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SonsOfTheForesrtSaveLib.Models.SinglePlayer;

public class ConfigurrationResultData {
    [JsonPropertyName("_entries")]public List<ConfigurrationResultDataItem> Items { get; set; }
}

public class ConfigurrationResultDataItem {
    [JsonPropertyName("Name")]public string Name { get; set; }

    [JsonPropertyName("SettingType")]public int SettingType { get; set; }

    [JsonPropertyName("Version")]public int Version { get; set; }

    [JsonPropertyName("IntValue")]public int IntValue { get; set; }


    [JsonPropertyName("FloatValue")]
    [JsonConverter(typeof(DoubleConverter))]
    [JsonNumberHandling(JsonNumberHandling.AllowNamedFloatingPointLiterals)]
    public double FloatValue { get; set; }

    [JsonPropertyName("StringValue")]
    public string StringValue { get; set; }

    [JsonPropertyName("Protected")]
    public bool Protected { get; set; }

    [JsonPropertyName("FloatArrayValue")]public List<double> FloatArrayValue { get; set; }

    [JsonPropertyName("IsSet")]public bool IsSet { get; set; }
}
