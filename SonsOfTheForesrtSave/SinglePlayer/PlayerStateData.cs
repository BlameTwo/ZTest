using SonsOfTheForesrtSaveLib.Contracts.Bases;
using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

namespace SonsOfTheForesrtSaveLib.SinglePlayer;

/// <summary>
/// 此类主要操作和转换：PlayerStateSaveData.json
/// </summary>
public class PlayerStateData
{

    public async Task<ModelBase<PlayerStateDataModel>> ReadSave() {
        var str = File.ReadAllText(Path.Combine(SaveConfig.DirPath, PlayerStateFileName));
        byte[] array = Encoding.ASCII.GetBytes(str);
        MemoryStream stream = new MemoryStream(array);
        StreamReader reader = new StreamReader(stream);
        return await JsonSerializer.DeserializeAsync<ModelBase<PlayerStateDataModel>>(stream);
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
        model.Data.Data = str;
        var result = JsonSerializer.Serialize(database,typeof(ModelBase<PlayerStateDataModel>));

        File.WriteAllText(Path.Combine(SaveConfig.DirPath, PlayerStateFileName), Regex.Unescape(result));
    }
}
