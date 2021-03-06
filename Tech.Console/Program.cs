﻿using System;
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
using MySql.Data.MySqlClient;
using System.Reflection;
using System.IO;
using System.Data;

namespace Tech.Console
{
    class Program
    {
        public static List<string> getExpression(string myString) {
            return myString.Split('"')
                     .Select((element, index) => index % 2 == 0  // If even index
                                           ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)  // Split the item
                                           : new string[] { element })  // Keep the entire item
                     .SelectMany(element => element).ToList();

        }
        public static string generateID()
        {
            long i = 1;

            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }

            string number = String.Format("{0:d4}", (DateTime.Now.Ticks / 10) % 1000000000);

            return number;
        }
        private static string CreateUser() {
            //Tz.ClientManager.User u = new Tz.ClientManager.User("sampath",
            //    "312", Tz.ClientManager.UserType.Admin, true,"sampath","kumar","sampath1983jan@gmail.com");
            //u.Save();
            //return u.UserID;
            return "";
        }
        private static void RemoveUser(string uid) {
            //Tz.ClientManager.User u = new Tz.ClientManager.User(uid);
            //u.Remove();
        }

        private static void PrintUser() {
            System.Console.Write(CreateUser());
            System.Console.ReadKey();
        }
        private static string CreateServer() {
            Tz.ClientManager.Server s = new Tz.ClientManager.Server("dell6", "talentozdev", "root", "admin312", 3006,"testing");
            s.Save();
            return s.ServerID;
        }
        private static void PrintServer() {
            System.Console.Write(CreateServer());
            System.Console.ReadKey();
        }
        private static void UpdateSever(string serverid)
        {

            Tz.ClientManager.Server ss = new Tz.ClientManager.Server(serverid);
            ss.Password = "welcometoq1";
            System.Console.Write(ss.Save());
            System.Console.ReadKey();
        }
        private static void RemoveServer(string serverid)
        {
           

            Tz.ClientManager.Server s = new Tz.ClientManager.Server(serverid);

            System.Console.Write(s.Remove());
            System.Console.ReadKey();
        }
        private static string CreateClient() {
            Tz.ClientManager.Client c = new Tz.ClientManager.Client("sampath kumar",
                "345435 34543",
                "asff",
                "sdfsdfds",
                "sfff",
                "sdfsdf",
                "2342423",
                "saaraaa",
                "talentoz.com",
                true
                );
            c.Save();
            return c.ClientID;
        }
        private static void RemoveClient(string clientid) {
            Tz.ClientManager.Client c = new Tz.ClientManager.Client(clientid);
            System.Console.Write(c.Remove());
            System.Console.ReadKey();
        }
        private static void Setup() {
            string s = Tech.Console.Properties.Resources.Conn;
          
            Tz.Deployment.Setup ss = new Tz.Deployment.Setup();
            try
            {
                ss.Reset(s);
                ss.Execute(s);
                ss.ExecuteClientSetup(s);
                System.Console.Write("stepup created");
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }
            finally
            {
                System.Console.ReadKey();
            }
        }
        private static void UpdateClient(string clientid) {
            Tz.ClientManager.Client c = new Tz.ClientManager.Client(clientid);
            c.OrganizationName = "Forziatech";
            System.Console.Write(c.Save());
        }

        private static void CreateTable(out string tableid, out string serverid) {
            string tablename = "";
            System.Console.WriteLine("Enter TableName");
            tablename = System.Console.ReadLine();
            System.Console.WriteLine("Enter Field name seperated by ,");
            string fields = "";
            tableid = "";
            fields = System.Console.ReadLine();
            var fs = fields.Split(',');
            serverid = CreateServer();
            if (serverid != "") {
                Tz.Net.DataManager dm = new Tz.Net.DataManager(serverid,"");
                dm.NewTable(tablename, "sys");
                foreach (string s in fs)
                {
                    if (s == "UserID")
                    {
                        dm.AddPrimarykey(s, System.Data.DbType.String, 25);
                        //  dm.AddField(s, System.Data.DbType.String, 25, true);
                    }
                    else {
                        dm.AddField(s, System.Data.DbType.String, 25, true);
                    }

                }
                try
                {
                    dm.AcceptChanges();
                    tableid = dm.getTableID();
                    System.Console.WriteLine("table created");
                    System.Console.ReadLine();
                }
                catch (System.Exception ex) {
                    tableid = "";
                    System.Console.WriteLine(ex.Message);
                }
            }
        }
        private static void addColumn (string tbID, string serverid){
            Tz.Net.DataManager dm = new Tz.Net.DataManager(tbID, serverid);
            string fields;
            fields = System.Console.ReadLine();
            var fs = fields.Split(',');
            foreach (string s in fs)
            {
                if (s == "UserID")
                {
                    dm.AddPrimarykey(s, System.Data.DbType.String, 25);                 
                }
                else
                {
                    dm.AddField(s, System.Data.DbType.String, 25, true);
                }
            }
            dm.AcceptChanges();
            System.Console.WriteLine("fields added");
            System.Console.ReadLine();
        }

        private static void AlterColumn(string tbid, string serverid) {
            Tz.Net.DataManager dm = new Tz.Net.DataManager(tbid, serverid);
            string fields;
            fields = System.Console.ReadLine();
            fields = "83eabe2691bc40dfb426c9ee741c65f9755807932,changepass,Password";
            var fs = fields.Split(',');
            //foreach (string s in fs)
            //{             
                    dm.ChangeField(fs[0], fs[1], System.Data.DbType.Int32 , 100, true,false,"");
                
            //}
            dm.AcceptChanges();
            System.Console.WriteLine("fields added");
            System.Console.ReadLine();
        }

        private static void DropTable(string tableid,   string serverid) {
          //  string serverid = CreateServer();
            Tz.Net.DataManager dm = new Tz.Net.DataManager(tableid, serverid);          
            try {
                dm.Remove();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            System.Console.WriteLine("droped");
            System.Console.ReadLine();
        }

        //static void Main(string[] args) {

            //MySqlConnection c = new MySqlConnection("Server=talentozstagemysql.mysql.database.azure.com;Initial Catalog=demo1talentoz;Uid=adminstage@talentozstagemysql;Pwd=SMRmme123456*");
            //try
            //{
            //    c.Open();
            //}
            //catch (Exception ex)
            //{


            //}
            //finally {
            //    c.Close();
            //}
            
           

         //   string tabid, serverid;
          //      CreateTable(out tabid, out serverid);
        //    addColumn(tabid, serverid);
         //   AlterColumn("60c1e054e52e4cf5917a9a235d615f86755744925", "5a0601c9263240a98b16e4d68ec7bd3c986933058");
           // DropTable(tabid,serverid);
             // Setup();
           //CreateClient();
           //RemoveClient();
           // UpdateClient();
           //  CreateServer();
           //string key = CreateServer();
           //UpdateSever(key);
           //System.Console.ReadKey();
           //RemoveServer(key);

            //    PrintUser();
            //RemoveUser(CreateUser());
            //System.Console.ReadKey();
        //}

         static void Main(string[] args)
         {
            //    Setup();



            //var strings =Assembly.GetExecutingAssembly().Location ;
            ////Setup();
            //string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            System.Console.WriteLine("Start time:" + DateTime.Now);

            string program = @"p:pUserID=90; p:getfield=F_200115; d:data=getdata(get(sys_user:[UserID,F_200005,F_200015,F_200030, F_200140, F_200115, F_200160, F_200200, F_200205]:a join Employee_Position:[status]:b with[a:userid equalto b:userid] join position:[PositionID]:c with[c:PositionID equalto b:PositionID],[c:ClientID equalto b:ClientID] join businessunit:[F_300015]:bu with[bu:BusinessUnitID equalto c:F_360025] join worklocation:[F_310035]:wl with[wl:WorkLocationID equalto c:F_360030] join department:[F_320005]:dp with[dp:DepartmentID equalto c:F_360035]), and[(b:status isequalto value:1)]);d:result = getvalue(data, getfield)";


            EvaluationParam ev = new EvaluationParam("connection", "Server=smrdbserver;Initial Catalog=talentozdev;Uid=admin;Pwd=admin312");
            QScriptStatement sq = new QScriptStatement(program, ev);
            var res = sq.Evaluation();
            System.Data.DataTable  dataTable = res.data;
           // System.Console.Write(dataTable.ToJSON());
            List<KeyValuePair<string, string>> find = new List<KeyValuePair<string, string>>();
            find.Add(new KeyValuePair<string, string>("-",""));
            find.Add(new KeyValuePair<string, string>(" ", ""));
            find.Add(new KeyValuePair<string, string>(":", "_"));           
            System.Data.DataTable dt = dataTable.FindReplace("F_200140", find, "AliasEmail");
            System.Data.DataTable dt_date = dataTable.DateParse("F_200140", ParseType._YEARMONTH);
            
            System.Console.WriteLine("End time:" +DateTime.Now);
            // System.Console.Write(res.result);        
            System.Data.DataTable dts = new DataTable();
            DataTable dtre = new DataTable();
            dts.TableName = "User";
            dts.Columns.Add(new DataColumn("UserID"));
            dts.Columns.Add(new DataColumn("UserName"));
          DataRow dr=  dts.NewRow();
            dr[0] = "1";
            dr[1] = "sampathkumar";
            dts.Rows.Add(dr);
            dtre.TableName = "info";
            dtre.Columns.Add(new DataColumn("UserID"));
            dtre.Columns.Add(new DataColumn("Password"));
             dr = dtre.NewRow();
            dr[0] = "1";
            dr[1] = "pavithra";
            dtre.Rows.Add(dr);
           var item = new List<ModelField>();
            item .Add(new ModelField("UserID", "User"));
            item.Add(new ModelField("UserName", "User"));
            item.Add(new ModelField("Password", "info"));

       
           var dm = new DataModel(dts).Select(item.ToArray()).Join(dtre,"UserID","UserID");
          DataTable abc=  (DataTable)dm.getResult();
            //    // Setup employee collection
            System.Console.Write(abc.ToJSON());
            System.Console.ReadLine();
            //    //Employees e = new Employees();
            //    //e.Attach(new Clerk());
            //    //e.Attach(new Director());
            //    //// Employees are 'visited'
            //    //e.Accept(new IncomeVisitor());
            //    //e.Accept(new VacationVisitor());
            //    //var program = System.Console.ReadLine();


            //    // Wait for user

            //    //var p = new Tech.QScript.Syntax.Pivot("sdfsd");
            //    //var k = new Tech.QScript.Syntax.Fun(FunctionType.Avg, "");

            //    //try
            //    //{
            //    //    DataAccess.Schema.DataLayer db = new DataAccess.Schema.DataLayer("Server=dell6;Initial Catalog=talentozdev;Uid=root;Pwd=admin312");
            //    //    db.CreateSchool();
            //    //    System.Console.WriteLine("Created...");
            //    //    System.Console.ReadLine();
            //    //}
            //    //catch (Exception ex) {
            //    //    System.Console.WriteLine(ex.Message);
            //    //    System.Console.ReadLine();
            //    //}

            //    //try
            //    //{
            //    //    DataAccess.Schema.DataLayer db = new DataAccess.Schema.DataLayer("Server=dell6;Initial Catalog=talentozdev;Uid=root;Pwd=admin312");
            //    //    //db.Insert(3, "abc school", "mambakkam", "he only");
            //    //    //System.Console.WriteLine("Inserted...");
            //    //    //System.Console.ReadLine();
            //    //    System.Data.DataTable dt = db.Get();
            //    //    System.Console.WriteLine("Total Records..." + dt.Rows.Count);
            //    //    System.Console.ReadLine();
            //    //}

            //    //catch (Exception ex)
            //    //{
            //    //    System.Console.WriteLine(ex.Message);
            //    //    System.Console.ReadLine();
            //    //}
            //    //dynamic person = new DynamicDictionary();
            //    //person.UserName = "sampathkumar";
            //    //person.Email = "Email:sampathkumar@gmail.com";
            //    //person.Shout = new Action(() => { System.Console.WriteLine("Hellooo!!!"); });
            //    //System.Console.WriteLine("Data:" + person.UserName + ":" + person.Email + ":count "  + person.Count);
            //    //person.Shout();
            //    //System.Console.ReadLine();

            //    //  var query = "getdata(get[sdfs,sf,dfsd],from[sf,sf,sdf],and[sdfds,fsfs,s])";

            //    //string myStr = "put(tb,[f1:val,f2:val,f3:val],and[(),(),()])";
            //    //var st= Regex.Split(myStr, @",").Where(s => s != string.Empty); ;

            //    //System.Console.WriteLine(string.Join(",", st));

            //    //string  data = System.Console.ReadLine();

            //    //var d= getExpression(data);
            //    //System.Console.WriteLine(string.Join(",", d));
            //    // System.Console.ReadLine();



            //    //var query = "and[(d:sampath),(sdfs:Sdfsdf)]";
            //    //string rx = @"\((?:(?:p(1))|(?:[^()]))*\)";
            //    //MatchCollection ms= Regex.Matches(query, rx, RegexOptions.IgnoreCase);
            //    //foreach (Match m in ms) {
            //    //    if (m.Success)
            //    //    {
            //    //        var val = Regex.Split(query, rx);

            //    //        System.Console.WriteLine(val[0] + " variable name:" + val[1]);
            //    //    }
            //    //}


            //    while (true)
            //    {
            //        string program = "d:data= getdata(get(sys_user:[userid, F_200115, F_200005,LastUPD]:a join Employee_Position:[status]:b with[a:userid equalto b:userid] join position:[PositionID]:c with[c:PositionID equalto b:PositionID]))"; //,and[(c:F_360010 with value:Project),(b:status isequalto value:1)]
            //        //  program = program + ";data= case(data,status:[equalto True:active position,else:inactive position],positional);";
            //        program = program + "; data= duplicate(data,F_200005,EmployeeName)";
            //        //program = program + "; data= dateparse(data,lastupd,y);";
            //        //program = program + "; data= split(data,lastupd,/,'date');";
            //        // program = program + "; data= replace(data,F_200115,@,#,F_200115);";
            //       // program = program + "; data= changecase(data,F_200115,U);";
            //       // program = program + "; data= trancate(data,F_200115,2);";
            //        //program = program + "; data= calculate(data,calculation,(F_200115 + F_200005));";
            //        program = program + "; d:values= Getvalues(data,F_200115);";

            //        //program = program + ";data= case(data,PositionID:[equalto 124:welcome,else:dontcome],performance);";

            //string a = "0";
            //System.Console.WriteLine("Enter Logical value:");
            //string val = System.Console.ReadLine();
            //System.Console.WriteLine("Enter a Value:");
            //a = System.Console.ReadLine();
            //System.Console.WriteLine("Enter b  Value:");
            //string b = System.Console.ReadLine();

            // string program = "d:val; val=" + val + ";d:a=" + a + ";d:b=" + b + ";d:k;";
            //program = program + "; d:pe=if(mod(" + a + "," + b + "),'i am printing', 'i am not printing');";
            // program = program + "; k=Expr([a] * [b]);";

            ////124
            ////string str = "fkdfdsfdflkdkfk@dfsdfjk72388389@kdkfkdfkkl@jkdjkfjd@jjjk@";
            ////str = str.Replace("@",  "");                
            ////var s = Regex.Split(str, @"join");
            ////System.Console.WriteLine(str);
            ////  System.Console.ReadKey();
            //     var program = "d:data= getdata(get(sys_user:[UserID,F_200005,F_200015,F_200030,F_200140,F_200115,F_200160,F_200200,F_200205]:a join Employee_Position:[status]:b with[a:userid equalto b:userid] join position:[PositionID,F_360010]:c with[c:PositionID equalto b:PositionID,c:ClientID equalto b:ClientID] join businessunit:[F_300015]:bu with[bu:BusinessUnitID equalto c:F_360025] join worklocation:[F_310035]:wl with[wl:WorkLocationID equalto c:F_360030] join department:[F_320005]:dp with[dp:DepartmentID equalto c:F_360035]),and[(b:status isequalto value:1)]);"; /*System.Console.ReadLine();   */
            //var program = "p:pUserID;p:getfield;d:data= getdata(get(sys_user:[UserID,F_200005,F_200015,F_200030,F_200140,F_200115,F_200160,F_200200,F_200205]:a join Employee_Position:[status]:b with[a:userid equalto b:userid] join position:[PositionID,F_360010]:c with[c:PositionID equalto b:PositionID,c:ClientID equalto b:ClientID] join businessunit:[F_300015]:bu with[bu:BusinessUnitID equalto c:F_360025] join worklocation:[F_310035]:wl with[wl:WorkLocationID equalto c:F_360030] join department:[F_320005]:dp with[dp:DepartmentID equalto c:F_360035]),and[(a:UserID isequalto value:90),(b:status isequalto value:1)]);d: result = getvalue(data, F_200005); result = orderby(data,[userid: asc]);";
            ////program = program + ";data=orderby(data,[F_200005:desc,F_200015:asc])";
            //EvaluationParam ev = new EvaluationParam("connection", "Server=dell6;Initial Catalog=talentozdev;Uid=root;Pwd=admin312");
            //QScriptStatement sq = new QScriptStatement(program, ev);
            //var res = sq.Evaluation();
            //System.Console.WriteLine("Total:" + res.data);
            //System.Console.ReadLine();
            //        try
            //        {                    
            //            //for (int i = 0; i < res.Count; i++)
            //            //{
            //                System.Console.WriteLine(res.data);
            //            System.Console.WriteLine(res.values);
            //            //System.Console.WriteLine("Row Count:" + res.data.Rows.Count);
            //            //System.Console.WriteLine("Expression:" + res.k);
            //            //}
            //        }
            //        catch (Exception e)
            //        {
            //            System.Console.WriteLine(res);
            //        }
            //        //foreach (Tech.QScript.Source.ErrorEntry token in parser.ErrorSink.Errors)
            //        //{
            //        //    System.Console.WriteLine("Error in line no " + token.Span.Start.Line + " Message:" + token.Message + " line " + token.Lines[0]);
            //        //}
            //        //if (parser.ErrorSink.Errors.ToList().Count == 0)
            //        //{
            //        //    System.Console.WriteLine("no error");
            //        //}
            //    }


            //    //Tech.QueryParser.Language.Lexer.QueryScriptLexer lexer = new Tech.QueryParser.Language.Lexer.QueryScriptLexer();
            //    //Tech.QueryParser.Language.Parser.QueryScriptParser parser = new Tech.QueryParser.Language.Parser.QueryScriptParser(lexer.ErrorSink);
            //    //while (true)
            //    //{
            //    //    System.Console.Write("GlassScript> ");
            //    //    var program = System.Console.ReadLine();
            //    //    var sourceCode = new SourceCode(program);
            //    //    var tokens = lexer.LexFile(sourceCode).ToArray();
            //    //}
        }
    }
}

