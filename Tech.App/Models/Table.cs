using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Tz.BackApp.Models
{
    [Serializable]
    public class Table
    {
        public string TableID;
        public string TableName;
        public string Category;
        public Table() {
            TableID = "";
            TableName = "";
            Category = "";
        }
    }
    [Serializable]
    public class Field
    {
        public string FieldID { get; set; }
        public string FieldName { get; set; }
        public DbType FieldType { get; set; }
        public int Length { get; set; }
        public bool IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public string TableID  {get;set;}
        public bool IsChanged { get; set; }
        public string NewFieldName { get; set; }     
    }
}