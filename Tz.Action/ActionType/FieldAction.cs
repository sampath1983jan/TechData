using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.UIAction.ActionType
{
    public class FieldAction
    {
        public enum FieldActionType {
            hidefield,
            showfield,
            enablefield,
            disablefield,
            showsubform,
            inputchange, // write script to execute.
        }
        public string FieldName { get; set; }
        public FieldActionType Type { get; set; }
        public string QScript { get; set; }
    }
}
