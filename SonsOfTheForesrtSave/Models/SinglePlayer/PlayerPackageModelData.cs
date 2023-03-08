using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SonsOfTheForesrtSaveLib.Models.SinglePlayer;

public class PlayerPackageModelData : IData {
    [JsonPropertyName("PlayerInventory")] public string Data { get; set; }
}

public class PlayerInventoryData {
    [JsonPropertyName("Version")]public string Version { get; set; }

    [JsonPropertyName("QuickSelect")]public QuickSelect QuickSelect { get; set; }

    [JsonPropertyName("EquippedItems")] public List<EquippedItem> EquippedItems { get; set; }

    [JsonPropertyName("ItemInstanceManagerData")] public ItemInstanceManager ItemInstanceManager { get; set; }
}

public class ItemInstanceManager {
    [JsonPropertyName("Version")]public string Version { get; set; }

    [JsonPropertyName("ItemBlocks")] public List<ItemBlock> ItemBlocks { get; set; }

}


public class ItemBlock {
    [JsonPropertyName("ItemId")]public int ItemID { get; set; }

    [JsonPropertyName("TotalCount")]public int TotalCount { get; set; }

    [JsonPropertyName("UniqueItems")] public List<UniqueItem> UniqueItems { get; set;    }
}

public class UniqueItem {

    [JsonPropertyName("Modules")] public List<EquippedModulesItem> EquippedModulesItems { get; set; }
}

public class EquippedItem {
    [JsonPropertyName("ItemId")]public int ItemID { get; set; }

    [JsonPropertyName("Modules")] public List<EquippedModulesItem> EquippedModulesItems { get; set; }
}

public class EquippedModulesItem {
    [JsonPropertyName("Version")]public string Version { get; set; }

    [JsonPropertyName("ModuleId")]public int ModuleId { get; set; }

    [JsonPropertyName("AmmoType")]public int AmmoType { get; set; }

    [JsonPropertyName("CurrentAmmo")]public int CurrentAmmo { get; set; }

    [JsonPropertyName("WeaponModIds")]public object WeaponModIDS { get; set; }


    [JsonPropertyName("ChannelWeights"),JsonConverter(typeof(ChannelWeightsConverter))]
    public ChannelWeights ChannelWeights { get; set; }
}



public class ChannelWeights {
    [JsonConverter(typeof(DoubleConverter))]
    [JsonNumberHandling(JsonNumberHandling.AllowNamedFloatingPointLiterals)]
    [JsonPropertyName("x")]
    public double x { get; set; }

    [JsonConverter(typeof(DoubleConverter))]
    [JsonNumberHandling(JsonNumberHandling.AllowNamedFloatingPointLiterals)]
    [JsonPropertyName("y")]
    public double y { get; set; }

    [JsonConverter(typeof(DoubleConverter))]
    [JsonNumberHandling(JsonNumberHandling.AllowNamedFloatingPointLiterals)]
    [JsonPropertyName("z")] 
    public double z { get; set; }

    [JsonConverter(typeof(DoubleConverter))]
    [JsonNumberHandling(JsonNumberHandling.AllowNamedFloatingPointLiterals)]
    [JsonPropertyName("w")] 
    
    public double w { get; set; }
}

public class DoubleConverter : JsonConverter<double> {
    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        var element = JsonDocument.ParseValue(ref reader).RootElement;
        //过滤\符号获取原Json数据
        string value = element.GetRawText().Replace(@"\","");
        //转换double
        return reader.GetDouble();
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options) {
        //直接写入原数据
        var result = value.ToString("F1");
        writer.WriteRawValue(result);
        
    }
}



public class ChannelWeightsConverter : JsonConverter<ChannelWeights> {
    public override ChannelWeights Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        var result = JsonSerializer.Deserialize<ChannelWeights>(ref reader, new JsonSerializerOptions() { WriteIndented = true }) ;
        return result;
    }
    
    public override void Write(Utf8JsonWriter writer, ChannelWeights value, JsonSerializerOptions options) {
        var result = JsonSerializer.Serialize(value, typeof(ChannelWeights));
        writer.WriteRawValue(result);
    }
}

public class QuickSelect {

    /// <summary>
    /// 快捷键栏物品
    /// </summary>
    [JsonPropertyName("Slots")] public List<Slots> Slots { get; set; }

    [JsonPropertyName("Version")]public string Version { get; set; }
}

public class Slots {
    public int ItemId { get; set; }

    public double Age { get; set; }
}