using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Parser
{
   public sealed partial class QScriptParser
    {
        private Syntax.Statements StatementParser() {
            if (_current.Kind == TokenKind.If)
            {
                string content = getValue(Syntax.StatementDefination.IfStatement, _current.Value);
                var st = new Syntax.SelectionStatement(Syntax.SelectionType.ifstatement, _current.Span);
                st.addText(content);
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
