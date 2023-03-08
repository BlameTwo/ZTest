using System.IO;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace SonsOfTheForesrtSaveLib;
public class SaveBase
{
    internal async virtual Task<ModelBase<PlayerStateDataModel>> ReadSave(string filename) {
        var str = File.ReadAllText(Path.Combine(SaveConfig.DirPath, filename));
        byte[] array = Encoding.ASCII.GetBytes(str);
        MemoryStream stream = new MemoryStream(array);
        StreamReader reader = new StreamReader(stream);
        return await JsonSerializer.DeserializeAsync<ModelBase<PlayerStateDataModel>>(stream);
    }
}
