using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax
{
    public class IFStatement : Statements
    {
        private string _text;
        public string Text => _text;
        public Declare AssignTo;
        public IFStatement(Declare assignTo,Source.SourceSpan span) : base(TokenKind.If, span) {
            AssignTo = assignTo;
        }
        public void addText(string text)
        {
            _text = text;
        }
        public override void Constructor()
        {            
                set(TokenKind.If);                      
        }
        public void Accept(IVisitor visitor, EvaluationParam evaluationParam)
        {
            visitor.Visit(this, evaluationParam);
        }
        public void setResult(string name, object Right)
        {
            throw new NotImplementedException();
        }
    }

}
