using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax
{
    public class GetInto : SyntaxNode
    {
        public string SourceVariable = "";
        public string TableName = "";
        public override SyntaxCatagory Catagory => SyntaxCatagory.Query;
        public override TokenKind Kind =>  TokenKind.GetInto;
        public GetInto(QScript.Source.SourceSpan span,string sourceVariable,string tbName) :base(span) {
            SourceVariable = sourceVariable;
            TableName = tbName;
        }
    }
}
