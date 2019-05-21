using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.QScript.Syntax;

namespace Tech.QScript.Parser
{
   public sealed partial class QScriptParser
    {
        private Syntax.Statements StatementParser(Declare assignTo) {
            if (_current.Kind == TokenKind.If)
            {
                string content = getValue(Syntax.StatementDefination.IfStatement, _current.Value);
                var st = new Syntax.IFStatement(assignTo, _current.Span);
                st.addText(_current.Value);
                return st;
            }
            else if (Scan(Syntax.StatementDefination.SwitchStatement, _current.Value))
            {
                AddError(Source.Severity.Error, "Switch statement not implimented", _current.Span);
                return null;
            }
            else {
                AddError(Source.Severity.Error, "Invalid Statement", _current.Span);
                return null;
            }
        }              
    }
}
