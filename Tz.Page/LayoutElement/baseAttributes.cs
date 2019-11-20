using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Page.LayoutElement
{
    public abstract class baseAttributes
    {
         public int width { get; set; }
        public string height { get; set; }
        public ElementAttributes.Padding Padding { get; set; }
        public ElementAttributes.Margin Margin { get; set; }
        public ElementAttributes.Scroll Scroll { get; set; }
        public bool Enable { get; set; }
    }
}
