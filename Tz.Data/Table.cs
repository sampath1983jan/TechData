using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Tech.Data;
using Tech.Data.Query;
using System.Data.Common;

namespace Tz.Data
{
   public class Table:DataBase
    {
        DBDatabase db;
        public Table() {
            InitDbs("");
            db = base.Database;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetTables() {
            DBQuery select;
            select = DBQuery.SelectAll(TzAccount.Tables.Table).From(TzAccount.Tables.Table);
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public DataTable GetTable(string tableid) {
            DBConst dbtableid = DBConst.String(tableid);
            DBQuery select;
            select = DBQuery.SelectAll().From(TzAccount.Tables.Table)
                .LeftJoin(TzAccount.Field.Table).On(TzAccount.Field.Table,TzAccount.Field.TableID.Name, Compare.Equals, TzAccount.Tables.Table, TzAccount.Tables.TableID.Name)
                .WhereField(TzAccount.Tables.Table, TzAccount.Tables.TableID.Name, Compare.Equals, dbtableid);

            return db.GetDatatable(select);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="tableCategory"></param>
        /// <returns></returns>
        public string Save(string tableName,
            string tableCategory)
        {
            string a = Shared.generateID();
            DBConst dbTableid = DBConst.String(a);
            DBConst dbtableName = DBConst.String(tableName);
            DBConst dbtableCategory = DBConst.String(tableCategory);

            DBQuery insert = DBQuery.InsertInto(TzAccount.Tables.Table).Fields(
              TzAccount.Tables.TableID.Name,
              TzAccount.Tables.TableName.Name,
              TzAccount.Tables.Category.Name
          ).Values(
              dbTableid,
              dbtableName,
              dbtableCategory
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableid"></param>
        /// <param name="tableName"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool Update(string tableid,
            string tableName,
            string category)
        {
            DBConst dbtableid = DBConst.String(tableid);
            DBConst dbtableName = DBConst.String(tableName);
            DBConst dbcategory = DBConst.String(category);
            DBQuery update = DBQuery.Update(TzAccount.Tables.Table).Set(
                TzAccount.Tables.TableName.Name, dbtableName
                ).Set(
                TzAccount.Tables.Category.Name, dbcategory
                ).WhereField(TzAccount.Tables.TableID.Name, Compare.Equals, dbtableid);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public bool Remove(string tableid) {
            DBDatabase db;
            db = base.Database;
            DBConst dbtableid = DBConst.String(tableid);

            DBQuery del = DBQuery.DeleteFrom(TzAccount.Tables.Table)
                                .WhereFieldEquals(TzAccount.Tables.TableID.Name, dbtableid);
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
