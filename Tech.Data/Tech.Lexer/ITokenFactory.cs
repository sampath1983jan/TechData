using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
 

namespace Tech.Lexer
{

    public interface ITokenFactory
    {
        /// <summary>
        /// This is the method used to create tokens in the lexer and in the
        /// error handling strategy.
        /// </summary>
        /// <remarks>
        /// This is the method used to create tokens in the lexer and in the
        /// error handling strategy. If text!=null, than the start and stop positions
        /// are wiped to -1 in the text override is set in the CommonToken.
        /// </remarks>
        //[return: NotNull]
        //IToken Create(Tuple<ITokenSource, ICharStream> source, int type, string text, int channel, int start, int stop, int line, int charPositionInLine);

        /// <summary>Generically useful</summary>
      
       IToken Create(int type, string text);
        IToken Create(int type, string text,int tokenIndex,int line);

    }
}
