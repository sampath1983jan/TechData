using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Tech.App.Models
{
    public static class DataResult
    {
       //  List<Fieldset> Fields;
       //public Paging page;
        public static string Create(System.Data.DataTable dt,int pageSize,int index, int totalRecord) {
            //Fields = new List<Fieldset>();
            //page = new Paging();
            //page.CurrentPage = index;
            //page.PageSize = pageSize;
            //page.TotalRecord = totalRecord;
            StringBuilder sb = new StringBuilder();
            sb.Append("{data:[");
            int rowLen = dt.Rows.Count;
            int rowIndex = 0;
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                int colLen = dt.Columns.Count;
                int cCol = 0;
                sb.Append("{");
                foreach (System.Data.DataColumn dc in dt.Columns)
                {
                    sb.Append("'"+ dc.ColumnName +"':'"+ dr[dc.ColumnName].ToString() +"'" );
                    cCol = cCol + 1;
                    if (colLen != cCol)
                    {
                        sb.Append(",");
                    }
                }               
                sb.Append("}");
                rowIndex= rowIndex + 1;
                if (rowLen != cCol)
                {
                    sb.Append(",");
                }
            }
            sb.Append("]");
         // string dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            // sb.Append(dataJson.Trim());            
            //sb.Append(",Page:{PageNo:"+ index + ",TotalRecord:"+ totalRecord + ",PageSize:"+ pageSize  +" }}");
            //return sb.ToString();
            Fieldset fs = new Fieldset();
            fs.Data = dt;
            fs.Page = new Paging();
            fs.Page.PageSize = pageSize;
            fs.Page.CurrentPage = index;
            fs.Page.TotalRecord = totalRecord;
            string dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(fs);
            return dataJson;
        }
    }
    public class Paging {
        public int TotalRecord;
        public int CurrentPage;
        public int PageSize;
        public Paging() {

        }
    }
    public class Fieldset {
        public System.Data.DataTable Data;
        public Paging Page;
        public Fieldset() {

        }
    }
}