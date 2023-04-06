using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing_BOT.Models
{
    public class BingChangedArgs : EventArgs
    {
        public BingChangedArgs(bool Begin, bool End, string Text)
        {
            this.Begin = Begin;
            this.End = End;
            this.Text = Text;
        }

        public bool Begin { get; }
        public bool End { get; }
        public string Text { get; }
    }

    public delegate void BingMessageChangedHandle(object sender, BingChangedArgs args,bool isopen);

}
