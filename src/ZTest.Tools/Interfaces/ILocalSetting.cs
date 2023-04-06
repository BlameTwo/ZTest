namespace ZTest.Tools.Interfaces;
public interface ILocalSetting {
    /// <summary>
    /// 配置文件名称
    /// </summary>
    string LocalSettingFileName { get; }



    /// <summary>
    /// 当前配置文件
    /// </summary>
    Dictionary<string, object> Config { get; set; }

    /// <summary>
    /// 保存配置
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns>保存Task任务，可放置到后代码运行</returns>
    Task SaveConfig<T>(string key, T value);

    /// <summary>
    /// 读取值类型数据
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>值类型结果，需要自行增加显示转换</returns>
    Task<object> ReadConfig(string key);

    /// <summary>
    /// 读取object引用类型数据
    /// </summary>
    /// <typeparam name="T">引用类型对象</typeparam>
    /// <param name="key">键</param>
    /// <returns>引用类型结果</returns>
    Task<T> ReadObjectConfig<T>(string key);

    /// <summary>
    /// 删除配置
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>运行结果</returns>
    Task<bool> DelectConfig(string key);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <returns>初始化结果</returns>
    Task<bool> InitSetting();
}
