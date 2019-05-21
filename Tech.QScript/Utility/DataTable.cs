using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

using Microsoft.VisualBasic;


public enum AggregateFunctionE
{
    Count,
    Sum,
    First,
    Last ,
    Average,
    Max,
    Min ,
    Exists ,
    NONE,
        Distinct_Count,
    StandardDeviation,
    Variance,
    Median,
    Mode,
}
public enum ParseType
{
    _NONE,
    _DATE,
    _HOUR,
    _MIN,
    _SEC,
    _DAY,
    _MONTH,
    _QUARTER,
    _YEAR,
    _WEEK,
    _DDMMYYYY,
    _MMDDYYYY,
    _MMMDDYYYY,
    _DDMMMYYYY,
    _YEARMONTH,
    _YEARQUARTER,
}

namespace System.Data
{
    public static class DataTableExtensions
    {

        public enum GroupAggregate
        {
            _none,
            _sum,
            _avg,
            _min,
            max,
        }

        public enum ColumnAggregate
        {
            _none,
            _individual,
            _grand,
            _both,
        }

        public enum titleCase
        {
            First,
            All
        }

        private static string GetString(object o)
        {
            if (o == null)
            {
                return "NONE";
            }
            return o.ToString().Replace("00:00:00", "").ToString().Trim();
        }

