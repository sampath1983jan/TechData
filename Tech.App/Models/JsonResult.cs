using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

public class JsonpResult : ActionResult
{
    private readonly object _obj;

    public JsonpResult(object obj)
    {
        _obj = obj;
    }

    public override void ExecuteResult(ControllerContext context)
    {
        //   var serializer = new JavaScriptSerializer();
        var callbackname = context.HttpContext.Request["callback"];
        Boolean isDateExist = false;
        DataTable dt = new DataTable();
        if ((_obj).GetType().FullName.IndexOf("DataTable") >= 0)
        {
            dt = (DataTable)(_obj);
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.DataType.FullName == ("MySql.Data.Types.MySqlDateTime"))
                {
                    isDateExist = true;
                }
            }
        }


        var jsonp = "";
        if (isDateExist)
        {
            jsonp = dt.ToJSON();
            //   jsonp = string.Format("{0}({1})", (jsonp));
        }
        else
        {
            jsonp = string.Format("{0}({1})", callbackname, Newtonsoft.Json.JsonConvert.SerializeObject(_obj));
        }

        //  var jsonp = string.Format("{0}({1})", callbackname, Newtonsoft.Json.JsonConvert.SerializeObject(_obj));
        var response = context.HttpContext.Response;
        response.ContentType = "application/json";
        response.Write(jsonp);
    }
}

public class JsonpFilter : ActionFilterAttribute
{
    public string Param { get; set; }
    public Type JsonDataType { get; set; }
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (filterContext.HttpContext.Request.ContentType.Contains("application/json"))
        {
            string inputContent = filterContext.HttpContext.Request.Params[Param];
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var result = serializer.Deserialize(inputContent, JsonDataType);
            filterContext.ActionParameters[Param] = result;
        }
    }
}

public static class Extend {
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
}
     
