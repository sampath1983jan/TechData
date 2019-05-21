using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax.Query
{
   public class From
    {
        private List<Table> _tables;
        public List<Table> Tables { get { return _tables; } }
        public From() {
            _tables = new List<Table>();
        }
        public void AddTable(string TableName,string tbAlias ="") {
            _tables.Add( new Table(TableName,tbAlias));
        }
    }
}
