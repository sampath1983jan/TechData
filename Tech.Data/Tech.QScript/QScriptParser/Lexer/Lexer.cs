using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tech.QScript.Source;

namespace Tech.QScript.Lexer
{
    public class QLexer
    {
        private StringBuilder _builder;
        private int _column;
        private ErrorSink _errorSink;
        private int _index;
        private int _line;
        private string word;
        private SourceCode _sourceCode;
        private SourceLocation _tokenStart;
        private string[] words;
        private List<string> Declaration = new List<string>();
        private string _ch => _sourceCode.Lines[_index];

        public ErrorSink ErrorSink => _errorSink;
        /// <summary>
        /// 
        /// </summary>
        public QLexer() {
            _builder = new StringBuilder();
            _errorSink = new ErrorSink();
         
        }
/// <summary>
/// 
/// </summary>
/// <param name="sourceCode"></param>
/// <returns></returns>
        public IEnumerable<Token> LexFile(string sourceCode)
        {
            return LexFile(new SourceCode(sourceCode));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <returns></returns>
        public IEnumerable<Token> LexFile(SourceCode sourceCode)
        {
            _sourceCode = sourceCode;
            _builder.Clear();
            _line = 1;
            _index = 0;
            _column = 0;
            _tokenStart = new SourceLocation(0, _line, 0);
            //   CreateToken(TokenKind.EndOfFile);
            return LexContents();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="severity"></param>
        private void AddError(string message, Severity severity)
        {
            var span = new SourceSpan(_tokenStart, new SourceLocation(_index, _line, _column));
            _errorSink.AddError(message, _sourceCode, severity, span);
        }
        /// <summary>
        /// 
        /// </summary>
        private void DoNewLine()
        {
            _line = _line + 1;
            _index = 0;
            _column = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        private void Advance()
        {
            _index++;
            _column++;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Token> LexContents()
        {
            string left = "";
            string right = "";
            
            while (!IsEOF())
            {
                var line = _sourceCode.Lines[_line - 1];
               
                if (line == "") {
                    CreateToken(TokenKind.WhiteSpace, "");                    
                    DoNewLine();
                    continue;
                }
                if (line.IndexOf('=') > 0)
                {
                    left = line.Split('=')[0];
                    right = line.Split('=')[1];                    
                    words = Tech.QScript.Parser.QScriptParser.splitWithSpace (right).ToArray();
                    yield return ScanDeclaration(left);
                    Advance();

                    for (int iword = 0; iword < words.Length; iword++)
                    {
                        word = words[iword].Replace(";", "");
                        //   while (!IsEOF())
                        // {                 
                        yield return LexToken();
                        Advance();
                        //  }
                        // yield return CreateToken(TokenKind.EndOfLine);
                    }
                }
                else
                {
                    left = line;
                    right = line;
                    words = Tech.QScript.Parser.QScriptParser.splitWithSpace(left).ToArray();

                    for (int iword = 0; iword < words.Length; iword++)
                    {
                        word = words[iword].Replace(";", "");
                        //   while (!IsEOF())
                        // {                 
                        yield return LexToken();
                        Advance();
                        //  }
                        // yield return CreateToken(TokenKind.EndOfLine);
                    }
                   //Advance();
                }
               

                
                DoNewLine();
            }

            yield return addEndofFile();
         
          
        }
        private Token addEndofFile() {
          return  CreateToken(TokenKind.EndOfFile, "");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Token LexToken()
        {
            if (Scan(TokenExpression.Get))
            {
                return ScanGet();
            }
            else if (Scan(TokenExpression.GetInto))
            {
                return ScanGetInto();
            }
            else if (Scan(TokenExpression.from))
            {
                return Scanfrom();
            }
            else if (Scan(TokenExpression.And))
            {
                return ScanAnd();
            }
            else if (Scan(TokenExpression.Or))
            {
                return ScanOr();
            }
            else if (Scan(TokenExpression.Case))
            {
                return ScanCase();
            }
            else if (Scan(TokenExpression.Put))
            {
                return ScanPut();
            }
            else if (Scan(TokenExpression.Push))
            {
                return ScanPush();
            }
            else if (Scan(TokenExpression.d))
            {
                return ScanD();
            }
            else if (Scan(TokenExpression.p))
            {
                return ScanP();
            }
            else if (Scan(TokenExpression.Count))
            {
                return ScanCount();
            }
            else if (Scan(TokenExpression.UCount))
            {
                return ScanUCount();
            }
            else if (Scan(TokenExpression.Filter))
            {
                return ScanFilter();
            }
            else if (Scan(TokenExpression.Merge))
            {
                return ScanMerge();
            }
            else if (Scan(TokenExpression.Avg))
            {
                return ScanAvg();
            }
            else if (Scan(TokenExpression.Min))
            {
                return ScanMin();
            }
            else if (Scan(TokenExpression.Max))
            {
                return ScanMax();
            }
            else if (Scan(TokenExpression.Mean))
            {
                return ScanMean();
            }
            else if (Scan(TokenExpression.Medium))
            {
                return ScanMedium();
            }
            else if (Scan(TokenExpression.Mode))
            {
                return ScanMode();
            }
            else if (Scan(TokenExpression.Range))
            {
                return ScanRange();
            }
            else if (Scan(TokenExpression.Join))
            {
                return ScanJoin();
            }
            else if (Scan(TokenExpression.GetValue))
            {
                return ScanGetValue();
            }
            else if (Scan(TokenExpression.GetValues))
            {
                return ScanGetValues();
            }
            else if (Scan(TokenExpression.Tostring))
            {
                return ScanTostring();
            }
            else if (Scan(TokenExpression.Expr)) {
                return ScanExpr();
            }
            else if (Scan(TokenExpression.ifstate))
            {
                return ScanExpr();
            }
            else if (Scan(TokenExpression.literal))
            {
                return ScanLiteral();
            }
            else if (word.Trim() == "")
            {
                return whiteSpace();
            }
            else
            {
                AddError("syntax error: " + word, Severity.Error);
                return CreateToken(TokenKind.Error, "Invalid string:" + word);
            }            
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private bool isDeclaration(string val) {
            Match result = Regex.Match(val, TokenExpression.d, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return result.Success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private Token ScanDeclaration(string val) {
            Match result = Regex.Match(val, TokenExpression.d, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (result.Success)
            {
                if (Declaration.Where(x => x == "d:" + val || x == "p:" + val).ToList().Count > 0)
                {
                    var token = CreateToken(TokenKind.Error, "Variable "+ val +" already declared:");
                    AddError("Variable " + val + " already declared:", Severity.Error);
                    return token;
                }
                else {
                    Declaration.Add(result.Value);
                    return CreateToken(TokenKind.d, result.Value);
                }
                
            }
            else if (Regex.Match(val, TokenExpression.p, System.Text.RegularExpressions.RegexOptions.IgnoreCase).Success)
            {
                Declaration.Add(result.Value);
                return CreateToken(TokenKind.p, result.Value);
            }
            else {
                if (Declaration.Where(x => x == "d:" + val).ToList().Count > 0)
                {
                    return CreateToken(TokenKind.d, result.Value);
                }
                else if (Declaration.Where(x => x == "p:" + val).ToList().Count > 0) {
                    return CreateToken(TokenKind.d, result.Value);
                }
                else
                {
                    var token = CreateToken(TokenKind.Error, "Invalid declaration:");
                    AddError("Invalid declaration", Severity.Error);
                    return token;
                }                
            }            
        }
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        private bool isGet() {
            Match result = Regex.Match(word, TokenExpression.Get, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return result.Success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private bool Scan(string exp)
        {
            Match result = Regex.Match(word, exp, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (result.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanGet() {
            if (Scan(TokenExpression.Get))
            {
                return CreateToken(TokenKind.Get, word);
            }
            return null;
        }       
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanGetInto()
        {
           
            if (Scan(TokenExpression.GetInto))
            {
                return CreateToken(TokenKind.GetInto, word);
            }
            return null;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token Scanfrom()
        {

            if (Scan(TokenExpression.from))
            {
                return CreateToken(TokenKind.From, word);
            }
            return null;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanAnd()
        {

            if (Scan(TokenExpression.And))
            {
                return CreateToken(TokenKind.And, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanOr()
        {

            if (Scan(TokenExpression.Or))
            {
                return CreateToken(TokenKind.Or, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanPut()
        {

            if (Scan(TokenExpression.Put))
            {
                return CreateToken(TokenKind.Put, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanPush()
        {

            if (Scan(TokenExpression.Push))
            {
                return CreateToken(TokenKind.Push, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanCase()
        {

            if (Scan(TokenExpression.Case))
            {
                return CreateToken(TokenKind.Case, word);
            }
            return null;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanD()
        {

            if (Scan(TokenExpression.d))
            {
                return CreateToken(TokenKind.d, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanP()
        {

            if (Scan(TokenExpression.p))
            {
                return CreateToken(TokenKind.p, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanFilter()
        {

            if (Scan(TokenExpression.Filter))
            {
                return CreateToken(TokenKind.Filter, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanMerge()
        {

            if (Scan(TokenExpression.Merge))
            {
                return CreateToken(TokenKind.Merge, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanCount()
        {

            if (Scan(TokenExpression.Count))
            {
                return CreateToken(TokenKind.Count, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanUCount()
        {

            if (Scan(TokenExpression.UCount))
            {
                return CreateToken(TokenKind.UCount, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanAvg()
        {

            if (Scan(TokenExpression.Avg))
            {
                return CreateToken(TokenKind.Avg, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanMin()
        {

            if (Scan(TokenExpression.Min))
            {
                return CreateToken(TokenKind.Min, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanMax()
        {

            if (Scan(TokenExpression.Max))
            {
                return CreateToken(TokenKind.Max, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanMedium()
        {

            if (Scan(TokenExpression.Medium))
            {
                return CreateToken(TokenKind.Medium, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanMean()
        {

            if (Scan(TokenExpression.Mean))
            {
                return CreateToken(TokenKind.Mean, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanMode()
        {

            if (Scan(TokenExpression.Mode))
            {
                return CreateToken(TokenKind.Mode, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanRange()
        {

            if (Scan(TokenExpression.Range))
            {
                return CreateToken(TokenKind.Range, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanJoin()
        {

            if (Scan(TokenExpression.Join))
            {
                return CreateToken(TokenKind.Join, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanGetValue()
        {

            if (Scan(TokenExpression.GetValue))
            {
                return CreateToken(TokenKind.GetValue, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanGetValues()
        {

            if (Scan(TokenExpression.GetValues))
            {
                return CreateToken(TokenKind.GetValues, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanTostring()
        {

            if (Scan(TokenExpression.Tostring))
            {
                return CreateToken(TokenKind.toString, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanExpr()
        {

            if (Scan(TokenExpression.Expr))
            {
                return CreateToken(TokenKind.Expr, word);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanLiteral()
        {

            if (Scan(TokenExpression.literal))
            {
                return CreateToken(TokenKind.literal, word);
            }
            return null;
        }


        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token Scanif()
        {

            if (Scan(TokenExpression.ifstate))
            {
                return CreateToken(TokenKind.If, word);
            }
            return null;
        }
        private Token whiteSpace()
        {

          //  if (Scan(TokenExpression.))
           // {
                return CreateToken(TokenKind.WhiteSpace, word);
            //}
           // return null;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="txt"></param>
        /// <returns></returns>
        private Token CreateToken(TokenKind kind, string txt = "")
        {
            string contents = txt;
            SourceLocation end = new SourceLocation(_index, _line, _column);
            SourceLocation start = _tokenStart;
            _tokenStart = end;
            _builder.Clear();

            return new Token(kind, contents, start, end);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsEOF()
        {
            if (_line < _sourceCode.Lines.Length)
            {
                return false;
            }
            else return true;
        }

    }
}
