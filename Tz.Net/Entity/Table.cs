using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        string ServerID { get; set; }
        string ClientID { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Table : ITable
    {
        private string _tableid;
        private string _tablename;
        private string _category;
        private string _serverID;
        private string _clientID;
        private Data.Table dTable;
        private Data.Field dField;

        private List<IField> _fields;
        /// <summary>
        /// 
        /// </summary>
        public string TableID { get => _tableid; set => _tableid = value; }
        /// <summary>
        /// 
        /// </summary>
        public string TableName { get => _tablename; set => _tablename = value; }
        /// <summary>
        /// 
        /// </summary>
        public string Category { get => _category; set => _category = value; }
        /// <summary>
        /// 
        /// </summary>
        public List<IField> Fields { get { return _fields; } }
        /// <summary>
        /// 
        /// </summary>
        public string ServerID { get => _serverID; set => _serverID=value; }
        /// <summary>
        /// 
        /// </summary>
        public string ClientID { get => _clientID; set => _clientID = value; }

        /// <summary>
        /// 
        /// </summary>
        public Table() {
            _fields = new List<IField>();
            dTable = new Data.Table();
            _clientID = "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableid"></param>
        public Table(string serverID,string tableid,string clientID) {
            _serverID = serverID;
            _clientID = clientID;
            _tableid = tableid;
            _fields = new List<IField>();
            dTable = new Data.Table();
            Load();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="category"></param>
        public Table(string serverID,string tableName, string category, string clientID)
        {
            _clientID = clientID;
            _serverID = serverID;
            _tableid = "";
            _tablename = tableName;
            _category = category;
            _fields = new List<IField>();
            dTable = new Data.Table();
        }
        private dynamic dynamic(string x, string y) {
            int k = 0;
                Int32.TryParse(y,out k);
            if (x == "FieldType")
            {
                return (System.Data.DbType)k;
            }
            else {
                return y;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public static List<Table> GetTables(string clientid,string serverid) {
            var dTable = new Data.Table();
            DataTable dt = new DataTable();
            dt = dTable.GetTables(clientid,serverid);
            List<Table>ts                = dt.toList<Table>(new DataFieldMappings()               //  .Add(Tz.Data.TzAccount.Field.FieldID.Name, "FieldID")
               .Add(Tz.Data.TzAccount.Tables.TableID.Name, "TableID", true)
                  .Add(Tz.Data.TzAccount.Tables.TableName.Name, "TableName")
                .Add(Tz.Data.TzAccount.Tables.Category.Name, "Category")
                .Add(Tz.Data.TzAccount.Tables.ServerID.Name, "ServerID")
                .Add(Tz.Data.TzAccount.Tables.ClientID.Name, "ClientID")
                , null, null);
            return ts;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Load() {
         
            DataTable dt = new DataTable();
            dt=  dTable.GetTable(this.TableID);
            if (dt.Rows.Count > 0) {
            dynamic fs = dt.toList<Field>(new DataFieldMappings()               //  .Add(Tz.Data.TzAccount.Field.FieldID.Name, "FieldID")
                 .Add(Tz.Data.TzAccount.Field.FieldID.Name, "FieldID",true)
                    .Add(Tz.Data.TzAccount.Field.FieldName.Name, "FieldName")
                  .Add(Tz.Data.TzAccount.Field.FieldType.Name, "FieldType")
                  .Add(Tz.Data.TzAccount.Field.Length.Name, "Length")
                  .Add(Tz.Data.TzAccount.Field.ISPrimaryKey.Name, "IsPrimaryKey")
                  .Add(Tz.Data.TzAccount.Field.IsNullable.Name, "IsNullable")
                  ,  null,(x,y)=> dynamic(x,y));
                int i = 0;
                foreach (Field f in fs)
                {
                    f.setFieldID(dt.Rows[i]["FieldID"].ToString());
                    _fields.Add(f);
                    i = i + 1;
                }
                this.TableName = dt.Rows[0]["TableName"] == null ? "" : dt.Rows[0]["TableName"].ToString();
                this.Category = dt.Rows[0]["Category"] == null ? "" : dt.Rows[0]["Category"].ToString();
            }     
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool AcceptFieldChanges()
        {
            dField = new Data.Field();
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
                    dr[1] = field.NewFieldName ==""? field.FieldName : field.NewFieldName;
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Save() {
            dTable = new Data.Table();
            Data.Field dField = new Data.Field();
            if (Fields.Count == 0) {
                return "";
            }
            if (TableID == "")
            {
                TableID = dTable.Save(this.ServerID, this.TableName, this.Category,this.ClientID);
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
