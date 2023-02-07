namespace WSA_ADBShell.Services.Interfaces;

public interface ITaskToken
{
    /// <summary>
    /// Token生成标识
    /// </summary>
    public string TokenSource { get; }

    /// <summary>
    /// Token源名称
    /// </summary>
    public string TokenName { get;}

    public string Token { get; set; }
}
