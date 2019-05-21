using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax
{
   public  class Replace:Fun
    {
        private List<ParamFields> _arguments;
        public string ColumnName;
        public string FindString;
        public string ReplaceString;
        public string NewColumnName;

        public Replace(Declare assignTo, string dataSource, Source.SourceSpan span)
            : base(assignTo, FunctionType.Replace, dataSource, span)
        {
            _arguments = new List<ParamFields>();
        }

    }
}
