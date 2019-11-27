using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.UIAction.NotificationType
{
    public class SMS
    {
        public object Connection { get; set; } // it is custom object need to design based on the sms vendor
        public string To { get; set; }
        public string Message { get; set; }
    }
}
