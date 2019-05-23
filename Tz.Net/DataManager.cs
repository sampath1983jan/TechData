using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Net
{
 public class DataManager
    {
        private Entity.ITable Table;
        private Server Server;

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
            Table = new Entity.Table(tableID);
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public DataManager NewTable() {
            Table = new Entity.Table();
            return this;            
        }        
        /// <summary>
        /// 
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
            field.IsPrimaryKey = false;
            ((Entity.Table)Table).Add(field);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        public void AcceptChanges() {
            ((Entity.Table)Table).Save();            
        }
        /// <summary>
        /// 
        /// </summary>
        public void Remove() {
            Entity.Table t = (Entity.Table)Table;
            // write function here to drop table;
            t.Remove();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldid"></param>
        public void RemoveField(string fieldid) {
            Entity.Table t = (Entity.Table)Table;
            // write function here to remove field from table;
            t.RemoveField(fieldid);
        }
        /// <summary>
        /// 
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
           var field= ((Entity.Table)Table).NewField();
            field.FieldName = fieldName;
            field.FieldType = fieldType;
            field.Length = length;
            field.IsNullable = false;
            field.IsPrimaryKey = true;
            ((Entity.Table)Table).Add(field);
            return this;
        }
    }
}
