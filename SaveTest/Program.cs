using SonsOfTheForesrtSaveLib;
using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using SonsOfTheForesrtSaveLib.SinglePlayer;

SaveConfig.DirPath = "C:\\Users\\30140\\AppData\\LocalLow\\Endnight\\SonsOfTheForest\\Saves\\76561198956487770\\SinglePlayer\\1146096989";


//PlayerStateData playerStateData = new PlayerStateData();
//var result = await playerStateData.ReadSave();
//var result2 = playerStateData.FormatData(result);
//result2.Data[0].Name = "Testing";
//playerStateData.WriteSave(result,result2);


//GameWorldStateData gameworld = new();
//var json = await gameworld.ReadSave();
//var json2 = gameworld.FormatData(json.Data);
//json2.GameDay = 18;
//gameworld.WriteSave(json, json2);

//var res = await PackageManager.GetPackage();
PlayerPackageData data = new();
var result = await data.ReadSave();
var data2 = data.FormatData(result.Data);
Console.ReadLine();