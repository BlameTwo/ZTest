namespace ZTest.Tools;

public static class Base64ToMemoryStream
{
    /// <summary>
    /// Base64转换流
    /// </summary>
    /// <param name="strvalue"></param>
    /// <returns></returns>
    public static MemoryStream Convert(this string strvalue)
    {
        byte[] Bytes = System.Convert.FromBase64String(strvalue);
        MemoryStream stream = new MemoryStream(Bytes);
        return stream;
    }

    /// <summary>
    /// 反转Base64流
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static string BaseConvert(this MemoryStream stream)
    {
        var bytes = stream.ToArray();
        return System.Convert.ToBase64String(bytes);
    }

}
