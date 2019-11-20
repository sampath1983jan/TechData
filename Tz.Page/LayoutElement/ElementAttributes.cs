using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Page.LayoutElement.ElementAttributes
{

    public class Padding
    {
        public int Left { get; set; }
        public int Right { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }
        public Padding()
        {
            Left = 0;
            Right = 0;
            Top = 0;
            Bottom = 0;
        }
    }
    public class Margin {
        public int Left { get; set; }
        public int Right { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }
        public Margin() {
            Left = 0;
            Right = 0;
            Top = 0;
            Bottom = 0;
        }
    }
    public class Scroll {
        public bool Enable { get; set; }
        public bool EnableX { get; set; }
        public bool EnableY { get; set; }
        public Scroll()
        {
            Enable = false;
            EnableX = false;
            EnableY = false;
        }
        public void EnableAutoScroll() {
            Enable = true;
        }
    }

}
