using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.UIAction.ActionType
{    
   public class Notification
    {
        public enum NotificationType {
            email,
            sms,
            successmessage,
            redirection
        }
    }
}
