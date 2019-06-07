using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.QScript.Source;
namespace Tech.QScript
{
    public class QScriptStatement
    {
        public Result Result;
        private EvaluationParam _evp;
        public EvaluationParam EvalutionParam => _evp;
        private SourceCode sourceCode;
        public string Error { get; private set; }
        dynamic Content;
        public QScriptStatement(string statements, EvaluationParam evp) {
            sourceCode= new Tech.QScript.Source.SourceCode(statements);
            Result = new Result();
            _evp= evp;
        }
        public bool HasError() {
            try
            {
                Tech.QScript.Lexer.QLexer lex = new QScript.Lexer.QLexer();
                var tokens = lex.LexFile(sourceCode).ToList();
                tokens= tokens.Where(x => x != null).ToList();
                Tech.QScript.Parser.QScriptParser parser = new QScript.Parser.QScriptParser();
                 Content = parser.ParseFile(sourceCode, tokens);
                if (parser.ErrorSink.ToList().Count > 0)
                {
                    var sk = parser.ErrorSink.ToList();
                    for (int i=0;i<parser.ErrorSink.ToList().Count; i++)
                    {
                        Error = Error + "\n"  +sk[i].Message + "\n";
                    }
                    return true;
                }
                else {
                    return false;
                }
            }
            catch (Exception e) {
                Error = e.Message;
                return true;              
            }           
        }
        public dynamic Evaluation() {
            if (HasError())
            {
                return Error;
            }
            else {
                foreach (Syntax.SyntaxNode sn in Content) {
                     Compile(sn);
                };
                return Result;
            }
        }
        
        private object GetValue(dynamic c) {
            var name = "";
            
            if ((c.GetType().Name).ToLower() == "string")
            {
                name = c;
            }
            else {
                name = c.AssignTo.Name;
            }
            var aa = ((List<Tech.QScript.Syntax.SyntaxNode>)Content).Where(x => ((Syntax.SyntaxNode)x).Catagory == Syntax.SyntaxCatagory.Declaration).ToList();
            var a = (Syntax.Declare)(aa).Where(x => ((Syntax.Declare)x).Name == name).ToList().FirstOrDefault();
            if (a != null)
            {
                return a.GetResult(name);
            }
            else {
                return null;
            }
        }
        private void AssignTo(dynamic c, object val) {
            var aa = ((List<Tech.QScript.Syntax.SyntaxNode>)Content).Where(x => ((Syntax.SyntaxNode)x).Catagory == Syntax.SyntaxCatagory.Declaration).ToList();
            var a = (Syntax.Declare)(aa).Where(x => ((Syntax.Declare)x).Name == c.AssignTo.Name).ToList().FirstOrDefault();
            if (a != null)
            {
                var k = new EvalutionVisitor();                
                if (c.AssignTo != null) {
                    a.setResult(c.AssignTo.Name, val);
                    setResult(c.AssignTo.Name, val);
                }                
                a.Accept(k,EvalutionParam);                
            }
        }
        private void setResult(string name,object val) {
           // Result = new Result();
            Result.AddProperty(name, val);
        }
        private void Compile(dynamic c) {
            if (c.Catagory == Syntax.SyntaxCatagory.Declaration )
            {
                var v = new EvalutionVisitor();
                c.Accept(v, _evp);
               setResult(c.Name,"");              
            }
            else if (c.Catagory == Syntax.SyntaxCatagory.Expression)
            {
                var aa = ((List<Tech.QScript.Syntax.SyntaxNode>)Content).Where(x => ((Syntax.SyntaxNode)x).Catagory == Syntax.SyntaxCatagory.Declaration).ToList();
                foreach (Syntax.Declare ss in aa)
                {
                    Syntax.Declare d = (Syntax.Declare)ss;
                    c.ReplaceWith(d.Name, d.GetResult(d.Name));
                    }

                var v = new EvalutionVisitor();
                c.Accept(v, _evp);
                AssignTo(c, v.result.Value);                
            }
            else if (c.Catagory == Syntax.SyntaxCatagory.function)
            {
                var v = new EvalutionVisitor();
                var rslt = GetValue(((Tech.QScript.Syntax.Fun)c).Datasource);
                c.AddSource(rslt);
                c.Accept(v, _evp);
                AssignTo(c, v.result.Value);               
            }
            else if (c.Catagory == Syntax.SyntaxCatagory.Literal)
            {
                var v = new EvalutionVisitor();
                c.Accept(v, _evp);
                AssignTo(c, v.result.Value);
            }
            else if (c.Catagory == Syntax.SyntaxCatagory.Query)
            {
                var v = new EvalutionVisitor();
                c.Accept(v, _evp);                 
                AssignTo(c, v.result.Value);
            }
            else if (c.Catagory == Syntax.SyntaxCatagory.Statement)
            {
                var aa = ((List<Tech.QScript.Syntax.SyntaxNode>)Content).Where(x => ((Syntax.SyntaxNode)x).Catagory == Syntax.SyntaxCatagory.Declaration).ToList();
                foreach (Syntax.Declare ss in aa)
                {
                    Syntax.Declare d = (Syntax.Declare)ss;
                    c.addText( c.Text.Replace("[" + d.Name +"]", d.GetResult(d.Name)));
                }

                var v = new EvalutionVisitor();
                c.Accept(v, _evp);
                AssignTo(c, v.result.Value);
            }
            else
            {
                
            }
           

        }



    }

  
}


