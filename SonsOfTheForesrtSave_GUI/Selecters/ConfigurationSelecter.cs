using SonsOfTheForesrtSave_GUI.ViewModels.ItemViewModel;
using System.Windows;
using System.Windows.Controls;

namespace SonsOfTheForesrtSave_GUI.Selecters;

public class ConfigurationSelecter : DataTemplateSelector {
    public DataTemplate DefaultDataTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container) {
        if (item is not ConfigurationItemViewModel vm) return DefaultDataTemplate;
        if (vm.Name == "seenFirstTimeEquip_00000381"
            || vm.Name == "GotTutorialTarp"
            || vm.Name == "seenFirstTimeEquip_00000392"
            || vm.Name == "ShowMissingTacticalsTutorial"
            || vm.Name == "seenFirstTimeEquip_00000379"
            || vm.Name == "crafted.00000379"
            || vm.Name == "crafted.00000412"
            || vm.Name == "crafted.00000413"
            || vm.Name == "crafted.00000589"
            || vm.Name == "crafted.00000589"
            || vm.Name == "seenFirstTimeEquip_00000358"
            || vm.Name.StartsWith("is")
            ) {
            return BoolDataTemplate;
        }

        if (vm.Name.IndexOf("GameSetting.Vail.EnemySpawn") != -1)
            return BoolDataTemplate;

        if ( vm.Name == "StrengthLevel"
            || vm.Name == "consumed.00000446"
            )
            return IntDataTemplate;

        if (vm.Name == "CurrentStrength"
            || vm.Name == "RequiredCurrentStrengthPerLevel"
            || vm.Name == "MaxHealth"
            || vm.Name == "CurrentHealth"
            || vm.Name == "TargetHealth"
            || vm.Name == "Hydration"
            || vm.Name == "Fullness"
            || vm.Name == "Rest"
            || vm.Name == "Stamina"
            || vm.Name == "Strength"
            || vm.Name == "HydrationBuff"
            || vm.Name == "FullnessBuff"
            || vm.Name == "RestBuff"
            || vm.Name == "player.areaMask")
            return FloatDataTemplate;


        if (vm.Name == "Mode"
            ||vm.Name== "UID"
            ||vm.Name == "GameSetting.Vail.AnimalSpawnRate")
            return StringDataTemplate;


        if (vm.Name == "player.position")
            return PostionDataTemplate;


        if (vm.Name == "player.rotation")
            return RotaionDataTemplate;


        return DefaultDataTemplate;
    }
    
    public DataTemplate IntDataTemplate { get; set; }

    public DataTemplate FloatDataTemplate { get; set; }

    public DataTemplate StringDataTemplate { get; set; }

    public DataTemplate RotaionDataTemplate { get; set; }

    public DataTemplate PostionDataTemplate { get; set; }

    public DataTemplate BoolDataTemplate { get; set; }
}
