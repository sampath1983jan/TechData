using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tech.Data.Query;
using Tech.DataAccess;
using Tech.DataAccess.Tables;
using Tech.QScript.Syntax;
using Tech.QScript;
using Tech.QueryParser.Language;

namespace Tech.Console
{
    class Program
    {
        public static List<string> getExpression(string myString) {
            return  myString.Split('"')
                     .Select((element, index) => index % 2 == 0  // If even index
                                           ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)  // Split the item
                                           : new string[] { element })  // Keep the entire item
                     .SelectMany(element => element).ToList();

        }

        static void Main(string[] args)
        {

            // Setup employee collection

            //Employees e = new Employees();
            //e.Attach(new Clerk());
            //e.Attach(new Director());
            //// Employees are 'visited'
            //e.Accept(new IncomeVisitor());
            //e.Accept(new VacationVisitor());
            //var program = System.Console.ReadLine();


            // Wait for user

            //var p = new Tech.QScript.Syntax.Pivot("sdfsd");
            //var k = new Tech.QScript.Syntax.Fun(FunctionType.Avg, "");

            //try
            //{
            //    DataAccess.Schema.DataLayer db = new DataAccess.Schema.DataLayer("Server=dell6;Initial Catalog=talentozdev;Uid=root;Pwd=admin312");
            //    db.CreateSchool();
            //    System.Console.WriteLine("Created...");
            //    System.Console.ReadLine();
            //}
            //catch (Exception ex) {
            //    System.Console.WriteLine(ex.Message);
            //    System.Console.ReadLine();
            //}

            //try
            //{
            //    DataAccess.Schema.DataLayer db = new DataAccess.Schema.DataLayer("Server=dell6;Initial Catalog=talentozdev;Uid=root;Pwd=admin312");
            //    //db.Insert(3, "abc school", "mambakkam", "he only");
            //    //System.Console.WriteLine("Inserted...");
            //    //System.Console.ReadLine();
            //    System.Data.DataTable dt = db.Get();
            //    System.Console.WriteLine("Total Records..." + dt.Rows.Count);
            //    System.Console.ReadLine();
            //}

            //catch (Exception ex)
            //{
            //    System.Console.WriteLine(ex.Message);
            //    System.Console.ReadLine();
            //}
            //dynamic person = new DynamicDictionary();
            //person.UserName = "sampathkumar";
            //person.Email = "Email:sampathkumar@gmail.com";
            //person.Shout = new Action(() => { System.Console.WriteLine("Hellooo!!!"); });
            //System.Console.WriteLine("Data:" + person.UserName + ":" + person.Email + ":count "  + person.Count);
            //person.Shout();
            //System.Console.ReadLine();

            //  var query = "getdata(get[sdfs,sf,dfsd],from[sf,sf,sdf],and[sdfds,fsfs,s])";

            //string myStr = "put(tb,[f1:val,f2:val,f3:val],and[(),(),()])";
            //var st= Regex.Split(myStr, @",").Where(s => s != string.Empty); ;

            //System.Console.WriteLine(string.Join(",", st));

            //string  data = System.Console.ReadLine();

            //var d= getExpression(data);
            //System.Console.WriteLine(string.Join(",", d));
            // System.Console.ReadLine();



            //var query = "and[(d:sampath),(sdfs:Sdfsdf)]";
            //string rx = @"\((?:(?:p(1))|(?:[^()]))*\)";
            //MatchCollection ms= Regex.Matches(query, rx, RegexOptions.IgnoreCase);
            //foreach (Match m in ms) {
            //    if (m.Success)
            //    {
            //        var val = Regex.Split(query, rx);

            //        System.Console.WriteLine(val[0] + " variable name:" + val[1]);
            //    }
            //}


            while (true)
            {
               
                var program = System.Console.ReadLine();
              QScriptStatement sq = new QScriptStatement(program);
               var res= sq.Evaluation();
                for (int i = 0; i < res.Count; i++) {
                    System.Console.WriteLine(res[i].Value);
                }
                

                //foreach (Tech.QScript.Source.ErrorEntry token in parser.ErrorSink.Errors)
                //{
                //    System.Console.WriteLine("Error in line no " + token.Span.Start.Line + " Message:" + token.Message + " line " + token.Lines[0]);
                //}
                //if (parser.ErrorSink.Errors.ToList().Count == 0)
                //{
                //    System.Console.WriteLine("no error");
                //}
            }


            //Tech.QueryParser.Language.Lexer.QueryScriptLexer lexer = new Tech.QueryParser.Language.Lexer.QueryScriptLexer();
            //Tech.QueryParser.Language.Parser.QueryScriptParser parser = new Tech.QueryParser.Language.Parser.QueryScriptParser(lexer.ErrorSink);
            //while (true)
            //{
            //    System.Console.Write("GlassScript> ");
            //    var program = System.Console.ReadLine();
            //    var sourceCode = new SourceCode(program);
            //    var tokens = lexer.LexFile(sourceCode).ToArray();
            //}
        }
    }
}
