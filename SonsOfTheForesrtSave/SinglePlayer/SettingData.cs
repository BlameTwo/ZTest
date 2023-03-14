using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SonsOfTheForesrtSaveLib.SinglePlayer {
    public class SettingData: SaveBase {
        readonly string ConfigFileName = "GameSetupSaveData.json";

        public async Task<ModelBase<GameSetupDataModel>> GetSave() {
            return await Task.Run(async () => {
                return await JsonSerializer.DeserializeAsync<ModelBase<GameSetupDataModel>>(ReadSave(ConfigFileName));
            });
        }


        public SettingResultData FormatSave<T>(ModelBase<T> model)
        where T : IData {
            return JsonSerializer.Deserialize<SettingResultData>(model.Data.Data);
        }


        public void WriteSave(ModelBase<GameSetupDataModel> model,SettingResultData data) {

        }
    }
}
