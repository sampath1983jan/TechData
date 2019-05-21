using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax
{
    public enum IterativeType {
        fortype,
        whiletype,
        dowhiletype,
    }
    public class IterativeStatement: Statements,Element
    {
        private IterativeType _iterationType;
        private IterativeType IterationType => _iterationType;

        public dynamic EvalutionResult => throw new NotImplementedException();

        public IterativeStatement(IterativeType selectionType, Source.SourceSpan span) : base(TokenKind.If, span)
        {
            _iterationType = IterationType;
        }
        public override void Constructor()
        {
            if (_iterationType == IterativeType.fortype )
            {
                set(TokenKind.For);
            }
            else if (_iterationType == IterativeType.whiletype)
            {

            }
            else if (_iterationType == IterativeType.dowhiletype)
            {

            }
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void setResult(string name, object Right)
        {
            throw new NotImplementedException();
        }
    }
}
