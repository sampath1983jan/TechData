using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data.Query;

namespace Tz.Net
{
 public class DataManager
    {
        private Entity.ITable Table;
        private Server Server;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverID"></param>
        public DataManager(string serverID) {
            Server = new Server(serverID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableID"></param>
        /// <param name="serverID"></param>
        public DataManager(string tableID,string serverID) {
            Server = new Server(serverID);
            Table = new Entity.Table(tableID, serverID);
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
            Table = new Entity.Table(Server.ServerID, tableName,category);
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
           bool isNullable,string newfieldname=""
           )
        {
            var field = this.Table.Fields.Where(x=> x.FieldName.ToLower() == fieldName.ToLower()).FirstOrDefault();
            if (field == null)
            {
                throw new Data.Exception.TableFieldException(this.Table.TableName, fieldName, "Field dosenot exist");
            }
            else {
                field.FieldName = fieldName;
                field.FieldType = fieldType;
                field.Length = length;
                field.IsNullable = isNullable;
                field.IsPrimaryKey = false;
                field.NewFieldName = newfieldname;
                field.isChanged = true;
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
        public void AcceptChanges() {
            if (this.Table.TableID == "") // new table
            {                
                Data.TableBuilder tb = new Data.TableBuilder(this.Table.TableName, this.Server.Connection());
                foreach (Entity.Field f in Table.Fields) {
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
                    else {
                        tb.AddField(DBColumn.Column(f.FieldName, f.FieldType, f.Length));
                    }
                }
                try
                {
                    tb.Create();
                    ((Entity.Table)Table).Save();
                    //}
                }
                catch ( Data.Exception.TableException ex) {                   
                    throw ex;
                }
                catch (System.Exception ex)
                {
                    tb.DropTable();
                    ((Entity.Table)Table).Remove();
                    throw ex;
                }                
            }
            else {
                Data.TableBuilder tb = new Data.TableBuilder(this.Table.TableName, this.Server.Connection());
                foreach (Entity.Field f in Table.Fields)
                {
                    if (f.isChanged == true) {
                        if (f.IsPrimaryKey == true)
                        {
                            DBColumn c = DBColumn.Column(f.FieldName, f.FieldType, f.Length);
                            if (f.FieldID == "")
                                tb.AddField(c);
                            else
                                tb.AlterField(c,f.NewFieldName);

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
                                tb.AlterField(c,f.NewFieldName);
                        }
                        else
                        {
                            DBColumn c = DBColumn.Column(f.FieldName,
                            f.FieldType,
                            f.Length);
                            if (f.FieldID == "")
                                tb.AddField(c);
                            else
                                tb.AlterField(c,f.NewFieldName);
                        }
                    }                  
                }
                try
                {
                    if (tb.getFieldCount() > 0) {
                        tb.AlterTable();
                        ((Entity.Table)Table).Save();
                    }                   
                    
                }
                catch (Data.Exception.TableException ex)
                {
                    throw ex;
                }
                catch (System.Exception ex)
                {
                    tb.DropTable();
                    ((Entity.Table)Table).Remove();
                    throw new System.Exception("due to the error in this transaction, all process rollbacked. Error is,"+ ex.Message );
                }                
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
                if (tb.DropTable())
                {
                    // write function here to drop table;
                    t.Remove();
                }
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
            DBColumn c = DBColumn.Column(f.FieldName, f.FieldType, f.Length);
            if (tb.DropColumn()) {
                t.RemoveField(fieldid);
            }                // write function here to remove field from table;
            
        }
        
    }
}