        public static DataTable PivotData(this DataTable dt,string rowField, string dataField, AggregateFunctionE aggregate, params string[] columnFields)
        {
            IEnumerable<DataRow> _Source = new List<DataRow>();
            _Source = dt.Rows.Cast<DataRow>();

            string Separator = ".";
            List<string> rowList = _Source.Select(x => x[rowField].ToString()).Distinct().ToList();
            // Gets the list of columns .(dot) separated.
            var colList = _Source.Select(x => (columnFields.Select(n => x[n]).Aggregate((a, b) => a += Separator + b.ToString())).ToString()).Distinct().OrderBy(m => m);

            dt.Columns.Add(rowField);
            foreach (var colName in colList)
                dt.Columns.Add(colName);  // Cretes the result columns.//

            foreach (string rowName in rowList)
            {
                DataRow row = dt.NewRow();
                row[rowField] = rowName;
                foreach (string colName in colList)
                {
                    string strFilter = rowField + " = '" + rowName + "'";
                    string[] strColValues = colName.Split(Separator.ToCharArray(), StringSplitOptions.None);
                    for (int i = 0; i < columnFields.Length; i++)
                        strFilter += " and " + columnFields[i] + " = '" + strColValues[i] + "'";
                    row[colName] = dt.Computing(strFilter, dataField, aggregate);
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        public static DataTable PivotData(this DataTable dt, string rowField, string dataField, AggregateFunctionE aggregate, bool showSubTotal, params string[] columnFields)
        {
            IEnumerable<DataRow> _Source = new List<DataRow>();
            _Source = dt.Rows.Cast<DataRow>();
            string Separator = ".";
            List<string> rowList = _Source.Select(x => x[rowField].ToString()).Distinct().ToList();
            // Gets the list of columns .(dot) separated.
            List<string> colList = _Source.Select(x => columnFields.Aggregate((a, b) => x[a].ToString() + Separator + x[b].ToString())).Distinct().OrderBy(m => m).ToList();

            if (showSubTotal && columnFields.Length > 1)
            {
                string totalField = string.Empty;
                for (int i = 0; i < columnFields.Length - 1; i++)
                    totalField += columnFields[i] + "(Total)" + Separator;
                List<string> totalList = _Source.Select(x => totalField + x[columnFields.Last()].ToString()).Distinct().OrderBy(m => m).ToList();
                colList.InsertRange(0, totalList);
            }

            dt.Columns.Add(rowField);
            colList.ForEach(x => dt.Columns.Add(x));

            foreach (string rowName in rowList)
            {
                DataRow row = dt.NewRow();
                row[rowField] = rowName;
                foreach (string colName in colList)
                {
                    string filter = rowField + " = '" + rowName + "'";
                    string[] colValues = colName.Split(Separator.ToCharArray(), StringSplitOptions.None);
                    for (int i = 0; i < columnFields.Length; i++)
                        if (!colValues[i].Contains("(Total)"))
                            filter += " and " + columnFields[i] + " = '" + colValues[i] + "'";
                    row[colName] = dt.Computing(filter, dataField, colName.Contains("(Total)") ? AggregateFunctionE.Sum : aggregate);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
         
        private static void addRowsToTable(this DataTable _SourceTable, string[] RowFields, DataTable dt)
        {
            foreach (string s in RowFields)
            {
                //Get dataType of the column
                if (dt.Columns.Contains(s)) {
                    continue;
                }
                if (_SourceTable.Columns[s].DataType.Name.ToLower().IndexOf("datetime") >= 0 || _SourceTable.Columns[s].DataType.Name.ToLower().IndexOf("date") >= 0)
                {
                    dt.Columns.Add(s, typeof(System.DateTime));
                }
                else if (_SourceTable.Columns[s].DataType.Name.ToLower().IndexOf("int") >= 0 || _SourceTable.Columns[s].DataType.Name.ToLower().IndexOf("double") >= 0 || _SourceTable.Columns[s].DataType.Name.ToLower().IndexOf("float") >= 0 || _SourceTable.Columns[s].DataType.Name.ToLower().IndexOf("decimal") >= 0 || _SourceTable.Columns[s].DataType.Name.ToLower().IndexOf("single") >= 0)
                {
                    dt.Columns.Add(s, typeof(double));
                }
                else
                {
                    dt.Columns.Add(s);
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_SourceTable"></param>
        /// <param name="DataField"></param>
        /// <param name="Aggregate"></param>
        /// <param name="RowFields"></param>
        /// <param name="ColumnFields"></param>
        /// <param name="isFindSummary"></param>
        /// <param name="cag"></param>
        /// <param name="DyRowsOrder"></param>
        /// <param name="DynamicColumnsOrder"></param>
        /// <param name="isprintGroupHeader"></param>
        /// <returns></returns>
        public static DataTable PivotData(this DataTable _SourceTable, List<string> DataField, List<AggregateFunctionE> Aggregate, string[] RowFields, string[] ColumnFields, Boolean isFindSummary = false,
           int cag = 3, string[] DyRowsOrder = null, string[] DynamicColumnsOrder = null, Boolean isprintGroupHeader = false) //, string[] DynamicColumnsOrderAs = null
        {
            ColumnAggregate _cag;
            _cag = (ColumnAggregate)cag;
            DataTable dt = new DataTable();


            if (DataField.Count > 1)
            {  // If Aggregate column more than one then we need to create custom pivot columns
                DataTable dGroup = new DataTable();
                DataTable disTable = new DataTable();
                List<string> _discl = new List<string>();
                _discl.AddRange(RowFields);
                if (ColumnFields != null)
                {
                    _discl.AddRange(ColumnFields);
                }
                disTable = _SourceTable.DefaultView.ToTable(true, _discl.ToArray());
                _SourceTable.addRowsToTable(RowFields, dGroup);
                if (ColumnFields != null)
                {
                    _SourceTable.addRowsToTable(ColumnFields, dGroup);
                }
                dGroup.Columns.Add(new DataColumn("SummaryCategory", typeof(System.String)));
                dGroup.Columns.Add(new DataColumn("Summary", typeof(System.Double)));

                foreach (DataRow dr in disTable.Rows)
                {
                    for (int i = 0; i < DataField.Count; i++)
                    {
                        string rfilter = "";
                        DataRow rGroup;
                        rGroup = dGroup.NewRow();
                        foreach (DataColumn dc in disTable.Columns)
                        {
                            if (dr[dc.ColumnName] != null)
                            {
                                rfilter = rfilter + "AND [" + dc.ColumnName + "]" + "='" + dr[dc.ColumnName] + "' ";
                            }
                            rGroup[dc.ColumnName] = dr[dc.ColumnName];
                        }
                        if (rfilter.StartsWith("AND"))
                        {
                            rfilter = rfilter.Substring(3);
                        }
                        rGroup["SummaryCategory"] = DataField[i];
                        rGroup["Summary"] = _SourceTable.Computing(rfilter, DataField[i], Aggregate[i]);
                        dGroup.Rows.Add(rGroup);
                    }

                }
                List<string> sv = new List<string>();
                if (ColumnFields != null)
                {
                    sv = ColumnFields.ToList();
                    sv.Insert(ColumnFields.Length, "SummaryCategory");
                }
                else
                {
                    sv.Insert(0, "SummaryCategory");
                }
                ColumnFields = sv.ToArray();
                DataField.Clear();
                Aggregate.Clear();
                Aggregate.Add(AggregateFunctionE.NONE);
                DataField.Add("Summary");
                _SourceTable = dGroup;
            }

            var myList = new List<string>();
            string Separator = ".";
            for (var jj = 0; jj < RowFields.Count(); jj++)
            {
                if (DyRowsOrder != null)
                {
                    if (DyRowsOrder[jj] == "0") // Before PIVOT BIND rows
                    {
                        myList.Add(RowFields[jj]);
                    }
                }
                else {
                    myList.Add(RowFields[jj]);
                }                
            }
            var RowList = _SourceTable.DefaultView.ToTable(true, RowFields.ToArray()).AsEnumerable().ToList();
            //for (int index = RowFields.Count() - 1; index >= 0; index--)
            //    RowList = RowList.OrderBy(x => x.Field<object>(RowFields[index])).ToList();
            //Ordering Off - Month Order is change in report - by Faizan - eg JAN - DEC [Apr , Aug, Dec]
            // Gets the list of columns .(dot) separated.

            _SourceTable.addRowsToTable(myList.ToArray(), dt);
            //dt.Columns.Add(RowFields);
            //foreach (string s in RowFields)
            //{
            //    //Get dataType of the column

            //    if (_SourceTable.Columns[s].DataType.Name.ToLower().IndexOf("datetime") >= 0 || _SourceTable.Columns[s].DataType.Name.ToLower().IndexOf("date") >= 0)
            //    {
            //        dt.Columns.Add(s, typeof(System.DateTime));
            //    }
            //    else if (_SourceTable.Columns[s].DataType.Name.ToLower().IndexOf("int") >= 0 || _SourceTable.Columns[s].DataType.Name.ToLower().IndexOf("double") >= 0 || _SourceTable.Columns[s].DataType.Name.ToLower().IndexOf("float") >= 0 || _SourceTable.Columns[s].DataType.Name.ToLower().IndexOf("decimal") >= 0)
            //    {
            //        dt.Columns.Add(s, typeof(double));
            //    }
            //    else
            //    {
            //        dt.Columns.Add(s);
            //    }
            //}


            if (ColumnFields != null)
            {
                if (DynamicColumnsOrder != null)
                {
                    DataView viewFI = new DataView(_SourceTable);
                    viewFI.Sort = string.Join(",", DynamicColumnsOrder);
                    _SourceTable = viewFI.ToTable();
                }

                var ColList = (from x in _SourceTable.AsEnumerable()
                               select new
                               {
                                   Name = ColumnFields.Select(n => x.Field<object>(n))
                                       .Aggregate((a, b) => a = (GetString(a) + Separator + GetString(b)))
                               })
                                        .Distinct();
                ////.OrderBy(m => m.Name);
                // ColList = ColList.OrderBy(m => m.Name);
                ////.OrderBy(m => m.Name);
                List<string> uniqCol = new List<string>();
                for (int i = ColumnFields.Length - 1; i > 0; i--)
                {
                    foreach (var col in ColList)
                    {
                        if (col.Name != null)
                        {
                            string[] n = col.Name.ToString().Split(Separator.ToCharArray());
                            n = n.Where((val, idx) => idx != i).ToArray();
                            uniqCol.Add(string.Join(Separator, n));
                        }
                    }
                }



                List<PivotColumn> pc = new List<PivotColumn>();
                string gtotal = "";
                int cIndex = 0;
                Boolean IsTotalBind = true;
                var pcol = "";
                var invCol = "";
                if (uniqCol.Count > 0)
                {
                    foreach (var col in ColList)
                    {
                        IsTotalBind = true;
                        if (col.Name != null)
                        {
                            pc.Add(new PivotColumn() { Name = col.Name.ToString() });

                            if (gtotal == "")
                                gtotal = "[" + col.Name.ToString() + "]";
                            else
                                gtotal = gtotal + "+" + "[" + col.Name + "]";

                            if (col.Name.ToString().IndexOf(uniqCol[cIndex]) >= 0)
                            {
                                if (pcol != "")
                                {
                                    if (pcol.ToString().ToLower() != uniqCol[cIndex].ToString().ToLower())
                                    {
                                        List<string> results = uniqCol.FindAll(s => s.Equals(pcol.ToString()));

                                        if (results.Count == 1)
                                        {
                                            if (isprintGroupHeader == false)
                                            {
                                                IsTotalBind = false;
                                                invCol = "";
                                                invCol = "[" + col.Name.ToString() + "]";
                                            }
                                        }
                                        if (IsTotalBind)
                                        {
                                            //Add group total
                                            if (_cag == ColumnAggregate._individual || _cag == ColumnAggregate._both)
                                            {
                                                if (!dt.Columns.Contains(pcol.ToString() + "." + "Total"))
                                                {
                                                    dt.Columns.Add(pcol.ToString() + "." + "Total", typeof(double), invCol);
                                                }

                                            }
                                            invCol = "[" + col.Name.ToString() + "]";
                                            //pcol = uniqCol[cIndex];
                                        }
                                    }
                                    else
                                    {
                                        if (invCol == "")
                                            invCol = "[" + col.Name.ToString() + "]";
                                        else
                                            invCol = invCol + "+" + "[" + col.Name + "]";
                                    }
                                }
                                else
                                {
                                    //pcol = uniqCol[cIndex];
                                    if (invCol == "")
                                        invCol = "[" + col.Name.ToString() + "]";
                                    else
                                        invCol = invCol + "+" + "[" + col.Name + "]";
                                }
                            }
                            pcol = uniqCol[cIndex];
                            dt.Columns.Add(col.Name.ToString(), typeof(double));  // Cretes the result columns.//
                            cIndex = cIndex + 1;

                        }
                    }
                    if (_cag == ColumnAggregate._individual || _cag == ColumnAggregate._both)
                    {
                        List<string> results = uniqCol.FindAll(s => s.Equals(pcol.ToString()));

                        if (results.Count == 1)
                        {
                            IsTotalBind = false;
                        }
                        if (IsTotalBind)
                        {
                            if (invCol != "")
                            {
                                if (!dt.Columns.Contains(pcol.ToString() + "." + "Total"))
                                {
                                    dt.Columns.Add(pcol.ToString() + "." + "Total", typeof(double), invCol);
                                }
                                // dt.Columns.Add(pcol.ToString() + "." + "Total", typeof(double), invCol);
                                pcol = "";
                                invCol = "";
                            }
                        }
                    }

                    if (_cag == ColumnAggregate._both || _cag == ColumnAggregate._grand)
                        dt.Columns.Add("GrandTotal", typeof(double), gtotal);
                }
                else
                {
                    foreach (var col in ColList)
                    {
                        if (col.Name != null)
                        {
                            pc.Add(new PivotColumn() { Name = col.Name.ToString() });
                            dt.Columns.Add(col.Name.ToString(), typeof(double));  // Cretes the result columns.//
                            if (gtotal == "")
                                gtotal = "[" + col.Name.ToString() + "]";
                            else
                                gtotal = gtotal + "+" + "[" + col.Name + "]";
                        }
                    }
                    if (_cag == ColumnAggregate._both || _cag == ColumnAggregate._grand)
                        dt.Columns.Add("GrandTotal", typeof(double), gtotal);
                }


                var myList2 = new List<string>();
                for (var jj = 0; jj < RowFields.Count(); jj++)
                {
                    if (DyRowsOrder != null)
                    {
                        if (DyRowsOrder[jj] == "1") // AFTER PIVOT BIND rows
                        {
                            myList2.Add(RowFields[jj]);
                        }
                    }
                    else {
                        myList2.Add(RowFields[jj]);
                    }                    
                }
                _SourceTable.addRowsToTable(myList2.ToArray(), dt);


                foreach (var RowName in RowList)
                {
                    DataRow row = dt.NewRow();
                    string strFilter = string.Empty;

                    foreach (string Field in RowFields)
                    {
                        row[Field] = RowName[Field];
                        //strFilter += " and " + Field + " = \"" + RowName[Field].ToString() + "\"";
                        strFilter += string.Format(" and " + Field + " = '{0}'", RowName[Field].ToString().Replace("'", "''"));
                    }
                    strFilter = strFilter.Substring(5);
                    double total = 0;

                    foreach (var col in pc)
                    {

                        string filter = strFilter;
                        if (col.Name != null)
                        {
                            string[] strColValues = col.Name.ToString().Split(Separator.ToCharArray(), StringSplitOptions.None);
                            for (int i = 0; i < ColumnFields.Length; i++)
                                if (strColValues[i] != null)
                                {

                                    //filter += " and " + ColumnFields[i] + "= \"" + strColValues[i] + "\"";
                                    filter += string.Format(" and " + ColumnFields[i] + " = '{0}'", strColValues[i].ToString().Replace("'", "''"));
                                }
                            object val = _SourceTable.Computing(filter, DataField[0], Aggregate[0]);
                            if (val == null)
                            {
                                val = 0;
                            }
                            row[col.Name.ToString()] = val;
                            total = total + Convert.ToDouble(val);
                        }
                    }

                    //if (cag == ColumnAggregate._both || cag == ColumnAggregate._grand)
                    //    row["GrandTotal"] = total;

                    dt.Rows.Add(row);
                }
            }
            else
            {
                if (DataField.Count != 0)
                {
                    List<string> df;
                    df = DataField;
                    for (int i = 0; i < DataField.Count; i++)
                    {
                        dt.Columns.Add(df[i], typeof(double));
                    }

                    foreach (var RowName in RowList)
                    {
                        DataRow row = dt.NewRow();
                        string strFilter = string.Empty;

                        foreach (string Field in RowFields)
                        {
                            row[Field] = RowName[Field];

                            if ((row.IsNull(Field)))
                            {
                                strFilter += " and " + Field + " = NULL";
                            }
                            else
                            {
                                //strFilter += " and " + Field + " = `" + RowName[Field].ToString()+ "`";
                                strFilter += string.Format(" and " + Field + " = '{0}'", RowName[Field].ToString().Replace("'", "''"));
                            }


                            //  strFilter += " and " + Field + " = '" + RowName[Field].ToString() + "'";
                        }
                        strFilter = strFilter.Substring(5);
                        for (int i = 0; i < df.Count; i++)
                        {
                            row[df[i]] = _SourceTable.Computing (strFilter, df[i].ToString(), Aggregate[i]);
                        }
                        dt.Rows.Add(row);
                    }

                    if (isFindSummary)
                    {
                        for (int index = 0; index < RowFields.Count() - 1; index++)
                        {
                            DataTable _temp = new DataTable();
                            _temp = dt.Clone();

                            List<string> rFields = new List<string>();
                            DataTable _dt = new DataTable();
                            List<string> _disRow = new List<string>();


                            for (int rix = 0; rix < RowFields.Count() - 1; rix++)
                            {
                                if (index >= rix)
                                {
                                    {
                                        rFields.Add(RowFields[rix]);

                                    }
                                }
                            }


                            foreach (string i in rFields)
                            {
                                _disRow.Add(i);

                            }
                            _dt = _SourceTable.DefaultView.ToTable(true, _disRow.ToArray());



                            foreach (DataRow _dr in _dt.Rows)
                            {
                                DataRow dr = _temp.NewRow();
                                string strFilter = "";
                                foreach (string fn in _disRow)
                                {
                                    dr[fn] = _dr[fn];
                                    //strFilter += " and " + fn + " = `" + _dr[fn].ToString() + "`";
                                    strFilter += string.Format(" and " + fn + " = '{0}'", _dr[fn].ToString().Replace("'", "''"));
                                }
                                strFilter = strFilter.Substring(5);
                                for (int i = 0; i < df.Count; i++)
                                {
                                    dr[df[i]] = _SourceTable.Computing(strFilter, df[i], Aggregate[i], dt);
                                }
                                _temp.Rows.Add(dr);
                            }
                            dt.Merge(_temp);
                        }
                    }
                }
                else
                {
                    dt = _SourceTable.DefaultView.ToTable(true, RowFields);
                }




            }


            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.Replace(" ", "_");
                dt.AcceptChanges();
                if (dt.Columns[i].Expression != "")
                {
                    dt.Columns[i].Expression = dt.Columns[i].Expression.Replace(" ", "_");
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        /// <summary>
        /// Retrives the data for matching RowField value and ColumnFields values with Aggregate function applied on them.
        /// </summary>
        /// <param name="Filter">DataTable Filter condition as a string</param>
        /// <param name="DataField">The column name which needs to spread out in Data Part of the Pivoted table</param>
        /// <param name="Aggregate">Enumeration to determine which function to apply to aggregate the data</param>
        /// <returns></returns>
        public static object Computing(this DataTable dt, string Filter, string DataField, AggregateFunctionE Aggregate, DataTable dtAlias = null)
        {
            try
            {
                DataRow[] FilteredRows;
                //    var rows = dt.AsEnumerable().ToList().AsQueryable().Where(Filter);
                if (dtAlias != null)
                {
                     FilteredRows = dtAlias.Select(Filter);
                }
                else {
                    FilteredRows = dt.Select(Filter);
                }
               
                object[] objList = FilteredRows.Select(x => x.Field<object>(DataField)).ToArray();
                switch (Aggregate)
                {
                    case AggregateFunctionE.Average:
                        return GetAverage(objList);
                    case AggregateFunctionE.Count:
                        return objList.Count();
                    case AggregateFunctionE.Exists:
                        return (objList.Count() == 0) ? "False" : "True";
                    case AggregateFunctionE.First:
                        return GetFirst(objList);
                    case AggregateFunctionE.Last:
                        return GetLast(objList);
                    case AggregateFunctionE.Max:
                        return GetMax(objList);
                    case AggregateFunctionE.Min:
                        return GetMin(objList);
                    case AggregateFunctionE.Sum:
                        return GetSum(objList);
                    case AggregateFunctionE.Distinct_Count:
                        return objList.Distinct().Count();
                    case AggregateFunctionE.StandardDeviation:
                        var fArray = (from o in objList select Convert.ToDouble(o)).ToList();
                        //  List<double> fArray = objList.ToList().Select(s => (double)s).ToList();
                        if (fArray.Count > 0)
                        {
                            double average = fArray.Average();
                            double sumOfSquaresOfDifferences = fArray.Select(val => (val - average) * (val - average)).Sum();
                            double sd = Math.Sqrt(sumOfSquaresOfDifferences / fArray.Count);
                            return sd;
                        }
                        else
                        {
                            return 0;
                        }
                    case AggregateFunctionE.Variance:
                        var fVari = (from o in objList select Convert.ToDouble(o)).ToList();
                        if (fVari.Count > 0)
                        {
                            double var_Avg = fVari.Average();
                            double sumofSql = fVari.Select(val => (val - var_Avg) * (val - var_Avg)).Sum();
                            double var = (sumofSql / fVari.Count);
                            return var;
                        }
                        else
                        {
                            return 0;
                        }
                    case AggregateFunctionE.Median:
                        double[] a;
                        a = (from o in objList select Convert.ToDouble(o)).ToArray();
                        //  a = objList.ToList().Select(s => (double)s).ToArray() ;
                        if (a.Length > 0)
                        {
                            Array.Sort(a);
                            return a[Convert.ToInt32(a.Length / 2)];
                        }
                        else
                        {
                            return 0;
                        }
                    //var ys = xa.OrderBy(x => x).ToList();
                    //double mid = (ys.Count - 1) / 2.0;
                    //return (ys[(int)(mid)] + ys[(int)(mid + 0.5)]) / 2;   

                    case AggregateFunctionE.Mode:
                        var fmode = (from o in objList select Convert.ToDouble(o)).ToList();
                        if (fmode.Count > 0)
                        {
                            double high = fmode.ToList().OrderByDescending(n => n).First();
                            double mode = fmode.GroupBy(i => i)  //Grouping same items
                            .OrderByDescending(g => g.Count()) //now getting frequency of a value
                            .Select(g => g.Key) //selecting key of the group
                            .FirstOrDefault();   //Finally, taking the most frequent value
                            return mode;
                        }
                        else
                        {
                            return 0;
                        }
                    case AggregateFunctionE.NONE:
                        return (objList.Count() == 0) ? 0 : objList[0];
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static object GetAverage(this object[] objList)
        {
            return objList.Count() == 0 ? null : (object)(Convert.ToDouble(GetSum(objList)) / objList.Count());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static object GetSum(this object[] objList)
        {
            return objList.Count() == 0 ? null : (object)(objList.Aggregate(new double(), (x, y) => x += (Convert.ToDouble(y == "" ? 0 : y))));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static object GetFirst(this object[] objList)
        {
            return (objList.Count() == 0) ? null : objList.First();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static object GetLast(this object[] objList)
        {
            return (objList.Count() == 0) ? null : objList.Last();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static object GetMax(this object[] objList)
        {
            return (objList.Count() == 0) ? null : objList.Max();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static object GetMin(this object[] objList)
        {
            return (objList.Count() == 0) ? null : objList.Min();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtResult"></param>
        /// <param name="ColumnName"></param>
        /// <param name="DuplicateColumnName"></param>
        /// <returns></returns>
        public static DataTable DuplicateColumn(this DataTable dtResult, string ColumnName, string DuplicateColumnName)
        {
            DataColumn dc, newdc;
            dc = dtResult.Columns[ColumnName];
            newdc = new DataColumn();
            newdc.ColumnName = DuplicateColumnName;
            newdc.DataType = dc.DataType;
            dtResult.Columns.Add(newdc);
            foreach (DataRow dr in dtResult.Rows)
            {
                dr[DuplicateColumnName] = dr[ColumnName];
            }
            return dtResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtResult"></param>
        /// <param name="ColumnName"></param>
        /// <param name="Splitter"></param>
        /// <param name="ColPrefix"></param>
        /// <returns></returns>
        public static DataTable Split(this DataTable dtResult, string ColumnName, string Splitter, string ColPrefix)
        {
            DataTable newdtResult = new DataTable();
            newdtResult.Columns.Add(new DataColumn("auto", typeof(Int64)));
            newdtResult.Columns["auto"].AutoIncrement = true;
            newdtResult.Columns["auto"].AutoIncrementSeed = 1;
            newdtResult.Columns["auto"].AutoIncrementStep = 1;
            DataTableReader dtr = new DataTableReader(dtResult);
            newdtResult.Load(dtr);
            dtResult.Clear();
            dtResult.Columns.Clear();
            char[] myChar = Splitter.ToCharArray();

            var result = from s in newdtResult.AsEnumerable()
                         select new Col
                         {
                             ColsName = s[ColumnName].ToString().Split(myChar).ToArray(),
                             ColKey = Convert.ToInt64(s["auto"]),
                             MaxLength = s[ColumnName].ToString().Split(myChar).ToArray().Length,
                         };

            var maxObject = result.OrderByDescending(item => item.MaxLength).First();


            List<Col> toUpCol = new List<Col>();
            toUpCol = result.ToList();

            var joinTable = from t1 in toUpCol
                            join t2 in newdtResult.AsEnumerable()
                                on t1.ColKey equals t2["auto"]
                            select new { t2, t1 };

            foreach (DataColumn col in newdtResult.Columns)
                dtResult.Columns.Add(col.ColumnName, col.DataType);

            for (int i = 0; i < maxObject.MaxLength; i++)
            {
                dtResult.Columns.Add(ColPrefix + "_" + (i + 1), typeof(string));
            }


            foreach (var row in joinTable)
            {
                var newRow = dtResult.NewRow();
                var obj = row.t2.ItemArray.ToArray();
                var obj1 = row.t1.ColsName.ToArray();
                var z = new object[obj.Length + obj1.Length];
                obj.CopyTo(z, 0);
                obj1.CopyTo(z, obj.Length);
                newRow.ItemArray = z;
                try
                {
                    dtResult.Rows.Add(newRow);
                }
                catch { }
            }
            dtResult.Columns.Remove("auto");
            return dtResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtResult"></param>
        /// <param name="ColumnName"></param>
        /// <param name="pfindReplace"></param>
        /// <param name="NewColumnName"></param>
        /// <returns></returns>
        public static DataTable FindReplace(this DataTable dtResult, string ColumnName, List<KeyValuePair<string, string>> pfindReplace, string NewColumnName)
        {
            if (dtResult.Rows.Count == 0) return dtResult;



            if (NewColumnName =="")
            {
                foreach (var m in pfindReplace)
                {
                    string rp = "[{0}] like '%{1}%'";
                    dtResult.Select(string.Format(rp, ColumnName, m.Key)).ToList().ForEach(x => x.SetField<string>(ColumnName, x.Field<string>(ColumnName).Replace(m.Key, m.Value)));
                }               
            }
            else
            {
                DataSet ds = new DataSet();
                DataTable newdtResult = new DataTable();

                newdtResult.Columns.Add(new DataColumn("auto", typeof(Int64)));
                newdtResult.Columns["auto"].AutoIncrement = true;
                newdtResult.Columns["auto"].AutoIncrementSeed = 1;
                newdtResult.Columns["auto"].AutoIncrementStep = 1;

                DataTableReader dtr = new DataTableReader(dtResult);
                newdtResult.Load(dtr);
                dtResult.Clear();
                dtResult.Columns.Clear();

                var result = from s in newdtResult.AsEnumerable()
                             select new Col
                             {
                                 ColName = s[ColumnName].ToString().toReplace(pfindReplace),
                                 ColKey = Convert.ToInt64(s["auto"])
                             };

                List<Col> toUpCol = new List<Col>();
                toUpCol = result.ToList();

                //  var dtFinal = new DataTable();
                var joinTable = from t1 in newdtResult.AsEnumerable()
                                join t2 in toUpCol.AsEnumerable()
                                    on Convert.ToInt64(t1["auto"]) equals t2.ColKey
                                select new { t1, t2 };

                foreach (DataColumn col in newdtResult.Columns)
                    dtResult.Columns.Add(col.ColumnName, col.DataType);
                if (!dtResult.Columns.Contains(NewColumnName))
                {
                    dtResult.Columns.Add(NewColumnName, typeof(string));
                }

                foreach (var row in joinTable)
                {
                    var newRow = dtResult.NewRow();
                    var obj = row.t1.ItemArray.ToArray();
                    var obj1 = row.t2.ColName;
                    Array.Resize(ref obj, obj.Length + 1);
                    obj[obj.Length - 1] = obj1;
                    newRow.ItemArray = obj;
                    dtResult.Rows.Add(newRow);
                }
                dtResult.Columns.Remove("auto");
            }

            return dtResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtResult"></param>
        /// <param name="ColumnName"></param>
        /// <param name="DateParseType"></param>
        /// <returns></returns>
        public static DataTable DateParse(this DataTable dtResult, string ColumnName, ParseType DateParseType)
        {

            DataTable newdtResult = new DataTable();
            List<Col> toUpCol = new List<Col>();
            newdtResult.Columns.Add(new DataColumn("auto", typeof(Int64)));
            newdtResult.Columns["auto"].AutoIncrement = true;
            newdtResult.Columns["auto"].AutoIncrementSeed = 1;
            newdtResult.Columns["auto"].AutoIncrementStep = 1;

            DataTableReader dtr = new DataTableReader(dtResult);
            newdtResult.Load(dtr);
            dtResult.Clear();
            dtResult.Columns.Clear();
            // var result=new object();
            if (DateParseType == ParseType._DATE)
            {
                var result = from s in newdtResult.AsEnumerable()
                             select new Col
                             {
                                 ColName = s[ColumnName] is DBNull ? "" : Convert.ToDateTime(s[ColumnName]).Date.ToString(),
                                 ColKey = Convert.ToInt64(s["auto"])
                             };
                toUpCol = result.ToList();
            }
            else if (DateParseType == ParseType._DAY)
            {
                var result = from s in newdtResult.AsEnumerable()
                             select new Col
                             {
                                 ColName = s[ColumnName] is DBNull ? "" : Convert.ToDateTime(s[ColumnName]).Day.ToString(),
                                 ColKey = Convert.ToInt64(s["auto"])
                             };
                toUpCol = result.ToList();
            }
            else if (DateParseType == ParseType._YEAR)
            {
                var result = from s in newdtResult.AsEnumerable()
                             select new Col
                             {
                                 ColName = s[ColumnName] is DBNull ? "" : Convert.ToDateTime(s[ColumnName]).Year.ToString(),
                                 ColKey = Convert.ToInt64(s["auto"])
                             };
                toUpCol = result.ToList();
            }
            else if (DateParseType == ParseType._MONTH)
            {
                var result = from s in newdtResult.AsEnumerable()
                             select new Col
                             {
                                 ColName = s[ColumnName] is DBNull ? "" : Convert.ToDateTime(s[ColumnName]).Month.ToString(),
                                 ColKey = Convert.ToInt64(s["auto"])
                             };
                toUpCol = result.ToList();
            }
            else if (DateParseType == ParseType._WEEK)
            {
                CultureInfo cul = CultureInfo.CurrentCulture;
                var result = from s in newdtResult.AsEnumerable()
                             select new Col
                             {
                                 ColName = s[ColumnName] is DBNull ? "" : cul.Calendar.GetWeekOfYear(Convert.ToDateTime(s[ColumnName]), CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString(),
                                 ColKey = Convert.ToInt64(s["auto"])
                             };
                toUpCol = result.ToList();
            }
            else if (DateParseType == ParseType._QUARTER)
            {
                CultureInfo cul = CultureInfo.CurrentCulture;
                var result = from s in newdtResult.AsEnumerable()
                             select new Col
                             {
                                 ColName = s[ColumnName] is DBNull ? "" : Math.Ceiling(Convert.ToDateTime(s[ColumnName]).Month / 3m).ToString(),
                                 ColKey = Convert.ToInt64(s["auto"])
                             };
                toUpCol = result.ToList();
            }
            else if (DateParseType == ParseType._MMMDDYYYY)
            {
                CultureInfo cul = CultureInfo.CurrentCulture;
                var result = from s in newdtResult.AsEnumerable()
                             select new Col
                             {
                                 ColName = s[ColumnName] is DBNull ? "" : Convert.ToDateTime(s[ColumnName]).ToString("MMMM dd, yyyy"),
                                 ColKey = Convert.ToInt64(s["auto"])
                             };
                toUpCol = result.ToList();
            }
            else if (DateParseType == ParseType._DDMMYYYY)
            {
                CultureInfo cul = CultureInfo.CurrentCulture;
                var result = from s in newdtResult.AsEnumerable()
                             select new Col
                             {
                                 ColName = s[ColumnName] is DBNull ? "" : Convert.ToDateTime(s[ColumnName]).ToString("dd/MM/yyyy"),
                                 ColKey = Convert.ToInt64(s["auto"])
                             };
                toUpCol = result.ToList();
            }
            else if (DateParseType == ParseType._MMDDYYYY)
            {
                CultureInfo cul = CultureInfo.CurrentCulture;
                var result = from s in newdtResult.AsEnumerable()
                             select new Col
                             {
                                 ColName = s[ColumnName] is DBNull ? "" : Convert.ToDateTime(s[ColumnName]).ToString("MM/dd/yyyy"),
                                 ColKey = Convert.ToInt64(s["auto"])
                             };
                toUpCol = result.ToList();
            }
            else if (DateParseType == ParseType._DDMMMYYYY)
            {
                CultureInfo cul = CultureInfo.CurrentCulture;
                var result = from s in newdtResult.AsEnumerable()
                             select new Col
                             {
                                 ColName = s[ColumnName] is DBNull ? "" : Convert.ToDateTime(s[ColumnName]).ToString("dd MMMM, yyyy"),
                                 ColKey = Convert.ToInt64(s["auto"])
                             };
                toUpCol = result.ToList();
            }
            else if (DateParseType == ParseType._YEARMONTH)
            {
                CultureInfo cul = CultureInfo.CurrentCulture;
                var result = from s in newdtResult.AsEnumerable()
                             select new Col
                             {
                                 ColName = s[ColumnName] is DBNull ? "" : Convert.ToDateTime(s[ColumnName]).Year + "-" + Convert.ToDateTime(s[ColumnName]).Month,
                                 ColKey = Convert.ToInt64(s["auto"])
                             };
                toUpCol = result.ToList();
            }
            else if (DateParseType == ParseType._YEARQUARTER)
            {
                CultureInfo cul = CultureInfo.CurrentCulture;
                var result = from s in newdtResult.AsEnumerable()
                             select new Col
                             {
                                 ColName = s[ColumnName] is DBNull ? "" : Convert.ToDateTime(s[ColumnName]).Year + "-" + Math.Ceiling(Convert.ToDateTime(s[ColumnName]).Month / 3m).ToString(),
                                 ColKey = Convert.ToInt64(s["auto"])
                             };
                toUpCol = result.ToList();
            }
            else if (DateParseType == ParseType._HOUR)
            {
                CultureInfo cul = CultureInfo.CurrentCulture;
                var result = from s in newdtResult.AsEnumerable()
                             select new Col
                             {
                                 ColName = s[ColumnName] is DBNull ? "" : Convert.ToDateTime(s[ColumnName]).Hour.ToString(),
                                 ColKey = Convert.ToInt64(s["auto"])
                             };
                toUpCol = result.ToList();
            }
            else if (DateParseType == ParseType._MIN)
            {
                CultureInfo cul = CultureInfo.CurrentCulture;
                var result = from s in newdtResult.AsEnumerable()
                             select new Col
                             {
                                 ColName = s[ColumnName] is DBNull ? "" : Convert.ToDateTime(s[ColumnName]).Minute.ToString(),
                                 ColKey = Convert.ToInt64(s["auto"])
                             };
                toUpCol = result.ToList();
            }
            else if (DateParseType == ParseType._SEC)
            {
                CultureInfo cul = CultureInfo.CurrentCulture;
                var result = from s in newdtResult.AsEnumerable()
                             select new Col
                             {
                                 ColName = s[ColumnName] is DBNull ? "" : Convert.ToDateTime(s[ColumnName]).Second.ToString(),
                                 ColKey = Convert.ToInt64(s["auto"])
                             };
                toUpCol = result.ToList();
            }

            newdtResult.Columns.Remove(ColumnName);

            var joinTable = from t1 in toUpCol
                            join t2 in newdtResult.AsEnumerable()
                                on t1.ColKey equals t2["auto"]
                            select new { t2, t1 };

            foreach (DataColumn col in newdtResult.Columns)
                dtResult.Columns.Add(col.ColumnName, col.DataType);
            dtResult.Columns.Add(ColumnName, typeof(string));
            foreach (var row in joinTable)
            {
                var newRow = dtResult.NewRow();
                var obj = row.t2.ItemArray.ToArray();
                var obj1 = row.t1.ColName;
                Array.Resize(ref obj, obj.Length + 1);
                obj[obj.Length - 1] = obj1;
                newRow.ItemArray = obj;
                dtResult.Rows.Add(newRow);
            }
            dtResult.Columns.Remove("auto");
            return dtResult;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtResult"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public static DataTable UpperCase(this DataTable dtResult, string ColumnName)
        {
            DataTable newdtResult = new DataTable();
            newdtResult.Columns.Add(new DataColumn("auto", typeof(Int64)));
            newdtResult.Columns["auto"].AutoIncrement = true;
            newdtResult.Columns["auto"].AutoIncrementSeed = 1;
            newdtResult.Columns["auto"].AutoIncrementStep = 1;

            DataTableReader dtr = new DataTableReader(dtResult);
            newdtResult.Load(dtr);
            dtResult.Clear();
            dtResult.Columns.Clear();

            var result = from s in newdtResult.AsEnumerable()
                         select new Col
                         {
                             ColName = s[ColumnName].ToString().ToUpper(),
                             ColKey = Convert.ToInt64(s["auto"])
                         };
            List<Col> toUpCol = new List<Col>();
            toUpCol = result.ToList();
            newdtResult.Columns.Remove(ColumnName);

            var joinTable = from t1 in toUpCol
                            join t2 in newdtResult.AsEnumerable()
                                on t1.ColKey equals t2["auto"]
                            select new { t2, t1 };

            foreach (DataColumn col in newdtResult.Columns)
                dtResult.Columns.Add(col.ColumnName, col.DataType);
            dtResult.Columns.Add(ColumnName, typeof(string));
            foreach (var row in joinTable)
            {
                var newRow = dtResult.NewRow();
                var obj = row.t2.ItemArray.ToArray();
                var obj1 = row.t1.ColName;
                Array.Resize(ref obj, obj.Length + 1);
                obj[obj.Length - 1] = obj1;
                newRow.ItemArray = obj;
                dtResult.Rows.Add(newRow);
            }
            dtResult.Columns.Remove("auto");
            return dtResult;
        }
        public static DataTable Calculate(this DataTable dtResult, string columnName, string formula) {
            dtResult.Columns.Add(new DataColumn(columnName, typeof(string)));
            dtResult.Columns[columnName].Expression = formula;
            return dtResult;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtResult"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public static DataTable LowerCase(this DataTable dtResult, string ColumnName)
        {
            DataTable newdtResult = new DataTable();
            newdtResult.Columns.Add(new DataColumn("auto", typeof(Int64)));
            newdtResult.Columns["auto"].AutoIncrement = true;
            newdtResult.Columns["auto"].AutoIncrementSeed = 1;
            newdtResult.Columns["auto"].AutoIncrementStep = 1;
            DataTableReader dtr = new DataTableReader(dtResult);
            newdtResult.Load(dtr);
            dtResult.Clear();
            dtResult.Columns.Clear();
            var result = from s in newdtResult.AsEnumerable()
                         select new Col
                         {
                             ColName = s[ColumnName].ToString().ToLower(),
                             ColKey = Convert.ToInt64(s["auto"])
                         };
            List<Col> toUpCol = new List<Col>();
            toUpCol = result.ToList();
            newdtResult.Columns.Remove(ColumnName);
            var joinTable = from t1 in toUpCol
                            join t2 in newdtResult.AsEnumerable()
                                on t1.ColKey equals t2["auto"]
                            select new { t2, t1 };

            foreach (DataColumn col in newdtResult.Columns)
                dtResult.Columns.Add(col.ColumnName, col.DataType);
            dtResult.Columns.Add(ColumnName, typeof(string));
            foreach (var row in joinTable)
            {
                var newRow = dtResult.NewRow();
                var obj = row.t2.ItemArray.ToArray();
                var obj1 = row.t1.ColName;
                Array.Resize(ref obj, obj.Length + 1);
                obj[obj.Length - 1] = obj1;
                newRow.ItemArray = obj;
                dtResult.Rows.Add(newRow);
            }
            dtResult.Columns.Remove("auto");
            return dtResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtResult"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public static DataTable TitleCase(this DataTable dtResult, string ColumnName)
        {
            DataTable newdtResult = new DataTable();
            newdtResult.Columns.Add(new DataColumn("auto", typeof(Int64)));
            newdtResult.Columns["auto"].AutoIncrement = true;
            newdtResult.Columns["auto"].AutoIncrementSeed = 1;
            newdtResult.Columns["auto"].AutoIncrementStep = 1;
            DataTableReader dtr = new DataTableReader(dtResult);
            newdtResult.Load(dtr);
            dtResult.Clear();
            dtResult.Columns.Clear();
            var result = from s in newdtResult.AsEnumerable()
                         select new Col
                         {
                             ColName = s[ColumnName].ToString().toTitleCase(titleCase.All),
                             ColKey = Convert.ToInt64(s["auto"])
                         };
            List<Col> toUpCol = new List<Col>();
            toUpCol = result.ToList();
            newdtResult.Columns.Remove(ColumnName);
            var joinTable = from t1 in toUpCol
                            join t2 in newdtResult.AsEnumerable()
                                on t1.ColKey equals t2["auto"]
                            select new { t2, t1 };

            foreach (DataColumn col in newdtResult.Columns)
                dtResult.Columns.Add(col.ColumnName, col.DataType);
            dtResult.Columns.Add(ColumnName, typeof(string));
            foreach (var row in joinTable)
            {
                var newRow = dtResult.NewRow();
                var obj = row.t2.ItemArray.ToArray();
                var obj1 = row.t1.ColName;
                Array.Resize(ref obj, obj.Length + 1);
                obj[obj.Length - 1] = obj1;
                newRow.ItemArray = obj;
                try
                {
                    dtResult.Rows.Add(newRow);
                }
                catch { }

            }
            dtResult.Columns.Remove("auto");
            return dtResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtResult"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public static DataTable CapitalCase(this DataTable dtResult, string ColumnName)
        {
            DataTable newdtResult = new DataTable();
            newdtResult.Columns.Add(new DataColumn("auto", typeof(Int64)));
            newdtResult.Columns["auto"].AutoIncrement = true;
            newdtResult.Columns["auto"].AutoIncrementSeed = 1;
            newdtResult.Columns["auto"].AutoIncrementStep = 1;
            DataTableReader dtr = new DataTableReader(dtResult);
            newdtResult.Load(dtr);
            dtResult.Clear();
            dtResult.Columns.Clear();
            var result = from s in newdtResult.AsEnumerable()
                         select new Col
                         {
                             ColName = s[ColumnName].ToString().toTitleCase(titleCase.First),
                             ColKey = Convert.ToInt64(s["auto"])
                         };
            List<Col> toUpCol = new List<Col>();
            toUpCol = result.ToList();
            newdtResult.Columns.Remove(ColumnName);
            var joinTable = from t1 in toUpCol
                            join t2 in newdtResult.AsEnumerable()
                                on t1.ColKey equals t2["auto"]
                            select new { t2, t1 };

            foreach (DataColumn col in newdtResult.Columns)
                dtResult.Columns.Add(col.ColumnName, col.DataType);
            dtResult.Columns.Add(ColumnName, typeof(string));
            foreach (var row in joinTable)
            {
                var newRow = dtResult.NewRow();
                var obj = row.t2.ItemArray.ToArray();
                var obj1 = row.t1.ColName;
                Array.Resize(ref obj, obj.Length + 1);
                obj[obj.Length - 1] = obj1;
                newRow.ItemArray = obj;
                dtResult.Rows.Add(newRow);
            }
            dtResult.Columns.Remove("auto");
            return dtResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtResult"></param>
        /// <param name="ColumnName"></param>
        /// <param name="TrancateIndex"></param>
        /// <returns></returns>
        public static DataTable Trancate(this DataTable dtResult, string ColumnName, int TrancateIndex)
        {
            DataTable newdtResult = new DataTable();
            newdtResult.Columns.Add(new DataColumn("auto", typeof(Int64)));
            newdtResult.Columns["auto"].AutoIncrement = true;
            newdtResult.Columns["auto"].AutoIncrementSeed = 1;
            newdtResult.Columns["auto"].AutoIncrementStep = 1;
            DataTableReader dtr = new DataTableReader(dtResult);
            newdtResult.Load(dtr);
            dtResult.Clear();
            dtResult.Columns.Clear();
            var result = from s in newdtResult.AsEnumerable()
                         select new Col
                         {
                             ColName = s[ColumnName].ToString().Substring(0, s[ColumnName].ToString().Length < TrancateIndex ? s[ColumnName].ToString().Length : TrancateIndex),
                             ColKey = Convert.ToInt64(s["auto"])
                         };
            List<Col> toUpCol = new List<Col>();
            toUpCol = result.ToList();
            newdtResult.Columns.Remove(ColumnName);
            var joinTable = from t1 in toUpCol
                            join t2 in newdtResult.AsEnumerable()
                                on t1.ColKey equals t2["auto"]
                            select new { t2, t1 };

            foreach (DataColumn col in newdtResult.Columns)
                dtResult.Columns.Add(col.ColumnName, col.DataType);
            dtResult.Columns.Add(ColumnName, typeof(string));
            foreach (var row in joinTable)
            {
                var newRow = dtResult.NewRow();
                var obj = row.t2.ItemArray.ToArray();
                var obj1 = row.t1.ColName;
                Array.Resize(ref obj, obj.Length + 1);
                obj[obj.Length - 1] = obj1;
                newRow.ItemArray = obj;
                     dtResult.Rows.Add(newRow);
            }
            dtResult.Columns.Remove("auto");
            return dtResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="tcase"></param>
        /// <returns></returns>
        public static string toTitleCase(this string str, titleCase tcase = titleCase.First)
        {
            CultureInfo ci = new CultureInfo("en-US");

            str = str.ToLower();
            switch (tcase)
            {
                case titleCase.First:
                    var strArray = str.Split(' ');
                    if (strArray.Length > 1)
                    {
                        strArray[0] = ci.TextInfo.ToTitleCase(strArray[0]);
                        return string.Join(" ", strArray);
                    }
                    break;
                case titleCase.All:
                    return ci.TextInfo.ToTitleCase(str);
                default:
                    break;
            }
            return ci.TextInfo.ToTitleCase(str);
        }
               /// <summary>
               /// 
               /// </summary>
               /// <param name="Jsontable"></param>
               /// <returns></returns>
        public static string ToJSON(this List<DataTable> Jsontable)
        {
            int iTable = 0;
            dynamic JSONString = new StringBuilder();
            JSONString.Append("[");
            foreach (DataTable table in Jsontable)
            {
                if (table.Rows.Count > 0)
                {
                    for (int i = 0; i <= table.Rows.Count - 1; i++)
                    {
                        JSONString.Append("{");
                        for (int j = 0; j <= table.Columns.Count - 1; j++)
                        {
                            if (j < table.Columns.Count - 1)
                            {
                                if (table.Columns[j].DataType.Name == "MySqlDateTime" & table.Rows[i][j].ToString() == "0/0/0000 00:00:00")
                                {
                                    JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + "" + "\",");
                                }
                                else
                                {
                                    JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                                }

                            }
                            else if (j == table.Columns.Count - 1)
                            {
                                if (table.Columns[j].DataType.Name == "MySqlDateTime" & table.Rows[i][j].ToString() == "0/0/0000 00:00:00")
                                {
                                    JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + "" + "\"");
                                }
                                else
                                {
                                    JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                                }
                            }
                        }
                        if (iTable == Jsontable.Count - 1)
                        {
                            if (i == table.Rows.Count - 1)
                            {
                                JSONString.Append("}");
                            }
                            else
                            {
                                JSONString.Append("},");
                            }
                        }
                        else
                        {
                            //If i = table.Rows.Count - 1 Then
                            //    JSONString.Append("}")
                            //Else
                            //    JSONString.Append("},")
                            //End If
                            JSONString.Append("},");
                        }
                    }
                }
                iTable = iTable + 1;
            }
            JSONString.Append("]");
            return JSONString.ToString().Replace(Constants.vbCr, "").Replace(Constants.vbLf, "").Replace(Constants.vbTab, "");
            // carriage returns/ TAB Replace to Stirng 13-04-2017
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string ToJSON(this DataTable table)
        {
            dynamic JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i <= table.Rows.Count - 1; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j <= table.Columns.Count - 1; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            if (table.Columns[j].DataType.Name == "MySqlDateTime" & table.Rows[i][j].ToString() == "0/0/0000 00:00:00")
                            {
                                JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + "" + "\",");
                            }
                            else
                            {
                                JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                            }

                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            if (table.Columns[j].DataType.Name == "MySqlDateTime" & table.Rows[i][j].ToString() == "0/0/0000 00:00:00")
                            {
                                JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + "" + "\"");
                            }
                            else
                            {
                                JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                            }
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString().Replace(Constants.vbCr, "").Replace(Constants.vbLf, "").Replace(Constants.vbTab, "");
            // carriage returns/ TAB Replace to Stirng 13-04-2017
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="findreplace"></param>
        /// <returns></returns>
        private static string toReplace(this string str, List<KeyValuePair<string, string>> findreplace)
        {
            foreach (var element in findreplace)
            {
                str = str.Replace(element.Key, element.Value);
            }
            return str;
        }
                 
        /// <summary>
        /// 
        /// </summary>
        private class Col
        {
            public string ColName = "";
            public Int64 ColKey = 0;
            public string[] ColsName;
            public Int64 MaxLength;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToTable(this string json)
        {
            var jsonLinq = JObject.Parse(json);

            // Find the first array using Linq
            var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First();
            var trgArray = new JArray();
            foreach (JObject row in srcArray.Children<JObject>())
            {
                var cleanRow = new JObject();
                foreach (JProperty column in row.Properties())
                {
                    // Only include JValue types
                    if (column.Value is JValue)
                    {
                        cleanRow.Add(column.Name, column.Value);
                    }
                }

                trgArray.Add(cleanRow);
            }

            return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        public class PivotColumn
        {
            public string Name = "";
        }
        public class CustomColumn
        {
            public string FieldName;
            public string Formula;
        }


    }
}
