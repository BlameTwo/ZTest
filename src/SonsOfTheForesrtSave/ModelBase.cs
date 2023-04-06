using System.Text.Json.Serialization;

namespace SonsOfTheForesrtSaveLib;

public class ModelBase<T>
{
    [JsonPropertyName("Version")]
    public string Version { get; set; }

    [JsonPropertyName("Data")]
    public T Data { get; set; }
}
