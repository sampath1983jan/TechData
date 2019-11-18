using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data;
using Tech.Data.Query;
using Tech.Data.Schema;
using Tz.Global;
namespace Tz.Data
{
   public class TableBuilder: DataBase
    {
        private DBSchemaTable tables;
        private DBDatabase db;        
        private List<DBColumn> Columns;
        private List<DBColumn> AlterColumns;
        private List<DBColumn> PrimaryKey;
        private string Table;
        private List<string> NewColumns;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tbName"></param>
        /// <param name="conn"></param>
        public TableBuilder(string tbName,string conn) {
            InitDbs(conn);
            Table = tbName;
            db = base.Database;
            DBSchemaProvider provider = db.GetSchemaProvider();
            tables = provider.GetTable(tbName);
            AlterColumns = new List<DBColumn>();
            Columns= new List<DBColumn>();
            NewColumns = new List<string>();
            PrimaryKey = new List<DBColumn>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsTableExist() {         
            if (tables == null)
            {
                return false;
            }
            else {
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DropTable() {
            try
            {
                var q = DBQuery.Drop.Table(base.Schema, this.Table).IfExists();
                db.ExecuteNonQuery(q);
            }
            catch (System.Exception ex) {
                throw new Exception.TableException(this.Table, "Error while drop table");
            }
            return true;
        }

        public int getFieldCount() {
            return this.PrimaryKey.Count + this.AlterColumns.Count + this.Columns.Count;
        }
        public int getPrimaryFieldCount() {
            return this.PrimaryKey.Count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private bool IsFieldExist(string fieldName) {            
            if (tables != null)
            {
                if (tables.Columns.Where(x=> x.Name .ToLower() == fieldName.ToLower()).FirstOrDefault() !=null)
                {
                    return true;
                }
                else
                    return false;
            }
            else {
                if (Columns.Where(x => x.Name.ToLower() == fieldName.ToLower()).FirstOrDefault() == null)
                {
                    return false;
                }
                else
                    return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private bool IsFieldExist(string fieldName, List<DBColumn> columns) {
            if (IsTableExist())
            {
                if (tables.Columns.Where(x => x.Name.ToLower() == fieldName.ToLower()).FirstOrDefault() != null)
                {
                    return true;
                }
                else if (columns.Where(x => x.Name.ToLower() == fieldName.ToLower()).FirstOrDefault() != null)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public TableBuilder AddField(DBColumn c) {
            if (!IsFieldExist(c.Name))
            {
                this.Columns.Add(c);
            }
            else {
                throw new Exception.TableFieldException(this.Table, c.Name, "Field already exist");
            }            
            return this;
        }
        public TableBuilder AddDropField(DBColumn c) {
            if (IsFieldExist(c.Name))
            {
                this.Columns.Add(c);
            }
            else
            {
                throw new Exception.TableFieldException(this.Table, c.Name, "Field not exist");
            }
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public TableBuilder AddPrimaryKeyField(DBColumn c) {
           
            PrimaryKey.Add(c);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="newcolumn"></param>
        /// <returns></returns>
        public TableBuilder AlterField(DBColumn c, string newcolumn = "") {
            if (IsFieldExist(c.Name, AlterColumns))
            {
                this.AlterColumns.Add(c);
                NewColumns.Add(newcolumn);
            }
            else {
                throw new Exception.TableFieldException(this.Table, c.Name, "Field dosenot exist");
            }
            return this;
        }
        /// <summary>
        /// drop column in the table
        /// </summary>
        /// <returns></returns>
        public bool DropColumn()
        {
            //ALTER TABLE table
            // DROP COLUMN column_1,
            // DROP COLUMN column_2,
            string tb = "ALTER TABLE " + this.Table + " ";
            List<string> cols = new List<string>();
            int i=0;
            string fields = "";
            foreach (DBColumn dc in this.Columns)
            {
                i = i+1;
                if (this.Columns.Count == i)
                {
                    fields = fields + "," + dc.Name;
                    tb = tb + " DROP COLUMN " + dc.Name + ";";
                }
                else {
                    fields = fields + "," + dc.Name;
                    tb = tb + " DROP COLUMN " + dc.Name + ", ";
                }                
            }
            try
            {
                int r = db.ExecuteNonQuery(tb);
                return true;
            }
            catch (System.Exception ex) {
                throw new Exception.TableSchemaException(this.Table, fields, Exception.OperaitonType.dropfield, ex.Message);
            }
            
        } 

        public bool DropPrimarykey()
        {
            string tb = "ALTER TABLE `" + base.Schema + "`.`" + Table + "`";            
            tb = tb + " DROP PRIMARY KEY ";
            try
            {
                int r = db.ExecuteNonQuery(tb);
                return true;                
            }
            catch (System.Exception ex)
            {
                throw new Exception.TableSchemaException(this.Table, "", Exception.OperaitonType.addkey, ex.Message);
            }
        }
        /// <summary>
        /// change primary key
        /// </summary>
        /// <returns></returns>
        public bool AlterTableSetPrimarykey(bool isdrop=true)
        {
            if (this.PrimaryKey.Count == 0) {
                return true;
            }
            //ALTER TABLE `talentozdev`.`cf_fields` 
            //DROP PRIMARY KEY
            //, ADD PRIMARY KEY(`FieldID`);
            string tb = "ALTER TABLE `" + base.Schema + "`.`" + Table + "`";
            if (isdrop) {
                tb = tb + " DROP PRIMARY KEY, ";
            }            
            tb = tb + " Add PRIMARY KEY(";
            int i = 0;
            string fields = "";
            foreach (DBColumn dc in this.PrimaryKey)
            {
                i = i + 1;
                if (this.PrimaryKey.Count == i)
                {
                    fields = fields + "," + dc.Name;
                    tb = tb + "`" + dc.Name + "`";
                }
                else {
                    fields = fields + "," + dc.Name;
                    tb = tb + "`" + dc.Name + "`,";
                }                
            }
            tb = tb + ")";
            try
            {
                int r = db.ExecuteNonQuery(tb);
                return true;
                //if (r > 0)
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch (System.Exception ex) {
                throw new Exception.TableSchemaException(this.Table, fields, Exception.OperaitonType.addkey, ex.Message);
            }           
            
        }
        /// <summary>
        /// add column or alter attribute of existing column from existing table
        /// </summary>
        /// <returns></returns>
        public bool AlterTable() {
            int r=0;
            try
            {
                if (this.Columns.Count > 0)
                {
                    foreach (DBColumn dc in this.Columns)
                    {
                        string options;
                        string val;
                        string flag = "";
                        val = GetNativeTypeForDbType(dc.Type, dc.Length, dc.Precision, dc.Flags, out options);
                        if (string.IsNullOrEmpty(options) == false)
                        {
                            val = val + " " + options;
                        }
                        if (dc.Flags == DBColumnFlags.Nullable)
                        {
                            flag = " NULL ";
                        }
                        else if (dc.Flags == DBColumnFlags.PrimaryKey)
                        {
                            //  flag = " PRIMARY KEY ";
                        }
                        else
                        {
                            flag = " NOT NULL ";
                        }
                        string temp = " ALTER TABLE {0} ADD COLUMN {1} {2} {3}";
                        temp = string.Format(temp, Table, dc.Name, val, flag);
                        try
                        {
                            r = db.ExecuteNonQuery(temp);
                            if (this.PrimaryKey.Count > 0) {
                                AlterTableSetPrimarykey();
                            }
                        }
                        catch (System.Exception ex) {
                            throw new Exception.TableSchemaException(this.Table,dc.Name, Exception.OperaitonType.addfield, ex.Message, ex);
                        }                       
                    }                    
                }
                int index = 0;
                foreach (DBColumn dc in this.AlterColumns) {
                    string options;
                    string val;
                    string flag = "";
                    val = GetNativeTypeForDbType(dc.Type, dc.Length, dc.Precision, dc.Flags, out options);
                    if (string.IsNullOrEmpty(options) == false)
                    {
                        val = val + " " + options;
                    }
                    if (dc.Flags == DBColumnFlags.Nullable)
                    {
                        flag = " NULL ";
                    }
                    else if (dc.Flags == DBColumnFlags.PrimaryKey)
                    {
                        //  flag = " PRIMARY KEY ";
                    }
                    else
                    {
                        flag = " NOT NULL ";
                    }
                    //ALTER TABLE `talentozdev`.`cf_fields` CHANGE COLUMN `Length` `Lengths` VARCHAR(255) NULL DEFAULT NULL;
                    //ALTER TABLE `talentozdev`.`articles` CHANGE COLUMN `title` `title2` VARCHAR(200) CHARACTER SET 'latin1' NULL DEFAULT NULL;
                    string temp = "";
                    if (NewColumns[index] != "")
                    {
                        temp = " ALTER TABLE {0} CHANGE COLUMN  {1} {2} {3} {4}";
                        temp = string.Format(temp, Table, dc.Name, NewColumns[index], val, flag);
                    }
                    else {
                        temp = " ALTER TABLE {0} CHANGE COLUMN  {1} {2} {3} {4}";
                        temp = string.Format(temp, Table, dc.Name, dc.Name, val, flag);
                    }                   
                   //  temp = string.Format(temp, Table, dc.Name, val, flag); // change null,size
                    try
                    {
                        r = db.ExecuteNonQuery(temp);
                        if (this.PrimaryKey.Count > 0)
                        {
                            AlterTableSetPrimarykey();
                        }
                    }
                    catch(System.Exception ex){
                        throw new Exception.TableSchemaException(this.Table, dc.Name, Exception.OperaitonType.alterfield, ex.Message, ex);
                    }
                    index = index + 1;
                }
                return true;
            }           
            catch (System.Exception ex) {
                throw ex;
            }            
        }
        /// <summary>
        /// create table and columns
        /// </summary>
        /// <returns></returns>
        public bool Create() {
            try
            {
                DBCreateTableQuery dbcreate = DBQuery.Create.Table(base.Schema, Table);
                foreach (DBColumn dc in Columns)
                {
                    dbcreate.Add(dc);
                }
                int r = db.ExecuteNonQuery(dbcreate);                
                    AlterTableSetPrimarykey(false);
                return true;
            }
            catch (System.Exception ex) {
                throw new Exception.TableException(this.Table, ex.Message, ex); ;
            }            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NewName"></param>
        /// <returns></returns>
        public bool Rename(string NewName) {
            //ALTER TABLE old_table RENAME new_table;
            string query= "ALTER TABLE {0} RENAME {1}";
            query = string.Format(query, Table, NewName);
            try
            {
                 db.ExecuteNonQuery(query);
                
            }
            catch (System.Exception ex)
            {
                throw new Exception.TableException(this.Table, "Unable to change name of the table", ex);
            }
            return true;
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="setSize"></param>
        /// <param name="accuracy"></param>
        /// <param name="flags"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        protected virtual string GetNativeTypeForDbType(System.Data.DbType dbType, int setSize, int accuracy, DBColumnFlags flags, out string options)
        {
            string type;
            options = string.Empty;

            switch (dbType)
            {
                case System.Data.DbType.AnsiStringFixedLength:
                case System.Data.DbType.StringFixedLength:
                    if (setSize < 1)
                        setSize = 255;
                    else if (setSize > 255)
                        throw new ArgumentOutOfRangeException("CHAR size", "The maximum supported fixed length charater string is 255");

                    type = "CHAR";
                    options = "(" + setSize + ")";
                    break;

                case System.Data.DbType.String:
                case System.Data.DbType.AnsiString:
                    options = "";
                    if (setSize < 0)
                        setSize = 255;
                    if (setSize < 256)
                    {
                        type = "VARCHAR";
                        options = "(" + setSize.ToString() + ")";

                    }
                    else if (setSize < 65536)
                        type = "TEXT";
                    else if (setSize < 16777215)
                        type = "MEDIUMTEXT";
                    else
                        type = "LONGTEXT";

                    break;


                case System.Data.DbType.Binary:
                case System.Data.DbType.Object:

                    if (setSize > 0)
                    {
                        if (setSize < 256)
                            type = "TINYBLOB";
                        else if (setSize < 65536)
                            type = "BLOB";
                        else if (setSize < 16777216)
                            type = "MEDIUMBLOB";
                        else
                            type = "LONGBLOB";
                    }
                    else
                        type = "MEDIUMBLOB";
                    break;


                case System.Data.DbType.Boolean:
                    type = "BIT";
                    break;

                case System.Data.DbType.Byte:
                    type = "TINYINT";
                    options = " UNSIGNED";
                    break;

                case System.Data.DbType.Date:
                    type = "DATE";
                    break;

                case System.Data.DbType.DateTime:
                    type = "DATETIME";
                    break;

                case System.Data.DbType.DateTime2:
                    type = "DATETIME";
                    break;

                case System.Data.DbType.DateTimeOffset:
                    type = "TIMESTAMP";
                    break;

                case System.Data.DbType.Currency:
                case System.Data.DbType.Decimal:
                    type = "DECIMAL";
                    break;

                case System.Data.DbType.Double:
                    type = "DOUBLE";
                    break;

                case System.Data.DbType.Guid:
                    type = "BINARY";
                    options = "(16)";
                    break;

                case System.Data.DbType.Int16:
                    type = "SMALLINT";
                    break;

                case System.Data.DbType.Int32:
                    type = "INT";
                    break;

                case System.Data.DbType.Int64:
                    type = "BIGINT";
                    break;

                case System.Data.DbType.SByte:
                    type = "TINYINT";
                    break;

                case System.Data.DbType.Single:
                    type = "FLOAT";
                    break;

                case System.Data.DbType.Time:
                    type = "TIME";
                    break;

                case System.Data.DbType.UInt16:
                    type = "SMALLINT";
                    options = " UNSIGNED";
                    break;

                case System.Data.DbType.UInt32:
                    type = "INT";
                    options = " UNSIGNED";
                    break;
                case System.Data.DbType.UInt64:
                    type = "BIGINT";
                    options = " UNSIGNED";
                    break;
                case System.Data.DbType.VarNumeric:
                    type = "DECIMAL";
                    break;
                case System.Data.DbType.Xml:
                    type = "LONGTEXT";
                    break;
                default:
                    throw new NotSupportedException("The DbType '" + dbType.ToString() + "'is not supported");

            }

            return type;
        }

    }
}
