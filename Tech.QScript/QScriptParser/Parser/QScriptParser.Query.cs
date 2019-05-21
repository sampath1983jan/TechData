using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tech.QScript.Syntax;
using Tech.QScript.Syntax.Query;

namespace Tech.QScript.Parser
{
    public sealed partial class QScriptParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private SQuery QueryParser(Declare d,Token tokens) {                       
            return Parse(d,tokens);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool Scan(string exp, string word)
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
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        private string getValue(string exp, string word)
        {         
            Match result = Regex.Match(word, exp, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (result.Success)
            {
                return result.Value.Trim();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        private string[] split(string exp, string word) {
            var st = Regex.Split(word, exp).Where(s => s != string.Empty).ToArray();
            return st;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        private string[] getValues(string exp, string word) {
            List<string> val = new List<string>();
            MatchCollection result = Regex.Matches(word, exp, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            foreach(Match m in result)
            if (m.Success)
            {
                    val.Add(m.Value);
            };
          return   val.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sq"></param>
        /// <param name="gVal"></param>
        private void buildfrom(Syntax.SQuery sq ,string[] gVal)
        {
            var tb = "";
            var tbals = "";
            string[] tf;
            for (int i = 0; i < gVal.Length; i++)
            {
                tf = gVal[i].Split(':');
                if (tf.Length > 0)
                {
                    tb = tf[0];
                    if (tf.Length == 2)
                    {
                        tbals = tf[1];
                    }
                    sq.from.AddTable(tb, tbals);
                }
                else
                {
                    //write error here
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sq"></param>
        /// <param name="gVal"></param>
        private void buildSelect(Syntax.SQuery sq,string item)
        {
            var tb = "";
            string[] gVal = getValues(QueryDefination.getTableWithField, item);
            string[] tf;
            for (int i = 0; i < gVal.Length; i++) // only one record will come here
            {
                string tName = "";
                string tbAlis = "";
                if (Scan(QueryDefination.splitTableName, gVal[i])) { // check table name                  
                    // index 0 tbname index 2 tbalias index 1- table field
                    string[] gv = split(QueryDefination.splitTableName, gVal[i]); // split table name with alias
                    if (gv.Length > 0)
                    {
                        tName = gv[0];
                        if (gv.Length == 3) {
                            tbAlis = gv[2];
                        }
                        string tbf = gv[1].Replace("]","").Replace("[","");
                        string[] tbfs = tbf.Split(',');
                        foreach (string s in tbfs) {
                            tf = s.Split(':');
                            var fld = "";
                            var fldas = "";
                            if (tf.Length > 0)
                            {                                
                                fld = tf[0];
                                if (tf.Length == 2)
                                {
                                    fldas = tf[1];
                                }
                                else {
                                    fldas = fld;
                                }                                
                                sq.AddTableField(tName, fld,fldas, tbAlis);
                            }
                            else
                            {
                                //write error here
                            }
                        }                       

                    }
                    else {
                        //add error 
                    }                    
                }
                if (Scan(QueryDefination.getTableRelation, item)) {
                    string[] gv = getValues(QueryDefination.getTableRelation, item);
                    if (gv.Length > 0)
                    {
                        string relation = gv[0].ToString().Replace("with[", "").Replace("with [", "").Replace("with [ ", "").Replace("]", ""); 
                        buildRelation(sq, relation.ToString().Split(','), tName);
                    }
                    else {
                        //add error
                    }
                }                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="opt"></param>
        /// <returns></returns>
        private WhereOperators scanOperator(string opt) {
            if (opt.ToLower() == "equalto")
            {
                return WhereOperators.Equal;
            }
            else if (opt.ToLower() == "notequalto")
            {
                return WhereOperators.notEqual;
            }
            else if (opt.ToLower() == "contain")
            {
                return WhereOperators.In;
            }
            else if (opt.ToLower() == "notcontain")
            {
                return WhereOperators.NotIn;
            }
            else if (opt.ToLower() == "startwith")
            {
                return WhereOperators.start;
            }
            else if (opt.ToLower() == "endwith")
            {
                return WhereOperators.end;
            }
            else if (opt.ToLower() == "with")
            {
                return WhereOperators.Like;
            }
            else if (opt.ToLower() == "greater") {
                return WhereOperators.greater;
            }
            else if (opt.ToLower() == "greaterthan")
            {
                return WhereOperators.greaterthan;
            }
            else if (opt.ToLower() == "lesser")
            {
                return WhereOperators.lesser;

            }
            else if (opt.ToLower() == "lesserthan")
            {
                return WhereOperators.lesserthan;

            }
            else
            {
                return WhereOperators.none;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sq"></param>
        /// <param name="gVal"></param>
        /// <param name="condition"></param>
        private void buildCaseCondition(Syntax.SQuery sq, string[] gVal, int condition,string caseValue)
        {
            foreach (string s in gVal)
            {
                var ss = "";
                string[] items;
                ss = s;
                items = splitWithSpace(ss).ToArray();
                var oper = WhereOperators.none;
                var fld = "";
                var val = "";
                //foreach (string it in items)
                //{
                if (items.Length == 1)
                {
                    if (items[0].ToLower() == "else")
                    {
                        oper = WhereOperators.defaults;
                    }
                    sq.And("", fld, val, oper, caseValue);
                }
                else if (items.Length == 3)
                {
                    val = items[2];
                    fld = items[0];
                    oper = scanOperator(items[1]);
                    if (oper == WhereOperators.none)
                    {
                        //add error
                    }
                    sq.And("", fld, val, oper, caseValue);
                }
                else {
                    AddError(Source.Severity.Error, "case structure invalid. please check your code");
                }                                           
                
              //  }
            }
        }

        private void buildRelation(Syntax.SQuery sq, string[] gVal,string basetb) {
            foreach (string s in gVal)
            {
                var ss = "";
                string[] items;
                ss = s.Replace("(", "").ToString().Replace(")", "");
                // buildAndorCondition(sq, gVal, 1);
                items = Regex.Split(ss, @"\s+").Where(x => x != string.Empty).ToArray();
                bool isleft = false;
                string lefttb = "";
                string leftfld = "";
                string righttb = "";
                string rightfld = "";
                string val = "";
                var oper = WhereOperators.none;

                foreach (string it in items)
                {
                    if (Scan(QueryDefination.getFields, it)) // check table field
                    {
                        var tf = split(":", it);
                        if (tf.Length == 2 && tf[0].ToLower() != "value")
                        {
                            if (isleft == false)
                            {
                                isleft = true;

                                lefttb = tf[0];
                                if (tf[1] != "")
                                {
                                    leftfld = tf[1];
                                }
                                else
                                {
                                    //throw exception
                                }
                            }
                            else
                            {
                                righttb = tf[0];
                                if (tf[1] != "")
                                {
                                    rightfld = tf[1];
                                }
                                else
                                {
                                    //throw exception
                                }
                            }
                        }
                        else if (tf[0].ToLower() == "value")
                        {
                            val = tf[1];
                        }
                        else
                        {
                            //throw exception
                        }
                    }
                    else
                    {
                        oper = scanOperator(it);
                        if (oper == WhereOperators.none)
                        {
                            //throw error unknow expression here
                        }
                    }
                }
                sq.AddRelation(basetb, lefttb, leftfld, righttb, rightfld, oper);
            }            
//            buildAndorCondition(sq, gVal, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sq"></param>
        /// <param name="gVal"></param>
        /// <param name="condition">and-1, or-2</param>
        private void buildAndorCondition(Syntax.SQuery sq, string[] gVal,int condition) {
            foreach (string s in gVal) {
                var ss = "";
                string[] items;
                ss = s.Replace("(", "").ToString().Replace(")", "");
                items = Regex.Split(ss, @"\s+").Where(x => x != string.Empty).ToArray() ;
               var oper = WhereOperators.none;
                bool isleft = false;
                string lefttb = "";
                string leftfld = "";
                string righttb = "";
                string rightfld = "";
                string val = "";
                foreach (string it in items) {
                    if (Scan(QueryDefination.getFields, it)) // check table field
                    {
                        var tf = split(":", it);
                        if (tf.Length == 2 && tf[0].ToLower() != "value")
                        {
                            if (isleft == false)
                            {
                                isleft = true;

                                lefttb = tf[0];
                                if (tf[1] != "")
                                {
                                    leftfld = tf[1];
                                }
                                else
                                {
                                    //throw exception
                                }
                            }
                            else
                            {
                                righttb = tf[0];
                                if (tf[1] != "")
                                {
                                    rightfld = tf[1];
                                }
                                else
                                {
                                    //throw exception
                                }
                            }
                        }
                        else if (tf[0].ToLower() == "value")
                        {
                            val = tf[1];
                        }
                        else {
                            //throw exception
                        }
                    }                    
                    else {
                        oper = scanOperator(it);
                        if (oper == WhereOperators.none)
                        {
                            //throw error unknow expression here
                        }                        
                    }
                }
                if (condition == 1)
                {
                    if (val != "")
                    {
                        sq.And(lefttb, leftfld, val, oper);
                    }
                    else
                    {
                        sq.And(lefttb, leftfld, righttb, rightfld, oper);
                    }
                }
                else {
                    if (val != "")
                    {
                        sq.Or(lefttb, leftfld, val, oper);
                    }
                    else
                    {
                        sq.Or(lefttb, leftfld, righttb, rightfld, oper);
                    }
                }
                   
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sq"></param>
        /// <param name="gVal"></param>
        private void buildGetinto(Syntax.SQuery sq, string[] gVal)
        {
            if (gVal.Length == 2)
            {
                //1.get method variable name;
                //2.alias name of the table
                if (_tokens.Where(x => x.Catagory == TokenCatagory.Declaration && x.Value == "d:" + (gVal[0])).ToList().Count > 0)
                {
                    //throw error;
                }
                else
                {
                    sq.addGetInto(gVal[0], gVal[1]);
                }
            }
            else {
                //throw error
            }            
        }
        private void buildPush(Syntax.SQuery sq, string[] gVal) {
            if (gVal.Length == 2)
            {
                //1. table
                //2. Field & value (fd:value);
                var tbName = gVal[0];
                 var tbfields = gVal[1].Replace("[", "").Replace("]", "").Split(',');
                for (int i = 0; i < tbfields.Length; i++) {
                    var tbfld = split(":", tbfields[i]).ToList();                    
                    if (tbfld.Count == 2)
                    {
                        sq.AddTableField(tbName, tbfld[0], tbfld[1]);
                    }
                    else {
                        //throw error 
                    }                    
                }                
            }
            else {
                // throw error;
            }
        }
        private void buildPut(Syntax.SQuery sq, string[] gVal)
        {
            if (gVal.Length >= 2)
            {
                //1. table
                //2. Field & value (fd:value);
                var tbName = gVal[0];
                var tbfields = gVal[1].Replace("[", "").Replace("]", "").Split(',');
                //var condition = @",(?![^\(\[]*[\]\)])";
                for (int i = 0; i < tbfields.Length; i++)
                {
                    var tbfld = tbfields[i].Split(':').Where(s => s != string.Empty).ToList();
                    var val = tbfields[i].Split(':').Where(s => s != string.Empty);
                    if (tbfld.Count >= 2)
                    {
                        sq.AddTableField(tbName, tbfld[0], tbfld[1]);
                    }
                    else
                    {                        
                        //throw error 
                    }
                }
                for (int i = 2; i < gVal.Length; i++) {
                    if (Scan(QueryDefination.andcondition, gVal[i]))
                    {
                        string[] str;
                        str = getValues(QueryDefination.andcondition, gVal[i]);
                        foreach (string s in str)
                        {
                           
                           var tf = getValues(QueryDefination.andorconditionExtract, s);
                            buildAndorCondition(sq, tf, 1);
                        }
                    }
                    else if (Scan(QueryDefination.orcondition, gVal[i])) {
                        string[] str;
                        str = getValues(QueryDefination.orcondition, gVal[i]);
                        foreach (string s in str)
                        {
                           var tf = getValues(QueryDefination.andorconditionExtract, s);
                            buildAndorCondition(sq, tf, 1);
                            
                        }
                    }
                    //buildAndorCondition()
                }
            }
            else
            {
                // throw error;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sq"></param>
        /// <param name="gVal"></param>
        private void buildCase(Syntax.SQuery sq, string[] gVal) {

            var dSource = "";
            if (gVal.Length == 3)
            {
                dSource = gVal[0];
                if (this._tokens.Where(x => x.Kind == TokenKind.d && x.Value == dSource).ToList().Count == 0)
                {
                    AddError(Source.Severity.Error, "Variable name " + dSource + " dosen't exist");
                }

                string conditions = gVal[1];
                conditions = conditions.Replace(@"[", "").Replace(@"]", "");
                var items = split(@",", conditions).ToArray();
                for (int i = 0; i < items.Length; i++) {
                    var citem = split(@":", items[i]);
                    List<string> c = new List<string>();
                    c.Add(citem[0]);
                    buildCaseCondition(sq, c.ToArray(), 1,citem[1]);
                }
            }
            else {
                AddError(Source.Severity.Error, "Invalid case systax");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sq"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool extractGetQuery(Syntax.SQuery sq, Token token)
        {
            var gVal = "";
            string[] tf;
            if (token.Catagory == TokenCatagory.Query)
            {
                if (token.Kind == TokenKind.Get)
                {
                    if (Scan(QueryDefination.getdata, token.Value)) {
                        gVal = getValue(QueryDefination.getdata, token.Value.Trim());
                        tf = split(@"(?![^)(]*\([^)(]*?\)\)),(?![^\[]*\])", gVal).ToArray(); //split first level comma value only

                        gVal = tf[0].Replace("get(", "").Replace(")", "");

                        string[] s = Regex.Split(gVal, @"join");
                        for (int i = 0; i < s.Length; i++) {
                            string item = s[i];                         
                                                       
                            if (Scan(QueryDefination.getTableWithField, item)) // first find table field schema
                            {
                            
                                buildSelect(sq, item);
                            }
                            else {
                                //add error
                            }
                        }
                        bool isFirst = true;
                        foreach (string cd in tf) {
                            if (isFirst == true) {
                                isFirst = false;
                                continue;
                            }
                            if (Scan(QueryDefination.andcondition, cd))
                            {
                                string[] str;
                                str = getValues(QueryDefination.andcondition, cd);
                                foreach (string ss in str)
                                {

                                    var itm = getValues(QueryDefination.andorconditionExtract, ss);
                                    buildAndorCondition(sq, itm, 1);
                                }
                            }
                            if (Scan(QueryDefination.orcondition, cd))
                            {
                                var itm = getValues(QueryDefination.orcondition, cd);
                                buildAndorCondition(sq, itm, 2);
                            }
                        }                        
                    }
                   


                    //if (Scan(QueryDefination.get, token.Value))
                    //{
                    //    gVal = getValue(QueryDefination.get, token.Value);
                    //    gVal = gVal.Replace("get[", "").Replace("]", "");
                    //    tf = gVal.Split(',');
                    //    buildSelect(sq, tf);
                    //}
                    //if (Scan(QueryDefination.from, token.Value))
                    //{
                    //    gVal = getValue(QueryDefination.from, token.Value);
                    //    gVal = gVal.Replace("from[", "").Replace("]", "");
                    //    tf = gVal.Split(',');
                    //    buildfrom(sq, tf);
                    //}
                    //else
                    //{
                    //    AddError(Source.Severity.Error, "From missing in 'get'", token.Span);
                    //}
                    //if (Scan(QueryDefination.andcondition, token.Value))
                    //{
                    //    string[] str;
                    //    str = getValues(QueryDefination.andcondition, token.Value);
                    //    foreach (string s in str)
                    //    {
                    //        tf = getValues(QueryDefination.andorconditionExtract, s);
                    //        buildAndorCondition(sq, tf, 1);
                    //    }
                    //}
                    //if (Scan(QueryDefination.orcondition, token.Value))
                    //{
                    //    tf = getValues(QueryDefination.orcondition, token.Value);
                    //    buildAndorCondition(sq, tf, 2);
                    //}
                }
                else if (token.Kind == TokenKind.GetInto)
                {
                    if (Scan(QueryDefination.getinto, token.Value))
                    {
                        gVal = getValue(QueryDefination.getinto, token.Value);
                        //gVal = gVal.Replace("[", "").Replace("]", "");
                        tf = split(@"(?![^)(]*\([^)(]*?\)\)),(?![^\[]*\])", gVal).ToArray(); //split first level comma value only                        
                        buildGetinto(sq, tf);
                    }
                }
                else if (token.Kind == TokenKind.Case)
                {
                    if (Scan(QueryDefination.Case, token.Value))
                    {
                        tf = split(@"(?![^)(]*\([^)(]*?\)\)),(?![^\[]*\])", token.Value); // spit first level comma with square bracket
                        buildCase(sq, tf);
                    }
                }
                else if (token.Kind == TokenKind.Push)
                {
                    if (Scan(QueryDefination.push, token.Value))
                    {
                        gVal = getValue(QueryDefination.push, token.Value);
                        tf = split(@"(?![^)(]*\([^)(]*?\)\)),(?![^\[]*\])", gVal).ToArray(); //split first level comma value only
                        buildPush(sq, tf);
                    }
                }
                else if (token.Kind == TokenKind.Put)
                {
                    gVal = getValue(QueryDefination.push, token.Value);
                    tf = split(@"(?![^)(]*\([^)(]*?\)\)),(?![^\[]*\])", gVal).ToArray();//split first level comma value only
                    buildPut(sq, tf);
                }
            }
            return false;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private SQuery Parse(Declare d, Token tokens)
        {
            SQuery sq = new SQuery(d,QueryType.select, tokens.Span);

            if (tokens.Kind == TokenKind.Get)
            {
                sq = new SQuery(d, QueryType.select, tokens.Span);

            }
            else if (tokens.Kind == TokenKind.GetInto)
            {
                sq = new SQuery(d, QueryType.selectinto, tokens.Span);

            }
            else if (tokens.Kind == TokenKind.Push)
            {
                sq = new SQuery(d, QueryType.push, tokens.Span);

            }
            else if (tokens.Kind == TokenKind.Put)
            {
                sq = new SQuery(d, QueryType.put, tokens.Span);
            }
            else if (tokens.Kind == TokenKind.Case)
            {
                sq = new SQuery(d, QueryType.Cases, tokens.Span);
            }
            else {
                //add error;
            }
            extractGetQuery(sq, tokens);
            return sq;                      
        }

        public static List<string> splitWithSpace(string myString)
        {
            return myString.Split('"')
                     .Select((element, index) => index % 2 == 0  // If even index
                                           ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)  // Split the item
                                           : new string[] { element })  // Keep the entire item
                     .SelectMany(element => element).ToList();

        }
    }
}
