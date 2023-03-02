using SonsOfTheForesrtSaveLib.Contracts.Bases;
using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace SonsOfTheForesrtSaveLib.SinglePlayer;

/// <summary>
/// 此类主要操作和转换：PlayerStateSaveData.json
/// </summary>
public class PlayerStateData
{

    public async Task<ModelBase<PlayerStateDataModel>> ReadSave() {
        var str = new FileStream(Path.Combine(SaveConfig.DirPath, PlayerStateFileName), FileMode.Open, FileAccess.Read);
        return await JsonSerializer.DeserializeAsync<ModelBase<PlayerStateDataModel>>(str);
    }


    public PlayerStateDataEntries FormatData(ModelBase<Models.SinglePlayer.PlayerStateDataModel> data)
    {
        var ob = JsonObject.Parse(data.Data.StateData);
        var jo = JsonSerializer.Deserialize<PlayerStateDataEntries>(ob);
        return jo;
    }

    internal string PlayerStateFileName = "PlayerStateSaveData.json";

    public void WriteSave(ModelBase<PlayerStateDataModel> database, PlayerStateDataEntries playerStateDataEntries)
    {
        var byt = JsonSerializer.SerializeToUtf8Bytes(playerStateDataEntries,typeof(PlayerStateDataEntries));
        var str = System.Text.Encoding.UTF8.GetString(byt);
        ModelBase<PlayerStateDataModel> model = database;
        model.Data.StateData = str;
        var result = JsonSerializer.Serialize(database,typeof(ModelBase<PlayerStateDataModel>));

        File.WriteAllText(Path.Combine(SaveConfig.DirPath, PlayerStateFileName), Regex.Unescape(result));
    }
}
