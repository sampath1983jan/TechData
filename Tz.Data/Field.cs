using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Tech.Data;
using Tech.Data.Query;
using System.Data.Common;
using Tz.Global;

namespace Tz.Data
{
   public class Field:DataBase
    {
        DBDatabase db;
        public Field()
        {
            InitDbs("");
            db = base.Database;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public DataTable GetFields(string tableid)
        {
            DBQuery select;
            select = DBQuery.SelectAll(TzAccount.Field.Table).From(TzAccount.Field.Table)
                .LeftJoin(TzAccount.Field.Table).On(TzAccount.Field.TableID, Compare.Equals, TzAccount.Field.TableID);
            
            return db.GetDatatable(select);
        }

        public string Save(string fieldName,
            int fieldType,
            int length,
           bool isnull,
           bool isPrimarykey,
           string tableID) {

            string a = Shared.generateID();
            DBConst dbfieldid = DBConst.String(a);
            DBConst dbtableID = DBConst.String(tableID);
            DBConst dbfieldName = DBConst.String(fieldName);
            DBConst dbfieldtype = DBConst.Int32(fieldType);
            DBConst dbisnull = DBConst.Const(DbType.Boolean,isnull);
            DBConst dbisprimary = DBConst.Const(DbType.Boolean, isPrimarykey);
            DBConst dblength = DBConst.Int32(length);

            DBQuery insert = DBQuery.InsertInto(TzAccount.Field.Table).Fields(
                TzAccount.Field.TableID.Name,
              TzAccount.Field.FieldID.Name,
              TzAccount.Field.FieldName.Name,
              TzAccount.Field.FieldType.Name,
              TzAccount.Field.Length.Name,
              TzAccount.Field.IsNullable.Name,
              TzAccount.Field.ISPrimaryKey.Name
          ).Values(
                dbtableID,
              dbfieldid,
              dbfieldName,
              dbfieldtype, 
              dblength,
              dbisnull, 
              dbisprimary
              );
            int val = 0;
            using (DbTransaction trans = db.BeginTransaction())
            {
                val = db.ExecuteNonQuery(trans, insert);
                trans.Commit();
            }
            if (val > 0)
            {
                return a;
            }
            else
            {
                return "";
            }

        }

           public bool Save(ref DataTable dtFields) {                        
            DataRow item = null;
            dtFields.Columns.Add(new DataColumn("fieldid"));
            for (int i = 0; i < dtFields.Rows.Count; i++)
            {
                dtFields.Rows[i]["fieldid"] = Shared.generateID();
            }
            DBParam dbfieldid = DBParam.ParamWithDelegate(DbType.String, delegate { return item["fieldid"] == null ? Shared.generateID() : item["fieldid"]; });
            DBParam dbTableid = DBParam.ParamWithDelegate(DbType.String, delegate { return item["tableid"] == null ? "" : item["tableid"]; });
            DBParam dbfieldName = DBParam.ParamWithDelegate(DbType.String, delegate { return item["fieldname"] == null ? "" : item["fieldname"]; });
            DBParam dbfieldtype = DBParam.ParamWithDelegate(DbType.Int32, delegate { return (item["fieldtype"] == null ? 1 : item["fieldtype"]); });
            DBParam dbisnull = DBParam.ParamWithDelegate(DbType.Boolean, delegate { return (item["isnull"] == null ? false : item["isnull"]); });
            DBParam dbisprimary = DBParam.ParamWithDelegate(DbType.Boolean, delegate { return (item["isprimary"]==null?false: item["isprimary"]); });
            DBParam dblength = DBParam.ParamWithDelegate(DbType.Int32, delegate { return (item["length"]==null? 0: item["length"]); });

            
                DBQuery insert = DBQuery.InsertInto(TzAccount.Field.Table).Fields(
              TzAccount.Field.TableID.Name,
              TzAccount.Field.FieldID.Name,
              TzAccount.Field.FieldName.Name,
              TzAccount.Field.FieldType.Name,
              TzAccount.Field.Length.Name,
              TzAccount.Field.IsNullable.Name,
              TzAccount.Field.ISPrimaryKey.Name
          ).Values(
                dbTableid,
              dbfieldid,
              dbfieldName,
              dbfieldtype, 
              dblength,
              dbisnull, 
              dbisprimary
              );             
            int sum = 0;
            using (DbTransaction trans = db.BeginTransaction())
            {
               
                for (int i = 0; i < dtFields.Rows.Count; i++)
                {
                    item = dtFields.Rows[i];
                    sum += db.ExecuteNonQuery(trans, insert);
                }                  
                trans.Commit();
            }
            if (sum  == dtFields.Rows.Count )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(string fieldName,
            int fieldType,
            int length,
           bool isnull,
           bool isPrimarykey,
           string tableID,string fieldid)
        {            
            DBConst dbfieldid = DBConst.String(fieldid);
            DBConst dbfieldName = DBConst.String(fieldName);
            DBConst dbfieldtype = DBConst.Int32(fieldType);
            DBConst dbisnull = DBConst.Const(DbType.Boolean, isnull);
            DBConst dbisprimary = DBConst.Const(DbType.Boolean, isPrimarykey);
            DBConst dblength = DBConst.Int32(length);

            DBQuery update = DBQuery.Update(TzAccount.Field.Table).Set(
               TzAccount.Field.FieldName.Name, dbfieldName
               ).Set(
               TzAccount.Field.FieldType.Name, dbfieldtype
               ).Set(
               TzAccount.Field.ISPrimaryKey.Name, dbisprimary
               ).Set(
               TzAccount.Field.IsNullable.Name, dbisnull
               ).Set(
               TzAccount.Field.Length.Name, dblength
               )
               .WhereField(TzAccount.Field.FieldID.Name, Compare.Equals, dbfieldid);
            int i = db.ExecuteNonQuery(update);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool RemoveAll(string tableID)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbTableid = DBConst.String(tableID);

            DBQuery del = DBQuery.DeleteFrom(TzAccount.Field.Table)
                                .WhereFieldEquals(TzAccount.Field.TableID.Name, dbTableid);
            int i = db.ExecuteNonQuery(del);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool RemoveField(string fieldid)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbfieldid = DBConst.String(fieldid);
 
            DBQuery del = DBQuery.DeleteFrom(TzAccount.Field.Table)
                                .WhereFieldEquals(TzAccount.Field.FieldID.Name, dbfieldid);
            int i = db.ExecuteNonQuery(del);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
