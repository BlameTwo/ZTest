using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChatGPT_GUI.Selecter
{
    public class BingChatSelecter: DataTemplateSelector
    {
        public DataTemplate Bing { get; set; }

        public DataTemplate User { get; set; }

        public DataTemplate Error { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Bing_BOT.Models.VM.BingBotModel model)
            {
                switch (model.ChatType)
                {
                    case Bing_BOT.Enum.BingChatType.Bing:
                        return Bing;
                    case Bing_BOT.Enum.BingChatType.User:
                        return User;
                }
            }
            return Error;
        }
    }
}
