using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax.Query
{
   public class Table
    {
        public string TableName;
        public string TableAlias;
        public List<TableField> Fields;
        public Table() {
            TableName = "";
            TableAlias = "";
            Fields = new List<TableField>();
        }
        public Table(string tbName,string tbAlias="") {
            TableName = tbName;
            TableAlias = tbAlias;
        }
        public Table AddField(TableField tableField) {
            Fields.Add(tableField);
            return this;
        }
    }
}
