using ChatGPT_GUI.Models;
using System.Windows;
using System.Windows.Controls;

namespace ChatGPT_GUI.Selecter {
    public class ChatSelecter:DataTemplateSelector {
        public DataTemplate AI { get; set; }

        public DataTemplate User { get; set; }

        public DataTemplate Error { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            if(item is ChatModel model) {
                switch (model.Type) {
                    case ChatType.User:
                        return User;
                    case ChatType.AI:
                        return AI;
                }
            }
            return Error;
        }
    }
}
