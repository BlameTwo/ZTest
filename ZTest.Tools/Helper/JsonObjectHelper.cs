using System.Text.Json;

namespace ZTest.Tools.Helper;

public static class JsonObjectHelper
{
    public static object ConvertObject(this JsonElement jsonElement)
    {
        switch (jsonElement.ValueKind)
        {
            case JsonValueKind.Undefined:
                return null;
            case JsonValueKind.Object:
                throw new Exception("请使用LocalSetting泛型对象进行获取资源");
            case JsonValueKind.Array:
                return jsonElement.EnumerateArray().ToList();
            case JsonValueKind.String:
                return jsonElement.GetString()!;
            case JsonValueKind.Number:
                return jsonElement.GetInt64();
            case JsonValueKind.True:
                return true;
            case JsonValueKind.False:
                return false;
            case JsonValueKind.Null:
                return null;
            default:
                return null;
        }
    }
}
