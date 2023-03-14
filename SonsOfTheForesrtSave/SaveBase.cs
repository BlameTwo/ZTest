using System.IO;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace SonsOfTheForesrtSaveLib;
public class SaveBase
{
    internal  virtual Stream ReadSave(string filename) {
        var str = File.ReadAllText(Path.Combine(SaveConfig.DirPath, filename));
        byte[] array = Encoding.ASCII.GetBytes(str);
        MemoryStream stream = new MemoryStream(array);
        return stream ;
    }
}
