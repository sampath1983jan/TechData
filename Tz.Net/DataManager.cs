using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data.Query;
using Tech.Data.Schema;
using Tz.Data;
using Tz.Global;
using Tz.ClientManager;

namespace Tz.Net
{
 public class DataManager
    {
        private Entity.ITable Table;
        private Tz.ClientManager.Server Server;
        private string ClientID;
        private bool isPrimaryUpdated;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverID"></param>
        public DataManager(string serverID,string clientID) {
            Server = new ClientManager.Server(serverID);
            ClientID = clientID;
            isPrimaryUpdated = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        public DataManager(ClientManager.Server server, string clientID)
        {
            Server = server;
            ClientID = clientID;
            isPrimaryUpdated = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableID"></param>
        /// <param name="serverID"></param>
        public DataManager(string tableID,string serverID, string clientID) {
            Server = new ClientManager.Server(serverID);
            ClientID = clientID;
            Table = new Entity.Table( serverID, tableID, ClientID);
            isPrimaryUpdated = false;
        }


        public Entity.ITable GetTable() {
            return Table;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public string getTableID() {
            return Table.TableID;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public DataManager NewTable(string tableName,string category) {
            tableName = tableName.Replace(" ", "_");
            Table = new Entity.Table(Server.ServerID, tableName,category,this.ClientID);
            return this;            
        }        
        /// <summary>
        /// add column
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldType"></param>
        /// <param name="length"></param>
        /// <param name="isNullable"></param>
        /// <returns></returns>
        public DataManager AddField(string fieldName,
            DbType fieldType,
            int length,
            bool isNullable
            ) {
            var f = this.Table.Fields.Where(x => x.FieldName.ToLower() == fieldName.ToLower()).FirstOrDefault();
            if (f != null) {
                return this;
            }
            var field = ((Entity.Table)Table).NewField();            
            field.FieldName = fieldName;
            field.FieldType = fieldType;
            field.Length = length;
            field.IsNullable = isNullable;
            field.isChanged = true;
            field.IsPrimaryKey = false;
            ((Entity.Table)Table).Add(field);
            return this;
        }
        /// <summary>
        /// add primary key fields
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldType"></param>
        /// <param name="length"></param>
        /// <returns></returns>        ///
        public DataManager AddPrimarykey(string fieldName,
            DbType fieldType,
            int length
            )
        {
            var f = this.Table.Fields.Where(x => x.FieldName.ToLower() == fieldName.ToLower()).FirstOrDefault();
            if (f != null)
            {
                return this;
            }
            var field = ((Entity.Table)Table).NewField();
            field.FieldName = fieldName;
            field.FieldType = fieldType;
            field.Length = length;
            field.IsNullable = false;
            field.IsPrimaryKey = true;
            field.isChanged = true;
            ((Entity.Table)Table).Add(field);
            return this;
        }
        
        /// <summary>
        /// alter field information add here
        /// </summary>
        /// <param name="fieldID"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldType"></param>
        /// <param name="length"></param>
        /// <param name="isNullable"></param>
        /// <param name="newfieldname"></param>
        /// <returns></returns>
        public DataManager ChangeField(string fieldID,string fieldName,
           DbType fieldType,
           int length,
           bool isNullable,bool isPrimary,string newfieldname=""
           )
        {
            var field = this.Table.Fields.Where(x=> x.FieldID.ToLower() == fieldID.ToLower()).FirstOrDefault();
            if (field == null)
            {
                throw new Data.Exception.TableFieldException(this.Table.TableName, fieldName, "Field dosenot exist");
            }
            else {
                // field.FieldName = fieldName;
                if (field.FieldType != fieldType || field.FieldName != fieldName || field.IsNullable != isNullable || field.Length != length || field.IsPrimaryKey != isPrimary) {
                    field.FieldType = fieldType;
                    field.Length = length;
                    if (field.IsPrimaryKey != isPrimary)
                    {
                        isPrimaryUpdated = true;
                    }
                    else {
                        isPrimaryUpdated = false;
                    }
                    field.IsPrimaryKey = isPrimary;
                    field.IsNullable = isNullable;
                    //  field.IsPrimaryKey = false;
                    if (fieldName != field.FieldName)
                    {
                       // field.FieldName = fieldName;
                        field.NewFieldName = fieldName;
                    }
                    field.isChanged = true;
                }                
                //((Entity.Table)Table).Add(field);
                return this;
            }            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newName"></param>
        /// <returns></returns>
        public bool Rename(string newName,string category) {            
            Data.TableBuilder tb = new Data.TableBuilder(this.Table.TableName, this.Server.Connection());
            try
            {
                newName = newName.Replace(" ", "_");
                if (newName != "") {
                    tb.Rename(newName);
                    this.Table.TableName = newName;
                }                                
                this.Table.Category = category;
                ((Entity.Table)Table).Save();
            } catch (Data.Exception.TableException ex) {
                throw ex;
            }
            return true;
        }
        /// <summary>
        /// table create,alter column ,add column,add primary key
        /// </summary>
        public void AcceptChanges(bool issync=false) {
            if (this.Table.TableID == "") // new table
            {
                if (issync == false)
                {
                    Data.TableBuilder tb = new Data.TableBuilder(this.Table.TableName, this.Server.Connection());
                    foreach (Entity.Field f in Table.Fields)
                    {
                        if (f.IsPrimaryKey == true)
                        {
                            DBColumn c = DBColumn.Column(f.FieldName, f.FieldType, f.Length);
                            tb.AddField(c);
                            tb.AddPrimaryKeyField(c);
                        }
                        else if (f.IsNullable == true)
                        {
                            tb.AddField(DBColumn.Column(f.FieldName, f.FieldType, f.Length, Tech.Data.DBColumnFlags.Nullable));
                        }
                        else
                        {
                            tb.AddField(DBColumn.Column(f.FieldName, f.FieldType, f.Length));
                        }
                    }
                    try
                    {
                        if (issync == false)
                        {
                            tb.Create();
                        }

                        ((Entity.Table)Table).Save();
                        //}
                    }

                    catch (Data.Exception.TableException ex)
                    {
                        throw ex;
                    }
                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
                }
                else {
                    ((Entity.Table)Table).Save();
                }
                          
            }
            else {
                Data.TableBuilder tb = new Data.TableBuilder(this.Table.TableName, this.Server.Connection());
                foreach (Entity.Field f in Table.Fields)
                {
                    if (f.isChanged == true || isKeyUpdated(f)) {
                        if (f.IsPrimaryKey == true)
                        {
                            DBColumn c = DBColumn.Column(f.FieldName, f.FieldType, f.Length);
                            if (f.FieldID == "")
                                tb.AddField(c);
                            else
                                tb.AlterField(c, f.NewFieldName == null ? "" : f.NewFieldName);

                            DBColumn k = DBColumn.Column( f.NewFieldName ==""? f.FieldName:f.NewFieldName,
                               f.FieldType,
                               f.Length,
                               Tech.Data.DBColumnFlags.PrimaryKey);
                            tb.AddPrimaryKeyField(c);
                        }
                        else if (f.IsNullable == true)
                        {
                            DBColumn c = DBColumn.Column(f.FieldName,
                                f.FieldType,
                                f.Length,
                                Tech.Data.DBColumnFlags.Nullable);
                            if (f.FieldID == "")
                                tb.AddField(c);
                            else
                                tb.AlterField(c, f.NewFieldName == null ? "" : f.NewFieldName);
                        }
                        //else if (isPrimaryUpdated == true) {
                        //    /// check primary key removed
                            
                        //}
                        else
                        {
                            DBColumn c = DBColumn.Column(f.FieldName,
                            f.FieldType,
                            f.Length);
                            if (f.FieldID == "")
                                tb.AddField(c);
                            else
                                tb.AlterField(c, f.NewFieldName == null ? "" : f.NewFieldName);
                        }
                    }                  
                }
                try
                {
                    if (tb.getFieldCount() > 0) {
                        if (issync == false)
                        {
                            tb.AlterTable();
                            if (tb.getPrimaryFieldCount() == 0 && isPrimaryUpdated == true)
                            {
                                tb.DropPrimarykey();
                            }                            
                        }
                        
                        ((Entity.Table)Table).Save();
                    }                   
                    
                }
                catch (Data.Exception.TableException ex)
                {
                    throw ex;
                }
                catch (System.Exception ex)
                {
                 //   tb.DropTable();
                  //  ((Entity.Table)Table).Remove();
                    throw new System.Exception("due to the error in this transaction, all process rollbacked. Error is,"+ ex.Message );
                }                
            }
           
        }
        private bool isKeyUpdated(Entity.Field f) {
            if (f.IsPrimaryKey == true && isPrimaryUpdated == true)
            {
                return true;
            }
            else {
                return false;
            }
        }
        /// <summary>
        /// drop table with columns
        /// </summary>
        public void Remove() {
            try
            {
                Entity.Table t = (Entity.Table)Table;
                var tb = new Data.TableBuilder(t.TableName, this.Server.Connection());
                tb.DropTable();
                    // write function here to drop table;
                t.Remove();                
            }
            catch (System.Exception ex) {
                throw ex;
            }          
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldid"></param>
        public void RemoveField(string fieldid) {
            Entity.Table t = (Entity.Table)Table;
            var tb = new Data.TableBuilder(t.TableName, this.Server.Connection());
            var f = this.Table.Fields.Where(x => x.FieldID == fieldid).FirstOrDefault();
            if (f == null) {
                throw new Data.Exception.TableFieldException(this.Table.TableName, fieldid, fieldid + " field dosenot exist");
            }
            try {
                DBColumn c = DBColumn.Column(f.FieldName, f.FieldType, f.Length);
                tb.AddDropField(c);

                tb.DropColumn();
                    t.RemoveField(fieldid);
                                // write function here to remove field from table;
            }

            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public int GetDataCount()
        {
            //  var tb = new Data.TableBuilder(t.TableName, this.Server.Connection());
            var t = new Data.Table();
            return t.GetDataCount(Table.TableName);
        }
        public DataTable GetData(int currentPage,int pageSize) {
            //  var tb = new Data.TableBuilder(t.TableName, this.Server.Connection());
            var t = new Data.Table();
            return t.GetData(currentPage, currentPage, Table.TableName);
        }
        public static async Task<int> synSever(string ClientID) {
            System.Data.DataTable dt = new DataTable();
           ClientServer c = new ClientServer(ClientID);
           Tz.ClientManager. Server s = c.GetServer();
            Tech.Data.DBDatabase db = Tech.Data.DBDatabase.Create(s.Connection(), "MySql.Data.MySqlClient");
            Tech.Data.Schema.DBSchemaProvider provider = db.GetSchemaProvider();
            IEnumerable<Tech.Data.Schema.DBSchemaItemRef> tables = provider.GetAllTables();
            int count = 0;
            List<Tz.Net.Entity.Table> dtTb =  Net.Entity.Table.GetTables(ClientID, s.ServerID);
            await Task.Run(() =>
            {
                var dm = new DataManager(s, c.ClientID);
                foreach (Tech.Data.Schema.DBSchemaItemRef df in tables) {
                    if (dtTb.Where(x => x.TableName.ToLower() == df.Name.ToLower()).FirstOrDefault() != null) {
                        continue;
                    }
                    dm.NewTable(df.Name, df.Catalog);
                    DBSchemaTable schema = provider.GetTable(df.Name);
                    foreach (Tech.Data.Schema.DBSchemaTableColumn dc in schema.Columns) {
                        if (dc.ColumnFlags == Tech.Data.DBColumnFlags.Nullable)
                        {
                            dm.AddField(dc.Name, dc.DbType, dc.Size, true);
                        }
                        else if (dc.ColumnFlags == Tech.Data.DBColumnFlags.PrimaryKey)
                        {
                            dm.AddPrimarykey(dc.Name, dc.DbType, dc.Size);
                        }
                        else {
                            dm.AddField(dc.Name, dc.DbType, dc.Size, false);
                        }                        
                    }
                    try {
                        dm.AcceptChanges(true);
                    }
                    catch (Exception ex) {

                    }                    
                    count = count + 1;
                }
                return count;
            });
            return count;
        }

    }
}

