using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax
{
    

   public  class DateParse:Fun
    {
        private List<ParamFields> _arguments;
        public string ColumnName;
        public string ParseType;

        public DateParse(Declare assignTo, string dataSource, Source.SourceSpan span)
            : base(assignTo, FunctionType.Dateparse, dataSource, span)
        {
            _arguments = new List<ParamFields>();
        }

        public ParseType GetParse() {
            ParseType = ParseType.ToLower();
            if (ParseType == "day")
            {
                return global::ParseType._DAY;
            }
            else if (ParseType == "date")
            {
                return global::ParseType._DATE;
            }
            else if (ParseType == "month") {
                return global::ParseType._MONTH;
            }
            else if (ParseType == "yq")
            {
                return global::ParseType._YEARQUARTER;
            }
            else if (ParseType == "ym")
            {
                return global::ParseType._YEARMONTH;
            }
            else if (ParseType == "y")
            {
                return global::ParseType._YEAR;
            }
            else if (ParseType == "q")
            {
                return global::ParseType._QUARTER;
            }
            else if (ParseType == "w")
            {
                return global::ParseType._WEEK;
            }
            else if (ParseType == "sec")
            {
                return global::ParseType._SEC;
            }
            else if (ParseType == "m")
            {
                return global::ParseType._MIN;
            }
            else if (ParseType == "h")
            {
                return global::ParseType._HOUR;
            }
            else if (ParseType == "ddmmyyyy")
            {
                return global::ParseType._DDMMMYYYY;
            }
            else if (ParseType == "ddmmyyyy")
            {
                return global::ParseType._DDMMYYYY;
            }
            else if (ParseType == "mmddyyyy")
            {
                return global::ParseType._MMDDYYYY;
            }
            else if (ParseType == "mmmddyyyy")
            {
                return global::ParseType._MMMDDYYYY;
            }
            else  
            {
                return global::ParseType._DATE;
            }
        }
    }
}
