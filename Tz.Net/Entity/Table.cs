using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Tz.Net.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITable {
        string TableID { get; set; }
        string TableName { get; set; }
        string Category { get; set; }
          List<IField> Fields { get; }
    }
    /// <summary>
    /// 
    /// </summary>
  public  class Table:ITable
    {
        private string _tableid;
        private string _tablename;
        private string _category;
        private Data.Table dTable;
        private Data.Field dField;
        /// <summary>
        /// 
        /// </summary>
        public string TableID { get => _tableid; set => _tableid=value; }
        /// <summary>
        /// 
        /// </summary>
        public string TableName { get => _tablename; set => _tablename=value; }
        /// <summary>
        /// 
        /// </summary>
        public string Category { get => _category; set => _category=value; }
        /// <summary>
        /// 
        /// </summary>
        public List<IField> Fields { get; }
        /// <summary>
        /// 
        /// </summary>
        public Table() {
            Fields = new List<IField>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableid"></param>
        public Table(string tableid) {
            _tableid = tableid;
            Fields = new List<IField>();
        }

        private bool AcceptFieldChanges()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("tableid", typeof(string));
            dt.Columns.Add("fieldname", typeof(string));
            dt.Columns.Add("fieldtype", typeof(int));
            dt.Columns.Add("isnull", typeof(bool));
            dt.Columns.Add("isprimary", typeof(bool));
            dt.Columns.Add("length", typeof(int));
            DataRow dr;
            foreach (Field field in this.Fields)
            {
                if (field.FieldID == "")
                {
                    dr = dt.NewRow();
                    dr[0] = this.TableID;
                    dr[1] = field.FieldName;
                    dr[2] = field.FieldType;
                    dr[3] = field.IsNullable;
                    dr[4] = field.IsPrimaryKey;
                    dr[5] = field.Length;
                    dt.Rows.Add(dr);
                }
            }
            if (dt.Rows.Count > 0)
            {
                if (dField.Save(dt))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            foreach (Field field in this.Fields)
            {
                if (field.FieldID != "")
                {
                    field.Update();
                }
            }
            return true;
        }

        public string Save() {
            dTable = new Data.Table();
            Data.Field dField = new Data.Field();
            if (Fields.Count == 0) {
                return "";
            }
            if (TableID == "")
            {
                TableID = dTable.Save(this.TableName, this.Category);
                if (TableID != "")
                {
                    AcceptFieldChanges();
                    return TableID;
                }
                else
                {
                    return "";
                }
            }
            else {
                dTable.Update(TableID,this.TableName, this.Category);
                  AcceptFieldChanges();
                return TableID;
            }            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IField NewField() {
            var f= new Field(this.TableID);
            f.FieldType = System.Data.DbType.String;
            f.IsNullable = true;
            f.IsPrimaryKey = false;
            f.FieldName = "";            
            return f;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public Table Add(IField field) {
            Fields.Add(field);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public Table RemoveField(string  field) {
            var f = Fields.Where(x => x.FieldID == field).FirstOrDefault();
            if (f != null) {                
                Data.Field dField = new Data.Field();
                ((Field)f).Remove();
                Fields.Remove(f);
            }            
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Remove() {
            dTable = new Data.Table();
            Data.Field dField = new Data.Field();
            if (dTable.Remove(this.TableID)) {
                dField.RemoveAll(this.TableID);
                return true;
            }else
            {
                return false;
            }
        }        

    }
}
