using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.QScript.Source;



namespace Tech.QScript.Syntax
{
    public abstract class SyntaxNode
    {
        public abstract SyntaxCatagory Catagory { get; }

        public abstract TokenKind Kind { get; }

        public SourceSpan Span { get; }

        protected SyntaxNode(SourceSpan span)
        {
            Span = span;
        }
    }
}
