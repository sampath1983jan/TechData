﻿using System;
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
        string FieldID { get; set; }
        string FieldName { get; set; }
        DbType FieldType { get; set; }
        int Length { get; set; }
        bool IsNullable { get; set; }
        bool IsPrimaryKey { get; set; }        
        string TableID { get; }
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

        public string FieldID { get => _fieldid; set => _fieldid=value; }
        public string FieldName { get => _fieldName; set => _fieldName = value; }
        public DbType FieldType { get => _fieldtype; set => _fieldtype = value; }
        public int Length { get => _length; set => _length = value; }
        public bool IsNullable { get => _isNullable; set => _isNullable = value; }
        public bool IsPrimaryKey { get => _isPrimarykey; set => _isPrimarykey = value; }
        public string TableID { get => _tableID; }

    }
}
