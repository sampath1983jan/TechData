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
                    left = line.Split('=')[0].Trim();
                    right = line.Split('=')[1].Trim();
                    words = right.Split(';').ToArray(); //Tech.QScript.Parser.QScriptParser.splitWithSpace (right).ToArray();
                    yield return ScanDeclaration(left);
                    Advance();

                    for (int iword = 0; iword < words.Length; iword++)
                    {
                        word = words[iword].Replace(";", "").Trim();
                        //   while (!IsEOF())
                        // {                 
                        yield return LexToken(left);
                        Advance();
                        //  }
                        // yield return CreateToken(TokenKind.EndOfLine);
                    }
                }
                else
                {
                    left = line.Trim();
                    right = line.Trim();
                    words = right.Split(';').ToArray();
                    for (int iword = 0; iword < words.Length; iword++)
                    {
                        word = words[iword].Replace(";", "").Trim();
                        //   while (!IsEOF())
                        // {                 
                        yield return LexToken("");
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
        private Token LexToken(string d)
        {
            if (Scan(TokenExpression.Get))
            {
                return ScanGet(d);
            }
            else if (Scan(TokenExpression.GetInto))
            {
                return ScanGetInto(d);
            }
            else if (Scan(TokenExpression.from))
            {
                return Scanfrom(d);
            }
            else if (Scan(TokenExpression.And))
            {
                return ScanAnd(d);
            }
            else if (Scan(TokenExpression.Or))
            {
                return ScanOr(d);
            }
            else if (Scan(TokenExpression.Case))
            {
                return ScanCase(d);
            }
            else if (Scan(TokenExpression.Put))
            {
                return ScanPut(d);
            }
            else if (Scan(TokenExpression.Push))
            {
                return ScanPush(d);
            }
            else if (Scan(TokenExpression.d))
            {
                return ScanD(d);
            }
            else if (Scan(TokenExpression.p))
            {
                return ScanP(d);
            }
            else if (Scan(TokenExpression.Count))
            {
                return ScanCount(d);
            }
            else if (Scan(TokenExpression.UCount))
            {
                return ScanUCount(d);
            }
            else if (Scan(TokenExpression.Filter))
            {
                return ScanFilter(d);
            }
            else if (Scan(TokenExpression.Pivot))
            {
                return ScanPviot(d);
            }
            else if (Scan(TokenExpression.Merge))
            {
                return ScanMerge(d);
            }
            else if (Scan(TokenExpression.Avg))
            {
                return ScanAvg(d);
            }
            else if (Scan(TokenExpression.Min))
            {
                return ScanMin(d);
            }
            else if (Scan(TokenExpression.Max))
            {
                return ScanMax(d);
            }
            else if (Scan(TokenExpression.Mean))
            {
                return ScanMean(d);
            }
            else if (Scan(TokenExpression.Medium))
            {
                return ScanMedium(d);
            }
            else if (Scan(TokenExpression.Mode))
            {
                return ScanMode(d);
            }
            else if (Scan(TokenExpression.calculate))
            {
                return ScanCalculate(d);
            }
            else if (Scan(TokenExpression.Range))
            {
                return ScanRange(d);
            }
            else if (Scan(TokenExpression.Join))
            {
                return ScanJoin(d);
            }
            else if (Scan(TokenExpression.GetValue))
            {
                return ScanGetValue(d);
            }
            else if (Scan(TokenExpression.GetValues))
            {
                return ScanGetValues(d);
            }
            else if (Scan(TokenExpression.Tostring))
            {
                return ScanTostring(d);
            }
            else if (Scan(TokenExpression.Expr)) {
                return ScanExpr(d);
            }
            else if (Scan(TokenExpression.ifstate))
            {
                return Scanif(d);
            }
           
            else if (Scan(TokenExpression.duplicate))
            {
                return ScanDuplicate(d);
            }
            else if (Scan(TokenExpression.replace))
            {
                return ScanReplace(d);
            }
            else if (Scan(TokenExpression.split))
            {
                return ScanSplit(d);
            }
            else if (Scan(TokenExpression.trancate))
            {
                return ScanTrancate(d);
            }
            else if (Scan(TokenExpression.dateparser))
            {
                return ScanDateParse(d);
            }
            else if (Scan(TokenExpression.casechange))
            {
                return ScanChangeCase(d);
            }
            else if (Scan(TokenExpression.tojson))
            {
                return ScanToJson(d);
            }
            else if (Scan(TokenExpression.literal))
            {
                return ScanLiteral(d);
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
/// <param name="d"></param>
/// <returns></returns>
        private Token ScanToJson(string d)
        {

            if (Scan(TokenExpression.tojson))
            {
                return CreateToken(TokenKind.ToJson, word, d);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private Token ScanChangeCase(string d)
        {

            if (Scan(TokenExpression.casechange))
            {
                return CreateToken(TokenKind.ChangeCase, word, d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private Token ScanDateParse(string d)
        {

            if (Scan(TokenExpression.dateparser))
            {
                return CreateToken(TokenKind.Dateparse, word, d);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private Token ScanTrancate(string d)
        {

            if (Scan(TokenExpression.trancate))
            {
                return CreateToken(TokenKind.Trancate, word, d);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private Token ScanSplit(string d)
        {

            if (Scan(TokenExpression.split))
            {
                return CreateToken(TokenKind.Columnspit, word, d);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private Token ScanReplace(string d)
        {

            if (Scan(TokenExpression.replace))
            {
                return CreateToken(TokenKind.Replace, word, d);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private Token ScanDuplicate(string d)
        {

            if (Scan(TokenExpression.duplicate))
            {
                return CreateToken(TokenKind.Columnduplicate, word, d);
            }
            return null;
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
            val = val.Trim();
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
                    return null;
                   // return CreateToken(TokenKind.d, result.Value);
                }
                else if (Declaration.Where(x => x == "p:" + val).ToList().Count > 0) {
                    // return CreateToken(TokenKind.d, result.Value);
                    return null;
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
        private Token ScanGet(string d) {
            if (Scan(TokenExpression.Get))
            {
                return CreateToken(TokenKind.Get, word,d);
            }
            return null;
        }       
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanGetInto(string d)
        {
           
            if (Scan(TokenExpression.GetInto))
            {
                return CreateToken(TokenKind.GetInto, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token Scanfrom(string d)
        {

            if (Scan(TokenExpression.from))
            {
                return CreateToken(TokenKind.From, word,d);
            }
            return null;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanAnd(string d)
        {

            if (Scan(TokenExpression.And))
            {
                return CreateToken(TokenKind.And, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanOr(string d)
        {

            if (Scan(TokenExpression.Or))
            {
                return CreateToken(TokenKind.Or, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanPut(string d)
        {

            if (Scan(TokenExpression.Put))
            {
                return CreateToken(TokenKind.Put, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanPush(string d)
        {

            if (Scan(TokenExpression.Push))
            {
                return CreateToken(TokenKind.Push, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanCase(string d)
        {

            if (Scan(TokenExpression.Case))
            {
                return CreateToken(TokenKind.Case, word,d);
            }
            return null;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanD(string d)
        {

            if (Scan(TokenExpression.d))
            {
                return CreateToken(TokenKind.d, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanP(string d)
        {

            if (Scan(TokenExpression.p))
            {
                return CreateToken(TokenKind.p, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanFilter(string d)
        {

            if (Scan(TokenExpression.Filter))
            {
                return CreateToken(TokenKind.Filter, word,d);
            }
            return null;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanPviot(string d)
        {

            if (Scan(TokenExpression.Pivot))
            {
                return CreateToken(TokenKind.Pivot, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanMerge(string d)
        {

            if (Scan(TokenExpression.Merge))
            {
                return CreateToken(TokenKind.Merge, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanCount(string d)
        {

            if (Scan(TokenExpression.Count))
            {
                return CreateToken(TokenKind.Count, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanUCount(string d)
        {

            if (Scan(TokenExpression.UCount))
            {
                return CreateToken(TokenKind.UCount, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanAvg(string d)
        {

            if (Scan(TokenExpression.Avg))
            {
                return CreateToken(TokenKind.Avg, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanMin(string d)
        {

            if (Scan(TokenExpression.Min))
            {
                return CreateToken(TokenKind.Min, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanMax(string d)
        {

            if (Scan(TokenExpression.Max))
            {
                return CreateToken(TokenKind.Max, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanMedium(string d)
        {

            if (Scan(TokenExpression.Medium))
            {
                return CreateToken(TokenKind.Medium, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanMean(string d)
        {

            if (Scan(TokenExpression.Mean))
            {
                return CreateToken(TokenKind.Mean, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanMode(string d)
        {

            if (Scan(TokenExpression.Mode))
            {
                return CreateToken(TokenKind.Mode, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanCalculate(string d)
        {

            if (Scan(TokenExpression.calculate))
            {
                return CreateToken(TokenKind.Calculate, word, d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanRange(string d)
        {

            if (Scan(TokenExpression.Range))
            {
                return CreateToken(TokenKind.Range, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanJoin(string d)
        {

            if (Scan(TokenExpression.Join))
            {
                return CreateToken(TokenKind.Join, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanGetValue(string d)
        {

            if (Scan(TokenExpression.GetValue))
            {
                return CreateToken(TokenKind.GetValue, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanGetValues(string d)
        {

            if (Scan(TokenExpression.GetValues))
            {
                return CreateToken(TokenKind.GetValues, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanTostring(string d)
        {

            if (Scan(TokenExpression.Tostring))
            {
                return CreateToken(TokenKind.toString, word,d);
            }
            return null;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanExpr(string d)
        {

            if (Scan(TokenExpression.Expr))
            {
                return CreateToken(TokenKind.Expr, word,d);
            }
            return null;
        }
         
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token ScanLiteral(string d)
        {

            if (Scan(TokenExpression.literal))
            {
                return CreateToken(TokenKind.literal, word,d);
            }
            return null;
        }


        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        private Token Scanif(string d)
        {

            if (Scan(TokenExpression.ifstate))
            {
                return CreateToken(TokenKind.If, word,d);
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
        private Token CreateToken(TokenKind kind, string txt = "",string right="")
        {
            string contents = txt;
            SourceLocation end = new SourceLocation(_index, _line, _column);
            SourceLocation start = _tokenStart;
            _tokenStart = end;
            _builder.Clear();

            return new Token(kind, contents, right, start, end);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsEOF()
        {
            if (_line <= _sourceCode.Lines.Length)
            {
                return false;
            }
            else return true;
        }

    }
}
