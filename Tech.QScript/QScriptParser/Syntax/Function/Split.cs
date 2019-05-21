using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.QScript.Syntax;

namespace Tech.QScript.Syntax
{
   public class Split:Fun
    {
        private List<ParamFields> _arguments;
        public string SplitColumn;
        public string Spliter;
        public string ColumnPrefix;
                
        public Split(Declare assignTo, string dataSource, Source.SourceSpan span)
           : base(assignTo, FunctionType.Columnspit, dataSource, span)
        {
            _arguments = new List<ParamFields>();
        }

    }
}
