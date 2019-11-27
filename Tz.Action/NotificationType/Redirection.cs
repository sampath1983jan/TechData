using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.UIAction.NotificationType
{
   public class Redirection
    {
        public string Type { get; set; } // website,form,report,page and etc
        public string Value { get; set; } // based onthe type, user feed input value
        public string OpenIn { get; set; } // same,new window.
    }
}
