using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Page.WebControls
{
    public class Nav: Controls.Webcontrol
    {        
        public List<NavItem> NavItems { get; set; }
    }
    public class NavItem {
        public string NavID { get; set; }
        public string Name { get; set; }
        public string Css { get; set; }
        public string SelectEvent { get; set; }
        
    }
}
