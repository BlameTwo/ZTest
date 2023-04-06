using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SonsOfTheForesrtSaveLib.Models.SinglePlayer;
public class GameWorldStateResultData 
{
    /// <summary>
    /// 存档版本
    /// </summary>
    [JsonPropertyName("Version")]public string Version { get; set; }

    /// <summary>
    /// 保存时间
    /// </summary>
    [JsonPropertyName("SaveTime")]public DateTime SaveTime { get; set; }

    /// <summary>
    /// 世界难度
    /// </summary>
    [JsonPropertyName("GameType")]public string GameType { get; set; }

    /// <summary>
    /// ???
    /// </summary>
    [JsonPropertyName("GameId")]public object GameID { get; set; }

    /// <summary>
    /// 生存天数
    /// </summary>
    [JsonPropertyName("GameDays")]public int GameDay { get; set; }

    /// <summary>
    /// 生存小时
    /// </summary>
    [JsonPropertyName("GameHours")]public int GameHours { get; set; }

    /// <summary>
    /// 生存分钟
    /// </summary>
    [JsonPropertyName("GameMinutes")]public int GameMinutes { get; set; }

    /// <summary>
    /// 生存秒
    /// </summary>
    [JsonPropertyName("GameSeconds")]public int GameSeconds { get; set; }

    /// <summary>
    /// 生存毫秒
    /// </summary>
    [JsonPropertyName("GameMilliseconds")]public int GameMilliseconds { get; set; }

    /// <summary>
    /// 凯文是否死亡
    /// </summary>
    [JsonPropertyName("IsRobbyDead")]public bool IsRobbyDead { get; set; }

    /// <summary>
    /// 三手女人是否死亡
    /// </summary>
    [JsonPropertyName("IsVirginiaDead")]public bool IsVirginiaDead { get; set; }

    /// <summary>
    /// 主线是否通关
    /// </summary>
    [JsonPropertyName("CoreGameCompleted")]public bool CoreGameCompleted { get; set; }

    /// <summary>
    /// 是否逃离岛屿
    /// </summary>
    [JsonPropertyName("EscapedIsland")]public bool EscapedIsland { get; set; }

    /// <summary>
    /// 是否留在岛屿
    /// </summary>
    [JsonPropertyName("StayedOnIsland")]public bool StayedOnIsland { get; set; }

    /// <summary>
    /// 坠机地点
    /// </summary>
    [JsonPropertyName("CrashSite")]public string CrashSite { get; set; }

    /// <summary>
    /// 母鸡，应该是某些战术装备的拾取配置
    /// </summary>
    [JsonPropertyName("NamedIntDatas")]public JsonArray NamedIntDatas { get; set; }
}


public class GameWorldStateResultModel {
    [JsonPropertyName("GameState")]public string Data { get; set; }
}