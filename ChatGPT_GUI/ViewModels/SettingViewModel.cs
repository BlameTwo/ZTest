using CommunityToolkit.Mvvm.ComponentModel;
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using SimpleUI.Controls;
using SimpleUI.Interface;
using System.Threading.Tasks;
using Waher.Content.Html.Elements;
using ZTest.Tools.Interfaces;

namespace ChatGPT_GUI.ViewModels;
public partial class SettingViewModel:ObservableObject 
{
    public SettingViewModel(IThemeApply<App> themeApply,ILocalSetting localSetting, IOpenAIService openAIService)
    {
        ThemeApply = themeApply;
        LocalSetting = localSetting;
        OpenAIService = openAIService;
        Init();
    }

    private async void Init() {
        var keyw = (await LocalSetting.ReadConfig("KeyWord"));
        var themesrt= (await LocalSetting.ReadConfig("Theme"));
        Keywrold = keyw == null?"":keyw.ToString()!;
        Themestr = themesrt == null ? "深红色" : themesrt.ToString()!;
    }

    partial  void OnThemestrChanged(string value) {
        Task.Run(async () =>
        {
            if (value is string str)
            {
                switch (str)
                {
                    case "浅色":
                        ThemeApply.Apply(SimpleUI.Themes.ThemeType.Light);
                        await LocalSetting.SaveConfig("Theme", "浅色");
                        ThemeApply.IsEnable = false;
                        break;
                    case "深色":
                        ThemeApply.Apply(SimpleUI.Themes.ThemeType.Dark);
                        await LocalSetting.SaveConfig("Theme", "深色");
                        ThemeApply.IsEnable = false;
                        break;
                    case "深红色":
                        ThemeApply.Apply(SimpleUI.Themes.ThemeType.Cyberpunk);
                        await LocalSetting.SaveConfig("Theme", "深红色");
                        ThemeApply.IsEnable = false;
                        break;
                    case "自动":
                        await LocalSetting.SaveConfig("Theme", "自动");
                        ThemeApply.IsEnable = true;
                        break;
                }
                return;
            }
        });
    }

    partial void OnKeywroldChanged(string value) {
        Task.Run(async () =>
        {
            await LocalSetting.SaveConfig("KeyWord", value);
            OpenAIService = new OpenAIService(new OpenAiOptions() { ApiKey =value });

        });
    }

    public IThemeApply<App> ThemeApply { get; }
    public ILocalSetting LocalSetting { get; }
    public IOpenAIService OpenAIService { get; set; }

    [ObservableProperty]
    string _keywrold;

    [ObservableProperty]
    string _themestr;


}
