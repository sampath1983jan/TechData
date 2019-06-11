using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.QScript.Syntax;
using Tech.QScript.Parser;
namespace Tech.QScript.Parser
{
   public sealed partial class QScriptParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignTo"></param>
        /// <returns></returns>
        public Fun FunctionParser(Declare assignTo) {
            return parseFunction(assignTo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="opt"></param>
        /// <returns></returns>
        private string getOperator(string opt) {
             
                if (opt.ToLower() == "equalto")
                {
                    return "=";
                }
                else if (opt.ToLower() == "notequalto")
                {
                    return "<>";
                }
                else if (opt.ToLower() == "contain")
                {
                    return "LIKE ";
                }
                else if (opt.ToLower() == "notcontain")
                {
                    return "NOT LIKE ";
                }
                else if (opt.ToLower() == "startwith")
                {
                    return "";
                }
                else if (opt.ToLower() == "endwith")
                {
                    return "";
                }
                else if (opt.ToLower() == "with")
                {
                    return "";
                }
                else if (opt.ToLower() == "greater")
                {
                    return " > ";
                }
                else if (opt.ToLower() == "greaterthan")
                {
                    return " >= ";
                }
                else if (opt.ToLower() == "lesser")
                {
                    return " < ";

                }
                else if (opt.ToLower() == "lesserthan")
                {
                return " <= ";
                }
                else
                {
                    return "=";
                }
         
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private AggregateType getAggType(string s) {
            if (s.ToLower() == "count")
            {
                return AggregateType.count;
            }
            else if (s.ToLower() == "ucount")
            {
                return AggregateType.ucount;
            }
            else if (s.ToLower() == "min")
            {
                return AggregateType.min;
            }
            else if (s.ToLower() == "max")
            {
                return AggregateType.max;
            }
            else if (s.ToLower() == "sum")
            {
                return AggregateType.sum;
            }
            else if (s.ToLower() == "avg")
            {
                return AggregateType.avg;
            }
            else if (s.ToLower() == "mode")
            {
                return AggregateType.mode;
            }
            else if (s.ToLower() == "mean")
            {
                return AggregateType.mean;
            }
            else if (s.ToLower() == "medium")
            {
                return AggregateType.medium;
            }
            
            else
            {
                return AggregateType.none;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        private Pivot buildPivot(Pivot fn) {
            var gVal = getValue(FunDefination.fun, _current.Value);
            var items = split(FunDefination.splitbyComma, gVal);
            if (items.Length == 4)
            {
                fn.Datasource = items[0];
                var row = items[1];
                var col = items[2];
                var agg = items[3];
                if (QScriptParser.Scan(FunDefination.row, row) == true) {
                    var rs= split(FunDefination.splitbyComma, row.Replace("row:[", "").Replace("]",""));
                    foreach (string s in rs) {
                        fn.AddRowFields(s);
                    }                    
                }else
                {
                    AddError(Source.Severity.Error, "invalid pivot row syntax", _current.Span);
                }
                if (QScriptParser.Scan(FunDefination.column, col) == true)
                {
                    var rs = split(FunDefination.splitbyComma, col.Replace("column:[", "").Replace("]", ""));
                    foreach (string s in rs)
                    {
                        fn.AddColumnFields(s);
                    }
                }
                else {
                    AddError(Source.Severity.Error, "invalid pivot column syntax", _current.Span);
                }
                if (QScriptParser.Scan(FunDefination.agg, agg) == true)
                {
                    var rs = split(FunDefination.splitbyComma, agg.Replace("aggr:[", "").Replace("]", ""));

                    foreach (string s in rs)
                    {
                        var aggitems = split(@":",s);
                        if (aggitems.Length == 2)
                        {
                            if (getAggType(aggitems[1]) != AggregateType.none)
                            {
                                fn.AddAggregate(aggitems[0], getAggType(aggitems[1]));
                            }
                            else {
                                AddError(Source.Severity.Error, "Invalid pivot aggregate syntax", _current.Span);
                            }                            
                        }
                        else {
                            AddError(Source.Severity.Error, "Invalid pivot aggregate syntax", _current.Span);
                        }
                        
                    }

                }
                else {
                    AddError(Source.Severity.Error, "invalid pivot row syntax", _current.Span);
                }

            }
            else {
                AddError(Source.Severity.Error, "Invalid pivot function", _current.Span);
            }
                return fn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        private Join buildJoin(Join fn) {
            var gVal = getValue(FunDefination.fun, _current.Value);
            var items = split(FunDefination.splitbyComma, gVal);
           // foreach (string item in items) {
                var s1 = items[0];
                var s2 = items[1];
                fn.Datasource = s1;
                var relation = items[2].Replace("[","").Replace("]","");
                var rs= splitWithSpace(relation);
                if (rs.Count == 3) {
                    var left = rs[0];
                    var right = rs[2];
                    var opr = rs[1];

                var ld = "";
                var ldf = "";
                var rd = "";
                var rdf = "";
                if (Scan(@"(\w*?:\w*)", left)) {
                    var condition = split(":", left).ToList();
                    if (condition.Count == 2)
                    {
                        ld = condition[0];
                        ldf = condition[1];
                        if (s1 != ld && s2 != ld) {
                            AddError(Source.Severity.Error, "datasource not exist in the relationship : " + ld, _current.Span);
                        }
                    }
                    else {
                        AddError(Source.Severity.Error, "invalid relationship : " + left, _current.Span);
                    }
                }
                if (Scan(@"(\w*?:\w*)", right))
                {
                    var condition = split(":", right).ToList();
                    if (condition.Count == 2)
                    {
                        rd = condition[0];
                        rdf = condition[1];
                        if (s1 != rd && s2 != rd)
                        {
                            AddError(Source.Severity.Error, "datasource not exist in the relationship : " + rd, _current.Span);
                        }
                    }
                    else
                    {
                        AddError(Source.Severity.Error, "invalid relationship : " + left, _current.Span);
                    }
                }
                if (scanOperator(opr) == Syntax.Query.WhereOperators.none) {
                    fn.AddRelation(ld, ldf, rd, rdf, scanOperator(opr));
                }              

                }
            //}
            return fn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        private Merge buildMerge(Merge fn) {
            var gVal = getValue(FunDefination.fun, _current.Value);
            var items = split(FunDefination.splitbyComma, gVal);
            foreach (string item in items) {
                if (_tokens.Where(x => x.Catagory == TokenCatagory.Declaration && x.Value == "d:" + item).ToList().Count > 0)
                {
                    fn.AddSource(item);
                }
                else {
                    AddError(Source.Severity.Error, "Invalid merge: " +  item + " variable doesn't exist"  , _current.Span);
                }                
            }           
            return fn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        private Fun buildGetValuefunction(Fun fn)
        { // count,ucount,min,max,medium,mode,range,filter,avg,
            var gVal = getValue(FunDefination.fun, _current.Value);
            if (gVal != "")
            {
                var items = split(FunDefination.splitbyComma, gVal);
                if (items.Length == 2)
                {
                    fn.Datasource = items[0];
                    fn.AddFields(items[1], FieldType.String,"");                    
                    //fn.AddFields()
                }
                else
                {
                    AddError(Source.Severity.Error, "Invalid function:" + gVal, _current.Span);
                }
            }
            else
            {
                AddError(Source.Severity.Error, "Invalid function", _current.Span);
            }
            return fn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        private Fun buildfunction(Fun fn) { // count,ucount,min,max,medium,mode,range,filter,avg,
            var gVal = getValue (FunDefination.fun, _current.Value);
            if (gVal != "")
            {
                var items = split(FunDefination.splitbyComma, gVal);
                if (items.Length == 3)
                {
                    fn.Datasource = items[0];
                    var conditionstr = split(FunDefination.splitbyComma,items[1].Replace("[", "").Replace("]", "")).ToList();
                    foreach (string s in conditionstr) {
                        var f = split(":", s);
                        if (f.Length == 2)
                        {
                            fn.AddFields(f[0], FieldType.String, f[1]);
                        }
                        else {
                            AddError(Source.Severity.Error, "Invalid function", _current.Span);
                        }
                    }
                    fn.ActionField = items[2];

                    //fn.AddFields()
                }
                else if (items.Length == 2) {
                    fn.Datasource = items[0];
                    var conditionstr = split(FunDefination.splitbyComma, items[1].Replace("[", "").Replace("]", "")).ToList();
                    foreach (string s in conditionstr)
                    {
                        var f = split(":", s);
                        if (f.Length == 2)
                        {
                            fn.AddFields(f[0], FieldType.String, f[1]);
                        }
                        else
                        {
                            AddError(Source.Severity.Error, "Invalid function", _current.Span);
                        }
                    }
                }
            }
            else {
                AddError(Source.Severity.Error, "Invalid function", _current.Span);
            }
            return fn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        private Fun buildCase(Case fn)
        {
            var gVal = getValue(FunDefination.fun, _current.Value);
            var items = split(FunDefination.splitbyComma, gVal);
            if (items.Length == 3) // datasource,condition,newfieldname
            {
                var s1 = items[0];
                var s2 = items[1];
                fn.NewFieldName = items[2];
                fn.Datasource = s1;
               string[] fieldcondition= split(FunDefination.splitbyColn, s2);
                if (fieldcondition.Length == 2)
                {
                    var strcase = fieldcondition[1].Replace("[", "").Replace("]", "");
                    var caseitem = split(FunDefination.splitbyComma, strcase);
                    fn.FieldName = fieldcondition[0];
                  
                    foreach (string s in caseitem)
                    {
                        string[] citem = split(":", s);
                       
                        if (citem.Length == 2)
                        {
                            if (citem[0].IndexOf("else")>=0)
                            {
                                fn.elseValue = citem[1];
                            }else
                            {
                                List<string> rs = splitWithSpace(citem[0]);
                                if (rs.Count == 2)
                                {
                                    fn.AddCase(getOperator(rs[0]),rs[1], citem[1]);
                                }
                                else
                                {
                                    AddError(Source.Severity.Error, "Syntax error in Case : ", _current.Span);
                                }
                            }                            
                        }
                        else
                        {
                            AddError(Source.Severity.Error, "Syntax error in Case : ", _current.Span);
                        }
                    }
                }
                else {
                    AddError(Source.Severity.Error, "Syntax error in Case : ", _current.Span);
                }
                      
            }
            else {
                AddError(Source.Severity.Error, "Syntax error in Case : " , _current.Span);
            }
            return fn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        private Fun buildDateParse(DateParse fn) {
            //dateparse(data,columnName,parsetype);
            var gVal = getValue(FunDefination.fun, _current.Value);
            var items = split(FunDefination.splitbyComma, gVal);
            if (items.Length == 3)
            {
                fn.Datasource = items[0];
                fn.ColumnName = items[1];
                fn.ParseType = items[2];
                // fn.AliasColumnName = items[2];
            }
            else {
                AddError(Source.Severity.Error, "Syntax error in Case : ", _current.Span);
            }
            return fn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        private Fun buildDuplicate(Duplicate fn) {
            //duplidate(data,columnName,newcolumnName);
            var gVal = getValue(FunDefination.fun, _current.Value);
            var items = split(FunDefination.splitbyComma, gVal);
            if (items.Length == 3)
            {
                fn.Datasource = items[0];
                fn.ColumnName = items[1];
                fn.AliasColumnName = items[2];
            }
            else
            {
                AddError(Source.Severity.Error, "Syntax error in Case : ", _current.Span);
            }
            return fn;
        }
        private Fun buildOrder(Order fn) {
            var gVal = getValue(FunDefination.fun, _current.Value);
            var items = split(FunDefination.splitbyComma, gVal);
            if (items.Length == 2)
            {
                fn.Datasource = items[0];
                var order = split(FunDefination.splitbyComma, items[1].Replace("[", "").Replace("]", ""));
                foreach (string s in order)
                {
                    var aa = split(FunDefination.splitbyColn, s);
                    fn.AddField(aa[0], aa[1]);
                }
            }
            else {
                AddError(Source.Severity.Error, "Syntax error in order : ", _current.Span);
            }
            return fn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        private Fun buildSplit(Split fn)
        {
            //split(Data,columnName,spliter,newColumnNameprefix);
            var gVal = getValue(FunDefination.fun, _current.Value);
            var items = split(FunDefination.splitbyComma, gVal);
            if (items.Length ==4)
            {
                fn.Datasource = items[0];
                fn.SplitColumn = items[1];
                fn.ColumnPrefix = items[3];
                fn.Spliter = items[2];
            }
            else
            {
                AddError(Source.Severity.Error, "Syntax error in Case : ", _current.Span);
            }
            return fn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        private Fun buildReplace(Replace fn) {
            //Replace(Data,ColumnName,findstring,replacestring,newcolumnname);
            var gVal = getValue(FunDefination.fun, _current.Value);
            var items = split(FunDefination.splitbyComma, gVal);
            if (items.Length == 5)
            {
                fn.Datasource = items[0];
                fn.ColumnName  = items[1];                
                fn.FindString  = items[2];
                fn.ReplaceString  = items[3];
                fn.NewColumnName = items[4];
            }
            else
            {
                AddError(Source.Severity.Error, "Syntax error in Case : ", _current.Span);
            }
            return fn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        private Fun buildCalculate(Calculate fn) {
            //Calculate(data,cname,formula);
            var gVal = getValue(FunDefination.fun, _current.Value);
            var items = split(FunDefination.splitbyComma, gVal);
            if (items.Length == 3) {
                fn.Datasource = items[0];
                fn.ColumnName = items[1];
                fn.formula = items[2];
            }
            return fn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        private Fun buildTextCase(TextCase fn) {
            //ChangeCase(data,column,U/L/T/C)
            var gVal = getValue(FunDefination.fun, _current.Value);
            var items = split(FunDefination.splitbyComma, gVal);
            if (items.Length == 3)
            {
                fn.Datasource = items[0];
                fn.ColumnName = items[1];
                if (items[2] == "U")
                {
                    fn.CaseType = TextCaseType.UPPER;

                }
                else if (items[2] == "L") {
                    fn.CaseType = TextCaseType.LOWER;
                }
                else if (items[2] == "T")
                {
                    fn.CaseType = TextCaseType.TITLE;
                }
                else if (items[2] == "C")
                {
                    fn.CaseType = TextCaseType.CAPITAL;
                }
                
            }
            else
            {
                AddError(Source.Severity.Error, "Syntax error in Case : ", _current.Span);
            }
            return fn;
        }    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        private Fun buildTrancate(Trancate fn) {
            //Trancate(data,columnname,index);
            var gVal = getValue(FunDefination.fun, _current.Value);
            var items = split(FunDefination.splitbyComma, gVal);
            if (items.Length == 3) {
                fn.Datasource = items[0];
                fn.ColumnName = items[1];
                fn.TrancateIndex = Convert.ToInt32(items[2]);
            }
            return fn;            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        private void ExtractFun(object fn) {
            switch (_current.Kind) {
                case TokenKind.Count:
                case TokenKind.UCount:
                case TokenKind.Min:
                case TokenKind.Max:
                case TokenKind.Avg:
                case TokenKind.Mean:
                case TokenKind.Mode:
                case TokenKind.Range:
                case TokenKind.Sum:
                case TokenKind.Medium:
                case TokenKind.Filter:
                    fn = buildfunction((Fun)fn);
                    break;
                case TokenKind.GetValue:
                    fn = buildGetValuefunction((Fun)fn);
                    break;
                case TokenKind.GetValues:
                    fn = buildGetValuefunction((Fun)fn);
                    break;
                case TokenKind.Pivot:
                 fn =   buildPivot((Pivot)fn);
                    break;
                case TokenKind.Join:
                    buildJoin((Join)fn);
                    break;
                case TokenKind.Merge:
                    buildMerge((Merge)fn);
                    break;
                case TokenKind.Case:
                    buildCase((Case)fn);
                    break;
                case TokenKind.Dateparse:
                    fn = buildDateParse((DateParse)fn);
                    break;
                case TokenKind.ChangeCase:
                    fn = buildTextCase((TextCase)fn);
                    break;
                case TokenKind.ToJson:
                    break;
                case TokenKind.Trancate:
                    fn = buildTrancate((Trancate)fn);
                    break;
                case TokenKind.Columnduplicate:
                    fn = buildDuplicate((Duplicate)fn);
                    break;
                case TokenKind.Order:
                    fn = buildOrder((Order)fn);
                    break;
                case TokenKind.Columnspit:
                    fn = buildSplit((Split)fn);
                    break;
                case TokenKind.Replace:
                    fn = buildReplace((Replace)fn);
                    break;
                case TokenKind.Calculate:
                    fn = buildCalculate((Calculate)fn);
                    break;               
               }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignTo"></param>
        /// <returns></returns>
        private Fun parseFunction(Declare assignTo)
        {
            if (_current.Kind == TokenKind.Count)
            {

                var f = new Fun(assignTo, FunctionType.Count, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.UCount)
            {
                var f = new Fun(assignTo, FunctionType.Count, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Sum)
            {
                var f = new Fun(assignTo, FunctionType.Sum, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Min)
            {
                var f = new Fun(assignTo, FunctionType.Min, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Max)
            {
                var f = new Fun(assignTo, FunctionType.Max, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Mean)
            {
                var f = new Fun(assignTo, FunctionType.Mean, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Mode)
            {
                var f = new Fun(assignTo, FunctionType.Mode, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Medium)
            {
                var f = new Fun(assignTo, FunctionType.Medium, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Avg)
            {
                var f = new Fun(assignTo, FunctionType.Avg, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Filter)
            {
                var f = new Fun(assignTo, FunctionType.Filter, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.GetValue) {
                var f = new Fun(assignTo, FunctionType.GetValue, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.GetValues)
            {
                var f = new Fun(assignTo, FunctionType.GetValues, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Merge)
            {
                var f = new Merge(assignTo, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Join)
            {
                var f = new Join(assignTo, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Range)
            {
                var f = new Fun(assignTo, FunctionType.Range, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.GetValue)
            {
                var f = new Fun(assignTo, FunctionType.GetValue, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.GetValues)
            {
                var f = new Fun(assignTo, FunctionType.GetValues, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Pivot)
            {
                var f = new Pivot(assignTo, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Replace)
            {
                var f = new Replace(assignTo, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Case)
            {
                var f = new Case(assignTo, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Columnduplicate)
            {
                var f = new Duplicate(assignTo, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Columnspit)
            {
                var f = new Split(assignTo, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Dateparse)
            {
                var f = new DateParse(assignTo, "", _current.Span);
                ExtractFun(f);
                return f;

            }
            else if (_current.Kind == TokenKind.ChangeCase)
            {
                var f = new TextCase(assignTo, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Trancate)
            {
                var f = new Trancate(assignTo, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Order)
            {
                var f = new Order(assignTo, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.Calculate)
            {
                var f = new Calculate(assignTo, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else if (_current.Kind == TokenKind.ToJson)
            {
                var f = new TextCase(assignTo, "", _current.Span);
                ExtractFun(f);
                return f;
            }
            else
            {
                var f = new Fun(assignTo, FunctionType.Count, "", _current.Span);
                AddError(Source.Severity.Error, "Invalid function", _current.Span);
                return f;

            }

        }
    }
}
