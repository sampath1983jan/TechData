using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml.Serialization;

namespace Tz.Net.Entity
{
    public interface IField
    {
        string FieldID { get; }
        string FieldName { get; set; }
        DbType FieldType { get; set; }
        int Length { get; set; }
        bool IsNullable { get; set; }
        bool IsPrimaryKey { get; set; }        
        string TableID { get; }
        string NewFieldName { get; set; }
        bool isChanged { get; set; }
    }

    public class Field : IField
    {
        public string _fieldid;
        public string _fieldName;
        public DbType _fieldtype;
        public int _length;
        public bool _isNullable;
        public bool _isPrimarykey;
        public string _tableID;
        public string _newFieldname;

        public string FieldID { get => _fieldid; }
        public string FieldName { get => _fieldName; set => _fieldName = value; }
        public DbType FieldType { get => _fieldtype; set => _fieldtype = value; }
        public int Length { get => _length; set => _length = value; }
        public bool IsNullable { get => _isNullable; set => _isNullable = value; }
        public bool IsPrimaryKey { get => _isPrimarykey; set => _isPrimarykey = value; }
        public string TableID { get => _tableID; }
        public string NewFieldName { get => _newFieldname; set => _newFieldname = value; }
        public bool isChanged { get; set; }

        //  bool isChanged { get; set; }

        public Field() {
            _fieldid = "";
            FieldName = "";
            Length = 0;
            IsNullable = true;
            IsPrimaryKey = true;
            _tableID = "";
            _newFieldname = "";
            isChanged = false;
        }
        internal void setFieldID(string id){
            _fieldid = id;
}
        public Field(string tableid) {
            _fieldid = "";
            FieldName = "";
            Length = 0;
            IsNullable = true;
            IsPrimaryKey = true;
            _tableID = tableid;
            _newFieldname = "";
            isChanged = false;
        }
        public bool Save() {

            return true;
        }

        public bool Update() {
            Data.Field dField = new Data.Field();
            return dField.Update(FieldName, (int)FieldType, Length, IsNullable, IsPrimaryKey,this.TableID, _fieldid);
        }
        internal bool Remove() {
            Data.Field dField = new Data.Field();
           return dField.RemoveField(_fieldid);           
        }
    }
}
