using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Lexer
{
   public interface IToken
    {
        string Text {
            get;
        }
        int Type {
            get;
        }
        int Line {
            get;
        }
        int TokenIndex {
            get;
        }        
    }
}
