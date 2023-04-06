using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text.Json;
using System.Threading.Tasks;

namespace SonsOfTheForesrtSaveLib.SinglePlayer;
public class ConfigurationData: SaveBase {

    readonly string ConfigFileName = "PlayerStateSaveData.json";

    public async Task<ModelBase<PlayerStateDataModel>> GetSave() {
        return await Task.Run(async () => {
            return await JsonSerializer.DeserializeAsync<ModelBase<PlayerStateDataModel>>(ReadSave(ConfigFileName));
        });
    }

    public ConfigurrationResultData FormatSave<T>(ModelBase<T> model)
        where T: IData 
    {
        return JsonSerializer.Deserialize<ConfigurrationResultData>(model.Data.Data);
    }

    public void WriteSave(ModelBase<PlayerStateDataModel> model, ConfigurrationResultData data) {
        var byt = JsonSerializer.Serialize(data);
        model.Data.Data = byt;
        var result = JsonSerializer.Serialize(model, typeof(ModelBase<PlayerStateDataModel>));
        File.WriteAllText(Path.Combine(SaveConfig.DirPath, ConfigFileName), result, System.Text.Encoding.UTF8);
    }
}
