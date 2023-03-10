using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPT_GUI.Models {
    public class ChatModel {
        public DateTime DateTime { get; set; }  

        public string Message { get; set; }

        public ChatType Type { get; set; }
    }

    public enum ChatType {
        User,AI,System
    }
}
