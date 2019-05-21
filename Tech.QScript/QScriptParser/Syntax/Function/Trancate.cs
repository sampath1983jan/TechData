using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax
{
    public class Trancate:Fun
    {
        public string ColumnName;
        public int TrancateIndex;

        public Trancate(Declare assignTo, string dataSource, Source.SourceSpan span)
            : base(assignTo, FunctionType.Trancate, dataSource, span)
        {
            
        }
    }
}
