using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Controls
{
    public class Webcontrol: Events
    {
        public string ID { get; set; }
        public Style Style { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public string Height { get; set; }
        public string Description { get; set; }
 
        protected void Render() {

        }
    }
}
