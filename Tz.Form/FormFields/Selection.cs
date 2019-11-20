using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.UIForms.FormFields
{

    public enum SelectionType
    {
        SINGLE = 0,
        MULTIPLE = 1
    }

    public class Selection : FormField
    {
        public SelectionType SeletionType { get; set; }
        public string ValueField;
        public string DisplayField;
        
        public Selection(string clientid) : base(clientid, "")
        {

        }
        public Selection(string clientid, string formid) : base(clientid, formid)
        {

        }
        new public bool Save() {
            return true;
        }
    
    }

   
}
