using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.QScript.Syntax;

namespace Tech.QScript.Syntax
{
    public class Value : Syntax.SyntaxNode,Element
    {
        public override SyntaxCatagory Catagory => SyntaxCatagory.Literal;
        public override TokenKind Kind =>  TokenKind.literal;
        public static string ValueExpression= "(.*?)";
        private string _value;
        private Declare _assignTo;
        public Declare AssignTo { get => _assignTo; }

        public dynamic EvalutionResult => throw new NotImplementedException();

        public Value(Declare assignTo,Source.SourceSpan span,string value):base(span) {
            _assignTo = assignTo;
            _value = value;
        }
        public string getValue() {
            return _value;
        }

       

        public void setResult(string name, object Right)
        {
            throw new NotImplementedException();
        }

        public void Accept(IVisitor visitor, EvaluationParam evaluationParam)
        {
            visitor.Visit(this, evaluationParam);
        }
    }
}
