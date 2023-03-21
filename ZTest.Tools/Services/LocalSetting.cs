using System.Text.Json;
using ZTest.Tools.Interfaces;

namespace ZTest.Tools.Services {
    public class LocalSetting : ILocalSetting {
        public string LocalSettingFileName => "Setting.json";

        string filedir = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments);

        string file {
            get {
                return Path.Combine(filedir, LocalSettingFileName);
            }
        }

        public Dictionary<string, object> Config { get; set; }

        public async Task<bool> InitSetting() {
            if (!File.Exists(Path.Combine(filedir, LocalSettingFileName))) {
                await initWrite();
                return true;
            }
            string filestr = File.ReadAllText(file);
            if (!string.IsNullOrWhiteSpace(filestr)) {
                await refresh();
            }
            return true;
        }

        private async Task initWrite() {
            await Task.Run(() =>
            {
                File.WriteAllText(Path.Combine(filedir, LocalSettingFileName), "");
            });
        }

        async Task refresh() {
            await Task.Run(async () =>
            {
                StreamReader reader
                 = new(file);
                var str = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                if (!string.IsNullOrWhiteSpace(str))
                {
                    Config = JsonSerializer.Deserialize<Dictionary<string, object>>(str)!;
                }
                else
                    Config = new();
            });
        }

        public async Task<Object> ReadConfig(string key) {
            await refresh();
            if (Config.ContainsKey(key)) {
                object ob = null;
                Config.TryGetValue(key, out ob);
                if (ob is JsonElement json)
                    return Helper.JsonObjectHelper.ConvertObject(json);
                else
                    return null;
            }
            else {
                return default(object);
            }
        }


        public async Task<T> ReadObjectConfig<T>(string key) {
            await refresh();
            if (Config.ContainsKey(key)) {
                object ob = null;
                Config.TryGetValue(key, out ob);
                if (ob is JsonElement json && json.ValueKind == JsonValueKind.Object) {
                    return json.Deserialize<T>();
                }
                else {
                    throw new Exception("值类型请使用ReadConfig方法");
                }
            }
            else {
                return default(T);
            }
        }


        public async Task SaveConfig<T>(string key, T value) {
            await refresh();
            if (Config.ContainsKey(key)) {
                Config[key] = value;
            }
            else {
                Config.Add(key, value);
            }
            save();
        }

        public void save() {
            File.WriteAllText(Path.Combine(filedir, LocalSettingFileName), JsonSerializer.Serialize(Config));
        }

        public async Task<bool> DelectConfig(string key) {
            if (Config.ContainsKey(key)) {
                Config.Remove(key);
                save();
                return true;
            }
            return false;
        }

    }
}
