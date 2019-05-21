using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax
{
    public static class StatementDefination {
        public static string IfStatement = @"";
        public static string SwitchStatement = @"";
    }

    public abstract class Statements : SyntaxNode
    {

        public  TokenKind _kind;
        public override SyntaxCatagory Catagory =>  SyntaxCatagory.Statement;
        public override TokenKind Kind => _kind;

        public abstract void Constructor();
        public Statements(TokenKind kind ,Source.SourceSpan span) : base(span) {
            _kind = kind;
            this.Constructor();
        }
        public void set(TokenKind kind) {
            _kind = kind;
        }
        
    }
}
