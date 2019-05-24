using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Data.Exception
{
    public class TableException : DataException
    {
        public TableException() : base()
        {

        }
        public TableException(string tableName, string message) : base(processMessage(tableName, message))
        {

        }
        public TableException(string tableName, string message, System.Exception ex) : base(processMessage(tableName, message), ex)
        {

        }
        private static string processMessage(string tname, string m)
        {
            return "Error in table name:" + tname + " Error message:" + m;
        }
    }

    public class TableFieldException : DataException {
        public string TableName;
        public string FieldName;
        public TableFieldException() : base()
        {
            TableName = "Unknown table name";
            FieldName= "Unknown field name";

        }
        public TableFieldException(string tableName,string fieldName,string message) : base(processMessage(tableName, fieldName, message))
        {
            TableName = tableName;
            FieldName = fieldName;

        }
        public TableFieldException(string tableName,string fieldName,string message,System.Exception ex) : base(processMessage(tableName,fieldName,message), ex)
        {
            TableName = tableName;
            FieldName = fieldName;
        }
        private static string processMessage(string tname, string fname, string m) {
            return "Error in  table name: " + tname + " and field name: " + fname + "Error message:" + m;
        }
    }

    public enum OperaitonType {       
        addfield,
        alterfield,
        dropfield,
        dropkey,
        addkey,
        unknown,
    }
    public class TableSchemaException : DataException
    {
        public string TableName;
        public OperaitonType Operation;
        public string FieldName;
        public TableSchemaException() : base()
        {
            TableName = "Unknown table name";
            Operation =  OperaitonType.unknown;
        }
        public TableSchemaException(string tablename,
            string fieldname,
            OperaitonType operation,
            string message) 
            : base(processMessage(tablename, fieldname, operation,null))
        {
            TableName = tablename;
            Operation = operation;
            FieldName = fieldname;
        }
        public TableSchemaException(string tablename,
            string fieldname,
            OperaitonType operation,
            string message,
            System.Exception ex)
            : base(processMessage(tablename,
                fieldname,
           operation, message))
        {

        }
        private static string processMessage(string tablename,
            string fieldname,
          OperaitonType operation,
          string message)
        {
            if (message == null)
            {
                return "Error while " + getoperation(operation, fieldname) + " " + tablename;
            }
            else
            {
                return "Error while " + getoperation(operation, fieldname) + " " + tablename + ":" + message;
            }
        }
        private static string getoperation(OperaitonType o,string fieldname) {
            if (o == OperaitonType.addfield)
            {
                return "adding column "+ fieldname +" in";
            }
            else if (o == OperaitonType.addkey)
            {
                return "adding primary key " + fieldname + " in ";
            }
            else if (o == OperaitonType.alterfield)
            {
                return "altering column "+ fieldname + " attributes in ";
            }            
            else if (o == OperaitonType.dropfield)
            {
                return "droping column "+ fieldname + " in ";
            }
            else if (o == OperaitonType.dropkey)
            {
                return "droping primary key in ";
            }
            else {
                return " altering table in ";
            }
        }     
       
    }

}
