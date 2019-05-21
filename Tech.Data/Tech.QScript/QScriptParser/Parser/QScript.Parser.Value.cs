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

        private Syntax.Value ParseValue(Declare assignTo)
        {
            try
            {
               // var _strval = getValue(Syntax.Value.ValueExpression, _current.Value);
                var val = new Syntax.Value(assignTo, _current.Span, _current.Value);
                return val;
            }
            catch (Exception e)
            {
                AddError(Source.Severity.Error, e.Message, _current.Span);
                var val = new Syntax.Value(assignTo, _current.Span, "");
                return val;
            }
        }

    }
}
