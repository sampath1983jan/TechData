using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Tz.Net.Entity;
namespace Tz.Core
{
    public class ComponentAttribute:IField
    {
        private string fieldid;
        private string fieldName;
        private DbType fieldType;
        private int length;
        private bool isnull;
        private bool isprimary;
        private string tableid;
        private bool ischanged;


        public string ClientID { get;  set; }        
        public bool IsRequired { get; set; }
        public bool IsUnique { get; set; }
        public bool IsCore { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsSecured { get; set; }
        public int LookUpID { get; set; }
        public string DefaultValue { get; set; }
        public string FileExtension { get; set; }
        public string RegExp { get; set; }
        
                
        public string FieldID => fieldid;
        public string FieldName { get => fieldName; set => fieldName=value; }
        public DbType FieldType { get => fieldType; set => fieldType=value; }
        public int Length { get => length; set => length=value; }
        public bool IsNullable { get => isnull; set => isnull=value; }
        public bool IsPrimaryKey { get => isprimary; set => isprimary = value; }
        public string TableID => tableid;
        public string NewFieldName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool isChanged { get => ischanged; set => ischanged = value; }
    }
}
