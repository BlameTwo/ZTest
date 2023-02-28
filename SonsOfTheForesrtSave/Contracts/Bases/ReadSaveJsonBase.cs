using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SonsOfTheForesrtSaveLib.Contracts.Bases;

public class ReadSaveJsonBase
{
    public virtual async Task<ModelBase<PlayerStateDataModel>> ReadSave()
    {
        var str =new FileStream("PlayerStateSaveData.json",FileMode.Open,FileAccess.Read);
        return await JsonSerializer.DeserializeAsync<ModelBase<PlayerStateDataModel>>(str);
    }

}
