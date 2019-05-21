using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.QScript.Syntax;

namespace Tech.QScript.Syntax
{
   public  class Duplicate:Fun
    {

        private List<ParamFields> _arguments;
        public string ColumnName;
        public string AliasColumnName;
                
        public Duplicate(Declare assignTo, string dataSource, Source.SourceSpan span)
            : base(assignTo, FunctionType.Columnduplicate, dataSource, span)
        {
            _arguments = new List<ParamFields>();
        }       
    }


}
