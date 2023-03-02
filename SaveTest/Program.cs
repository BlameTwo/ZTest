using SonsOfTheForesrtSaveLib;
using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using SonsOfTheForesrtSaveLib.SinglePlayer;

SaveConfig.DirPath = "C:\\Users\\30140\\AppData\\LocalLow\\Endnight\\SonsOfTheForest\\Saves\\76561198956487770\\SinglePlayer\\0412932379";


//PlayerStateData playerStateData = new PlayerStateData();
//var result = await playerStateData.ReadSave();
//var result2 = playerStateData.FormatData(result);
//result2.Data[0].Name = "Testing";
//playerStateData.WriteSave(result,result2);


GameWorldStateData gameworld = new();
var json = await gameworld.ReadSave();
var json2 = gameworld.FormatData(json.Data);
json2.GameDay = 18;
gameworld.WriteSave(json, json2);
Console.ReadLine();