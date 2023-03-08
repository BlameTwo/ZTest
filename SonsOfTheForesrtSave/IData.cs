using System.Text.Json.Serialization;

namespace SonsOfTheForesrtSaveLib;
public interface IData {
    string Data { get; set; }
}

public class PlayerStateDataModel : IData {
   [JsonPropertyName("PlayerState")]public string Data { get; set; }
}

