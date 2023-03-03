using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SonsOfTheForesrtSaveLib.Models;
public class PackageData {
    [JsonPropertyName("PackageResource")] public List<PackageDataItem> Items { get; set; }
}

public class PackageDataItem {
    [JsonPropertyName("name")]public string Name { get; set; }
    [JsonPropertyName("ID")]public int ID { get; set; }
}
