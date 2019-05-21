using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax.Query
{
    public class TableField
    {
        public string FieldName;
        public FieldType Type;
        public string FieldValue;
        public TableField() {
            FieldName ="";
            FieldValue = "";
            Type = FieldType.Text;
        }

    }
}
