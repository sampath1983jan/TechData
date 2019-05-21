using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace Tech.QueryParser.Language.Syntax
{
    public abstract class Expression : SyntaxNode
    {
        public override SyntaxCatagory Catagory => SyntaxCatagory.Expression;

        protected Expression(SourceSpan span) : base(span)
        {
        }
    }
}
    