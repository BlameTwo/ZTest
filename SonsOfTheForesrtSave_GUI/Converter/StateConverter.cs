using System;
using System.Globalization;
using System.Windows.Data;

namespace SonsOfTheForesrtSave_GUI.Converter;
public class StateConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if(value is string str) {
            switch (str) {
                case "seenFirstTimeEquip_00000381":
                    return "是否见到00000381";
                case "seenFirstTimeEquip_00000392":
                    return "是否见到00000392";
                case "GotTutorialTarp":
                    return "得到最开始的防水袋";
                case "ShowMissingTacticalsTutorial":
                    return "显示缺少的战术装备";
                case "Strength":
                    return "力量";
                case "StrengthLevel":
                    return "力量等级";
                case "CurrentStrength":
                    return "当前力量";
                case "RequiredCurrentStrengthPerLevel":
                    return "每级所需力量";
                case "MaxHealth":
                    return "最大生命值";
                case "CurrentHealth":
                    return "当前生命值";
                case "TargetHealth":
                    return "目标生命值";
                case "Hydration":
                    return "水分";
                case "Fullness":
                    return "饱腹感";
                case "Rest":
                    return "休息增益";
                case "Stamina":
                    return "耐力";
                case "HydrationBuff":
                    return "水分增益";
                case "FullnessBuff":
                    return "饱腹感增益";
                case "RestBuff":
                    return "休息增益";
                case "player.position":
                    return "玩家位置";
                case "player.rotation":
                    return "玩家角度";
                case "player.areaMask":
                    return "*玩家区域遮罩";
                case "isNewItemInInventory_00000483":
                    return "00000483是库存新物品";
            }
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
