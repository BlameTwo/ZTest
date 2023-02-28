using SonsOfTheForesrtSaveLib;
using SonsOfTheForesrtSaveLib.Models.SinglePlayer;
using SonsOfTheForesrtSaveLib.SinglePlayer;

PlayerStateData playerStateData = new PlayerStateData();
var result = await playerStateData.ReadSave();
var result2 = playerStateData.FormatData(result);
result2.Data[0].Name = "Testing";
playerStateData.WriteSave(result,result2);
Console.ReadLine();