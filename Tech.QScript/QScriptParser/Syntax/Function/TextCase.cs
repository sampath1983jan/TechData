using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax
{
    public enum TextCaseType {
        UPPER,
        LOWER,
        TITLE,
        CAPITAL
    }
    public class TextCase : Fun
    {
        public string ColumnName;
        public TextCaseType CaseType;
        public TextCase(Declare assignTo, string dataSource, Source.SourceSpan span)
            : base(assignTo, FunctionType.ChangeCase , dataSource, span)
        {
            
        }
    }
}
