using Tech.QScript.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Parser
{
    public sealed partial class QScriptParser
    {
        private int getRandomNo() {
            return new Random(1).Next(10000000);
        }

        private string replaceAlpha(string sr) {
            var items = getValues(@"[A-Za-z]+", sr);
            foreach (string s in items) {
               sr= sr.Replace(s.ToString(), getRandomNo().ToString());
            }
            return sr;
        }

        private Expression ParseExpression(Declare assignTo) {            
            if (Scan(Expression.ExpressionReg, _current.Value))
            {
                var exp = getValue(Expression.ExpressionReg, _current.Value);
                try
                {
                    var cal = new NCalc.Expression(replaceAlpha(exp)).Evaluate();
                }
                catch (Exception e) {
                    AddError(Source.Severity.Error, e.Message, _current.Span);
                }                

                var ex = new Expression(assignTo,exp, _current.Span);
                return ex;
            }
            else {
                AddError(Source.Severity.Error, "Invalid Expression", _current.Span);
                return new Expression(assignTo,"", _current.Span);
            }
        }
    }


}
