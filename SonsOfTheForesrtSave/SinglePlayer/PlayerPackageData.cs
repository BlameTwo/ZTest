using System.IO;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using System.Text.Json.Nodes;

namespace SonsOfTheForesrtSaveLib.SinglePlayer;

/// <summary>
/// 背包信息
/// </summary>
public class PlayerPackageData {

    internal string GameWroldFileName = "PlayerInventorySaveData.json";
    public async Task<ModelBase<PlayerPackageModelData>> ReadSave() {
        var str = File.ReadAllText(Path.Combine(SaveConfig.DirPath, GameWroldFileName));
        byte[] array = Encoding.ASCII.GetBytes(str);
        MemoryStream stream = new MemoryStream(array);
        StreamReader reader = new StreamReader(stream);
        return await JsonSerializer.DeserializeAsync<ModelBase<PlayerPackageModelData>>(stream);
    }

    public PlayerInventoryData FormatData(PlayerPackageModelData data) {
        return JsonSerializer.Deserialize<PlayerInventoryData>(JsonObject.Parse(data.Data));
    }
}
