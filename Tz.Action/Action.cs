using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.UIAction
{ 

    public class Action
    {
        public enum ActionType
        {
            notification,
            fieldaction,
            dataacess,
            integration,
            datascript,
        }
        public enum RunBy {
            Always,
            Conditional
        }

    }
}
