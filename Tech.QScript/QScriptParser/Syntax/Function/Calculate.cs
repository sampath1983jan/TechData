using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax
{
  public  class Calculate:Fun
    {
        public string ColumnName;
        public string formula;

        public Calculate(Declare assignTo, string dataSource, Source.SourceSpan span)
            : base(assignTo, FunctionType.Calculate , dataSource, span)
        {
            
        }
    }
}
