using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace SonsOfTheForesrtSaveLib.SinglePlayer;
public class ConfigurationData: SaveBase {

    readonly string ConfigFileName = "PlayerStateSaveData.json";

    public Task<ModelBase<PlayerStateDataModel>> GetSave() => ReadSave(ConfigFileName);

    public ConfigurrationResultData FormatSave<T>(ModelBase<T> model)
        where T: IData 
    {
        return JsonSerializer.Deserialize<ConfigurrationResultData>(model.Data.Data);
    }
}
