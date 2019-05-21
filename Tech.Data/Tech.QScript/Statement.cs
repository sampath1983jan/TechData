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
        public List<dynamic> Result;

        private SourceCode sourceCode;
        public string Error { get; private set; }
        dynamic Content;
        public QScriptStatement(string statements) {
            sourceCode= new Tech.QScript.Source.SourceCode(statements);
            Result = new List<dynamic>();
        }
        public bool HasError() {
            try
            {
                Tech.QScript.Lexer.QLexer lex = new QScript.Lexer.QLexer();
                var tokens = lex.LexFile(sourceCode).ToList();
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
            var aa = ((List<Tech.QScript.Syntax.SyntaxNode>)Content).Where(x => ((Syntax.SyntaxNode)x).Catagory == Syntax.SyntaxCatagory.Declaration).ToList();
            var a = (Syntax.Declare)(aa).Where(x => ((Syntax.Declare)x).Name == c.AssignTo.Name).ToList().FirstOrDefault();
            if (a != null)
            {
                return a.GetResult();
            }
            else {
                return null;
            }
        }
        private void AssignTo(dynamic c, string val) {
            var aa = ((List<Tech.QScript.Syntax.SyntaxNode>)Content).Where(x => ((Syntax.SyntaxNode)x).Catagory == Syntax.SyntaxCatagory.Declaration).ToList();
            var a = (Syntax.Declare)(aa).Where(x => ((Syntax.Declare)x).Name == c.AssignTo.Name).ToList().FirstOrDefault();
            if (a != null)
            {
                var k = new EvalutionVisitor();
                a.setResult("", val);
                a.Accept(k);
            }
        }
        private void Compile(dynamic c) {
            if (c.Catagory == Syntax.SyntaxCatagory.Declaration)
            {
                var v = new EvalutionVisitor();
                c.Accept(v);
                Result.Add(v.result);
                //var d = new Domain.Declaration();
                //var v = new EvalutionVisitor();
                //d.Accept(c, v);
                //return d;
            }
            else if (c.Catagory == Syntax.SyntaxCatagory.Expression) {                           
                var v = new EvalutionVisitor();
                c.Accept(v);
                AssignTo(c, v.result);          
                Result.Add(v.result);                
            }
            else if (c.Catagory == Syntax.SyntaxCatagory.function)
            {               

                var v = new EvalutionVisitor();
                var rslt = GetValue(c);
                c.AddSource(rslt);
                c.Accept(v);
                AssignTo(c, v.result);
                Result.Add(v.result);
                //var d = new Domain.Function();
                //var v = new EvalutionVisitor();
                //d.Accept(c, v);
                //return d;
                //  System.Data.DataTable dt = new System.Data.DataTable();
            }
            else if (c.Catagory == Syntax.SyntaxCatagory.Literal)
            {
                //var d = new Domain.Value();
                //var v = new EvalutionVisitor();
                //d.Accept(c, v);
                //return d;
            }
            else if (c.Catagory == Syntax.SyntaxCatagory.Query)
            {
                var v = new EvalutionVisitor();
                var rslt = GetValue(c);
                c.AddSource(rslt);
                c.Accept(v);              
                Result.Add(v.result);

                //var d = new Domain.Query();
                //var v = new EvalutionVisitor();
                //d.Accept(c, v);
                //return d;

            }
            else if (c.Catagory == Syntax.SyntaxCatagory.Statement)
            {
                //var d = new Domain.LogicalStatement();
                //var v = new EvalutionVisitor();
                //d.Accept(c, v);
                //return d;
            }
           // return null;

        }


        public static bool IsPropertyExist(dynamic settings, string name)
        {
            Type objType = settings.GetType();

            if (objType == typeof(ExpandoObject))
            {
                return ((IDictionary<string, object>)settings).ContainsKey(name);
            }
            return objType.GetProperty(name) != null;
        }

    }

  
}


