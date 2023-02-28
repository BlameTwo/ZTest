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
    [JsonPropertyName("_entries")]public List<DataBaseModel> Data { get; set; }
}

public class PlayerStateConverter : JsonConverter<List<DataBaseModel>>
{
    public override List<DataBaseModel> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        List<DataBaseModel> list = new();
        var json = JsonObject.Parse(ref reader).AsArray();
        foreach (var item in json)
        {
            var it =   JsonSerializer.Deserialize<DataBaseModel>(item);
            list.Add(it);
        }
        return list;
    }

    public override void Write(Utf8JsonWriter writer, List<DataBaseModel> value, JsonSerializerOptions options)
    {

    }
}
