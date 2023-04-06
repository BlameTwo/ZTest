using SonsOfTheForesrtSaveLib.Contracts.Bases;
using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace SonsOfTheForesrtSaveLib.SinglePlayer; 
public class GameWorldStateData: ReadSaveJsonBase {
    internal string GameWroldFileName = "GameStateSaveData.json";
   

    public async Task<ModelBase<GameWorldStateResultModel>> ReadSave() {
        var str = File.ReadAllText(Path.Combine(SaveConfig.DirPath,GameWroldFileName));
        byte[] array = Encoding.ASCII.GetBytes(str);
        MemoryStream stream = new MemoryStream(array);    
        StreamReader reader = new StreamReader(stream);
        return await JsonSerializer.DeserializeAsync<ModelBase<GameWorldStateResultModel>>(stream) ;
    }

    public GameWorldStateResultData FormatData(GameWorldStateResultModel model) 
    {
        var result = JsonSerializer.Deserialize<GameWorldStateResultData>(model.Data);
        return result;
    }

    public void WriteSave(ModelBase<GameWorldStateResultModel> model, GameWorldStateResultData data) {
        var byt = JsonSerializer.Serialize(data);
        ModelBase<GameWorldStateResultModel> modelbase = model;
        model.Data.Data = byt;
        var result = JsonSerializer.Serialize(model, typeof(ModelBase<GameWorldStateResultModel>));
        File.WriteAllText(Path.Combine(SaveConfig.DirPath,GameWroldFileName),result,System.Text.Encoding.UTF8);
    }
}
