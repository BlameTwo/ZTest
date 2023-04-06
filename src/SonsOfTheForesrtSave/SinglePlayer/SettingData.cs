using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SonsOfTheForesrtSaveLib.SinglePlayer
{
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


        public void WriteSave(ModelBase<GameSetupDataModel> database,SettingResultData model) {

            var modeldata = JsonSerializer.Serialize<SettingResultData>(model);
            database.Data.Data = modeldata;
            var result = JsonSerializer.Serialize(database);
            File.WriteAllText(Path.Combine(SaveConfig.DirPath, ConfigFileName), result, Encoding.UTF8);
        }
    }
}
