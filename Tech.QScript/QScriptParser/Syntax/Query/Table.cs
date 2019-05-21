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
        public List<Relation> Relations;
        public Table() {
            TableName = "";
            TableAlias = "";
            Fields = new List<TableField>();
            Relations = new List<Relation>();
        }
        public Table(string tbName,string tbAlias="") {
            TableName = tbName;
            TableAlias = tbAlias;
        }
        public Table AddField(TableField tableField) {
            Fields.Add(tableField);
            return this;
        }
        public Table AddRelation(string lefttb, string leftfld, string righttb, string rightfld, WhereOperators operators) {
            Relation t = new Relation(lefttb, leftfld, righttb, rightfld, operators);
            this.Relations.Add(t);
            return this;
        }        
    }
}
