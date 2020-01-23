using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Tz.Net.Entity;
namespace Tz.Core
{
    public class ComponentAttribute:IField
    {
        public enum ComoponentAttributeType {
            _number,
            _decimal,
            _string,
            _longstring,
            _currency,
            _lookup,
            _componentlookup,
            _file,
            _picture,
            _date,
            _time,
            _datetime,
            _bit,
        }

        private string _fieldid;
        private string fieldName;
        private DbType fieldType;
        private int length;
        private bool isnull;
        private bool isprimary;
        private string _tableid;
        private bool ischanged;
        public string ComponentID { get; set; }
        public string AttributeName { get; set; }
        public string ClientID { get;  set; }        
        public bool IsRequired { get; set; }
        public bool IsUnique { get; set; }
        public bool IsCore { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsSecured { get; set; }
        public bool IsAuto { get; set; }
        public string LookUpID { get; set; }
        public string DefaultValue { get; set; }
        public string FileExtension { get; set; }
        public string RegExp { get; set; }
        public ComoponentAttributeType AttributeType { get; set; }
        public string FieldID { get => _fieldid; set => _fieldid = value; }
        public string FieldName { get => fieldName; set => fieldName=value; }
        public DbType FieldType { get => fieldType; set => fieldType=value; }
        public int Length { get => length; set => length=value; }
        public bool IsNullable { get => isnull; set => isnull=value; }
        public bool IsPrimaryKey { get => isprimary; set => isprimary = value; }
        public string TableID => _tableid;
        public string NewFieldName { get => ""; set => value=""; }
        public bool isChanged { get => ischanged; set => ischanged = value; }
        
        public void setFieldID(string fid) {
            _fieldid = fid;
        }
        public ComponentAttribute(string clientID,string componentid,string fieldid) {
            this.ClientID = clientID;
            ComponentID = componentid;
            _fieldid = fieldid;
        }
        public ComponentAttribute(string clientID)
        {
            this.ClientID = clientID;
            ComponentID = "";
            _fieldid = "";
        }
        public ComponentAttribute() {
            this.ClientID = "";
            ComponentID = "";
            _fieldid = "";
        }
    }
}
