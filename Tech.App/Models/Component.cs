using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tz.BackApp.Models
{
    public class Component
    {
        /// <summary>
        /// 
        /// </summary>
        public string ClientID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ComponentID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ComponentName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ComponentType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TableID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PrimaryKeys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TitleFormat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NewLayout { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ReadLayout { get; set; }
        public string Category { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool IsGlobal { get; set; }
    }

    public class Attribute {
        public string AttributeName { get; set; }
        public string ClientID { get; set; }
        public bool IsRequired { get; set; }
        public bool IsUnique { get; set; }
        public bool IsCore { get; set; }      
        public bool IsReadOnly { get; set; }
        public bool IsSecured { get; set; }
        public bool IsAuto { get; set; }
        public string LookUpID { get; set; }
        public string DefaultValue { get; set; }
        public string FileExtension { get; set; }
        public string RegExp { get; set; }
        public int AttributeType { get; set; }
        public string FieldID {get;set;}
        public string FieldName { get; set; }
        //public DbType FieldType { get; set; }
        public int Length { get; set; }
        public bool IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public string TableID   { get; set; }
        public string NewFieldName { get; set; }
        public bool isChanged { get; set; }
    }

    public class LookupItem {
        public string LookUpItemID { get;  set; }
        public string ClientID { get;  set; }
        public string LookUpID { get;  set; }
        public string Label { get; set; }
        public string ShortLabel { get; set; }
        public string Description { get; set; }
        public string ParentID { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
    }
}


