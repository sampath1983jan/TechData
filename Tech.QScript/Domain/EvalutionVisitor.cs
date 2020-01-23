using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.QScript.Domain;
using Tech.QScript.Syntax;
using System.Data;
using NCalc;

namespace Tech.QScript
{
    public class EvalutionVisitor : IVisitor
    {
        public dynamic result;         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="evp"></param>
        public override void Visit(Declare element, EvaluationParam evp)
        {
            result = new Result();
            if (evp.IsPropertyExist(element.Name))
            {
                result.Value = evp.GetValue(element.Name);
            }
            else {
                result.Value = element.Value;
            }            
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private System.Data.DataTable getRecord( Fun element ) {
            System.Data.DataTable dt = (System.Data.DataTable)element.GetSource();
            var flt = "1=1";
            foreach (ParamFields pf in element.Arguments)
            {
                flt = flt + " AND " + pf.FieldName + " = " + pf.FieldValue;
            }
            dt.DefaultView.RowFilter = flt;
            dt = dt.DefaultView.ToTable(true);
            dt.DefaultView.RowFilter = "";
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="evp"></param>
        public override void Visit(Fun element, EvaluationParam evp)
        {
            if (element.Type == FunctionType.Avg)
            {
                if (element.GetSource() != null)
                {
                    System.Data.DataTable dt = getRecord(element);
                    result = new Result();

                    result.Value = dt.Computing("", element.ActionField, AggregateFunctionE.Average);
                }
            }
            else if (element.Type == FunctionType.Count)
            {
                if (element.GetSource() != null)
                {
                    System.Data.DataTable dt = getRecord(element);
                    result = new Result();
                    result.Value = dt.Rows.Count;
                }
            }
            else if (element.Type == FunctionType.UCount)
            {
                if (element.GetSource() != null)
                {
                    System.Data.DataTable dt = getRecord(element);
                    result = new Result();
                    result.Value = dt.DefaultView.ToTable(true, element.ActionField).Rows.Count;
                }
            }
            else if (element.Type == FunctionType.Min)
            {
                if (element.GetSource() != null)
                {
                    DataTable dt = getRecord(element);
                    result = new Result();
                    result.Value = dt.Computing("", element.ActionField, AggregateFunctionE.Min);
                }
            }
            else if (element.Type == FunctionType.Max)
            {
                if (element.GetSource() != null)
                {
                    System.Data.DataTable dt = getRecord(element);
                    result = new Result();
                    result.Value = dt.Computing("", element.ActionField, AggregateFunctionE.Max);
                }
            }
            else if (element.Type == FunctionType.Sum)
            {
                if (element.GetSource() != null)
                {
                    System.Data.DataTable dt = getRecord(element);
                    result = new Result();
                    result.Value = dt.Computing("", element.ActionField, AggregateFunctionE.Sum);
                }
            }
            else if (element.Type == FunctionType.Case)
            {
                var c = (Case)element;
                System.Data.DataTable dt = getRecord(element);
                result = new Result();
                StringBuilder sb = new StringBuilder();
                sb.Append("{0}");
                foreach (CaseParam cp in c.Cases)
                {
                    StringBuilder s = new StringBuilder();
                    s.AppendFormat("IIF([{0}] {1} '{2}','{3}','{4}')", c.FieldName, cp.Condition, cp.Value, cp.AliasValue, "{0}");
                    string sst = sb.ToString();
                    sb.Clear();
                    sb.AppendFormat(sst, s.ToString());
                }
                if (sb.ToString() != "")
                {
                    string st = sb.ToString();
                    sb.Clear();
                    sb.AppendFormat(st, c.elseValue);
                    if (dt.Columns.Contains(c.NewFieldName) == false)
                    {
                        dt.Columns.Add(c.NewFieldName, typeof(string));
                    }
                    dt.Columns[c.NewFieldName].Expression = sb.ToString();
                    dt.AcceptChanges();
                    result.Value = dt;
                }
            }
            else if (element.Type == FunctionType.Order) {
                var c = (Order)element;
                System.Data.DataTable dt = getRecord(element);
                result = new Result();
                string s = "";
                foreach (ParamFields p in c.Fields) {
                    s = s + "," + p.FieldName + " " + p.Order; 
                }
                if (s.StartsWith(",")) {
                    s = s.Substring(1);
                }                 
                DataView viewFI = new DataView(dt);
                viewFI.Sort = s;
                dt = viewFI.ToTable();
                result.Value = dt;
            }
            else if (element.Type == FunctionType.Pivot)
            {
                var p = (Pivot)element;
                System.Data.DataTable dt = (DataTable)element.GetSource();
                List<string> row = new List<string>();
                List<string> datafield = new List<string>();
                List<AggregateFunctionE> dataagg = new List<AggregateFunctionE>();
                List<string> colfield = new List<string>();
                foreach (ParamFields d in p.RowArguments)
                {
                    row.Add(d.FieldName);
                }
                foreach (ParamFields d in p.AggrArguments)
                {
                    datafield.Add(d.FieldName);
                    dataagg.Add((AggregateFunctionE)d.Aggregate);
                }
                foreach (ParamFields d in p.ColumnArguments)
                {
                    colfield.Add(d.FieldName);
                }
                dt = dt.PivotData(datafield, dataagg.ToList(), row.ToArray(), colfield.ToArray(), false, 0, null, null, false);
                result = new Result();
                result.Value = dt;
            }
            else if (element.Type == FunctionType.Mean)
            {
                if (element.GetSource() != null)
                {
                    System.Data.DataTable dt = getRecord(element);
                    result = new Result();
                    result.Value = dt.Computing("", element.ActionField, AggregateFunctionE.Median);
                }
            }
            else if (element.Type == FunctionType.Mode)
            {
                if (element.GetSource() != null)
                {
                    System.Data.DataTable dt = getRecord(element);
                    result = new Result();
                    result.Value = dt.Computing("", element.ActionField, AggregateFunctionE.Mode);
                }
            }
            else if (element.Type == FunctionType.Range)
            {

            }
            else if (element.Type == FunctionType.Medium)
            {
                if (element.GetSource() != null)
                {
                    System.Data.DataTable dt = getRecord(element);
                    result = new Result();
                    result.Value = dt.Computing("", element.ActionField, AggregateFunctionE.Median);
                }
            }
            else if (element.Type == FunctionType.Merge)
            {

            }
            else if (element.Type == FunctionType.Join)
            {

            }
            else if (element.Type == FunctionType.Filter)
            {
                if (element.GetSource() != null)
                {
                    System.Data.DataTable dt = (System.Data.DataTable)element.GetSource();
                    var flt = "1=1";
                    foreach (ParamFields pf in element.Arguments)
                    {
                        flt = flt + " AND " + pf.FieldName + " = " + pf.FieldValue;
                    }
                    dt.DefaultView.RowFilter = flt;
                    dt = dt.DefaultView.ToTable(true);
                    dt.DefaultView.RowFilter = "";
                    result = new Result();
                    result.Value = dt;

                }
            }
            else if (element.Type == FunctionType.Calculate)
            {
                if (element.GetSource() != null)
                {
                    System.Data.DataTable dt = (System.Data.DataTable)element.GetSource();
                    dt.Calculate(((Calculate)element).ColumnName, ((Calculate)element).formula);
                    result = new Result();
                    result.Value = dt;
                }
            }
            else if (element.Type == FunctionType.Columnduplicate)
            {
                if (element.GetSource() != null)
                {
                    System.Data.DataTable dt = (System.Data.DataTable)element.GetSource();
                    dt = dt.DuplicateColumn(((Duplicate)element).ColumnName, ((Duplicate)element).AliasColumnName);
                    result = new Result();
                    result.Value = dt;
                }
            }
            else if (element.Type == FunctionType.Dateparse)
            {
                if (element.GetSource() != null)
                {
                    System.Data.DataTable dt = (System.Data.DataTable)element.GetSource();
                    dt = dt.DateParse(((DateParse)element).ColumnName, ((DateParse)element).GetParse());
                    result = new Result();
                    result.Value = dt;
                }
            }
            else if (element.Type == FunctionType.Columnspit)
            {
                if (element.GetSource() != null)
                {
                    System.Data.DataTable dt = (System.Data.DataTable)element.GetSource();
                    dt = dt.Split(((Split)element).SplitColumn, ((Split)element).Spliter, ((Split)element).ColumnPrefix);
                    result = new Result();
                    result.Value = dt;
                }
            }
            else if (element.Type == FunctionType.Replace)
            {
                if (element.GetSource() != null)
                {
                    List<KeyValuePair<string, string>> kp = new List<KeyValuePair<string, string>>();
                    kp.Add(new KeyValuePair<string, string>(((Replace)element).FindString, ((Replace)element).ReplaceString));
                    System.Data.DataTable dt = (System.Data.DataTable)element.GetSource();
                    dt = dt.FindReplace(((Replace)element).ColumnName, kp, ((Replace)element).NewColumnName);
                    result = new Result();
                    result.Value = dt;
                }
            }
            else if (element.Type == FunctionType.ChangeCase)
            {
                if (element.GetSource() != null)
                {
                    System.Data.DataTable dt = (System.Data.DataTable)element.GetSource();
                    if ((((TextCase)element).CaseType == TextCaseType.UPPER))
                    {
                        dt = dt.UpperCase(((TextCase)element).ColumnName);
                    }
                    else if ((((TextCase)element).CaseType == TextCaseType.CAPITAL))
                    {
                        dt = dt.CapitalCase(((TextCase)element).ColumnName);
                    }
                    else if ((((TextCase)element).CaseType == TextCaseType.LOWER))
                    {
                        dt = dt.LowerCase(((TextCase)element).ColumnName);
                    }
                    else if ((((TextCase)element).CaseType == TextCaseType.TITLE))
                    {
                        dt = dt.TitleCase(((TextCase)element).ColumnName);
                    }
                    result = new Result();
                    result.Value = dt;
                }
            }
            else if (element.Type == FunctionType.Trancate)
            {
                if (element.GetSource() != null)
                {
                    System.Data.DataTable dt = (System.Data.DataTable)element.GetSource();
                    dt.Trancate(((Trancate)element).ColumnName, ((Trancate)element).TrancateIndex);
                    result = new Result();
                    result.Value = dt;
                }
            }
            else if (element.Type == FunctionType.GetValue)
            {
                if (element.GetSource() != null)
                {
                    System.Data.DataTable dt = (System.Data.DataTable)element.GetSource();
                    if (dt.Rows.Count > 0)
                    {
                        var name = ((Fun)element).Arguments[0].FieldName;
                        var s = dt.Rows[0][name.Trim()];
                        result = new Result();
                        result.Value = s;
                    }
                    else
                    {
                        result = new Result();
                        result.Value = "";
                    }
                }
            }
            else if (element.Type == FunctionType.GetValues)
            {
                if (element.GetSource() != null)
                {
                    System.Data.DataTable dt = (System.Data.DataTable)element.GetSource();
                    if (dt.Rows.Count > 0)
                    {
                        var name = ((Fun)element).Arguments[0].FieldName.Trim();
                        Type type = dt.Columns[name].DataType;
                        var selectedColumn = string.Join(",", dt.AsEnumerable().Select(s => s.Field<string>(name)).ToArray());
                        result = new Result();
                        result.Value = selectedColumn;
                    }
                    else
                    {
                        result = new Result();
                        result.Value = "";
                    }
                }
                else
                {
                    result = new Result();
                    result.Value = "";
                }

            }
            else if (element.Type == FunctionType.toString)
            {
                if (element.GetSource() != null)
                {

                }
            }
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="element"></param>
        /// <param name="evp"></param>
        public override void Visit(IterativeStatement element, EvaluationParam evp)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="evp"></param>
        public override void Visit(SelectionStatement element, EvaluationParam evp)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="evp"></param>
        public override void Visit( Syntax. Expression element, EvaluationParam evp)
        {
            


            var a = new NCalc.Expression(element.formula).Evaluate();
            result = new Result();
            result.Value = a;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="evp"></param>
        public override void Visit(SQuery element, EvaluationParam evp)
        {
            result = new Result();
            dynamic c = evp;
            if (evp.IsPropertyExist("connection") == true) {
                var d =c.connection;
                var data = new Domain.QData.DataParser(d);
               data.GetDatatable(element);                
                result.Value = data.Datatable;
            }            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="evp"></param>
        public override void Visit(Value element, EvaluationParam evp)
        {
            result = new Result();
            result.Value = element.getValue();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="evp"></param>
        public override void Visit(IFStatement element, EvaluationParam evp)
        {
            NCalc.Expression e = new NCalc.Expression(element.Text);
           
            e.EvaluateFunction += delegate (string name, FunctionArgs args)
            {
                if (name.ToLower() == "mod")
                    args.Result = (int)args.Parameters[0].Evaluate() % (int)args.Parameters[1].Evaluate();
            };
          var a  = e.Evaluate();
            result = new Result();
            result.Value = a;
            //   throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        #region Functions  & Methods
        private void filter(Fun element) {

        }

        
        #endregion

    }
}

