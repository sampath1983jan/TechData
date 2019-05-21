using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax
{
    public enum SelectionType {
        ifstatement,
        switchstatement,
    }
    public class SelectionStatement: Statements, Element
    {
        private SelectionType _selectionType;
        private string _text;
        public string Text => _text;
        private SelectionType SelectionType => _selectionType;

        public dynamic EvalutionResult => throw new NotImplementedException();

        public SelectionStatement(SelectionType selectionType, Source.SourceSpan span) : base(TokenKind.If,span) {
            _selectionType = SelectionType;
        }

        public void addText(string text) {
            _text = text;
        }
        public override void Constructor()
        {
            if (_selectionType == SelectionType.ifstatement)
            {
                set(TokenKind.If);
            }
            else if (_selectionType == SelectionType.switchstatement) {

            }            
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
