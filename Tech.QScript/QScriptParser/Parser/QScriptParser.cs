using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.QScript.Source;
using Tech.QScript.Syntax;

namespace Tech.QScript.Parser

{
    public sealed partial class QScriptParser
    {
        private ErrorSink _errorSink;
        private int _index;
        private SourceCode _sourceCode;
        private IEnumerable<Token> _tokens;
        private Token _current => _tokens.ElementAtOrDefault(_index) ?? _tokens.Last();
        private bool _error;
        private Token _last => Peek(-1);
        private Token _next => Peek(1);
        public ErrorSink ErrorSink => _errorSink;
        public QScriptParser()
       : this(new ErrorSink())
        {
        }

        public QScriptParser(ErrorSink errorSink)
        {
            _errorSink = errorSink;
        }

        private void AddError(Severity severity, string message, SourceSpan? span = null)
        {
            _errorSink.AddError(message, _sourceCode, severity, span ?? CreateSpan(_current));
        }

        private SourceSpan CreateSpan(SourceLocation start, SourceLocation end)
        {
            return new SourceSpan(start, end);
        }

        private void Advance()
        {
            _index++;
        }

        private Token Peek(int ahead)
        {
            return _tokens.ElementAtOrDefault(_index + ahead) ?? _tokens.Last();
        }

        private SourceSpan CreateSpan(Token start)
        {
            return CreateSpan(start.Span.Start, _current.Span.End);
        }

        private void InitializeParser(SourceCode sourceCode, IEnumerable<Token> tokens)
        {
            _sourceCode = sourceCode;
            _tokens = tokens.Where(g => !g.IsTrivia()).ToArray();
         //   _options = options;
            _index = 0;
        }

        private SyntaxException SyntaxError(Severity severity, string message, SourceSpan? span = null)
        {
            _error = true;
            AddError(severity, message, span);
         return new SyntaxException(message);
        }

        private Token Take()
        {
            var token = _current;
            Advance();
            return token;
        }
        private Token Take(TokenKind kind)
        {
            if (_current.Kind != kind)
            {
                throw UnexpectedToken(kind);
            }
            return Take();
        }
        private SyntaxException UnexpectedToken(TokenKind expected)
        {
            return UnexpectedToken(expected.ToString());
        }

        private SyntaxException UnexpectedToken(string expected)
        {
            Advance();
            var value = string.IsNullOrEmpty(_last?.Value) ? _last?.Kind.ToString() : _last?.Value;
            string message = $"Unexpected '{value}'.  Expected '{expected}'";
            return SyntaxError(Severity.Error, message, _last.Span);
        }

        private SourceSpan CreateSpan(SourceLocation start)
        {
            return CreateSpan(start, _current.Span.End);
        }

        private Declare getDeclare(List<SyntaxNode> contents) {
               var dy =contents.Where(x => x.Catagory == SyntaxCatagory.Declaration).ToList().ToArray();
            foreach (SyntaxNode ss in dy) {
                Declare d = (Declare)ss;
                if ("d:" +d.Name == _current.Left ||  d.Name == _current.Left || d.Name.Replace("d:","") ==   _current.Left) {
                    return d;
                }
            }
            return null;
        }

        private dynamic ParseDocument() {
            List<SyntaxNode> contents = new List<SyntaxNode>();
            while (_current.Kind != TokenKind.EndOfFile) {
                if (_current.Catagory == TokenCatagory.Declaration)
                {
                    contents.Add(DeclarationParser(_current));
                }
                else if (_current.Catagory == TokenCatagory.Query)
                {
                    contents.Add(QueryParser(getDeclare(contents), _current));
                }
                else if (_current.Catagory == TokenCatagory.Function)
                {
                    contents.Add(FunctionParser(getDeclare(contents)));
                }
                else if (_current.Catagory == TokenCatagory.Statement)
                {
                    contents.Add(StatementParser(getDeclare(contents)));
                }
                else if (_current.Catagory == TokenCatagory.Expression)
                {
                  

                        contents.Add(ParseExpression(getDeclare(contents)));
                }
                else if (_current.Catagory == TokenCatagory.Literal) {
                    contents.Add(ParseValue(getDeclare(contents)));
                }
                Take();
            }            
            return contents;
        }
    }
}
