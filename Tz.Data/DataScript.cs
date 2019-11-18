using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Tech.Data.Query;
using Tech.Data;
using System.Data.Common;
using Tz.Global;
namespace Tz.Data
{
 public   class DataScript:DataBase
    {
        public DataScript() {
            InitDbs("");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetScripts() {
            DBDatabase db;
            db = base.Database;
            DBQuery select;

            select = DBQuery.SelectAll().From(TzAccount.DataScript.Table).LeftJoin(TzAccount.ScriptIntend.Table).
                On(TzAccount.DataScript.Table,TzAccount.DataScript.ScriptID.Name ,Compare.Equals,TzAccount.ScriptIntend.Table,TzAccount.ScriptIntend.ScriptID.Name);                
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptID"></param>
        /// <returns></returns>
        public DataTable GetScript(string scriptID)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;

            select = DBQuery.SelectAll(TzAccount.DataScript.Table).From(TzAccount.DataScript.Table)
                .WhereField(TzAccount.DataScript.Table, TzAccount.DataScript.ScriptID.Name, Compare.Equals, DBConst.String(scriptID));
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public string Save(string script,string ScriptName,string category) {
            DBDatabase db;
            db = base.Database;
            string a = Shared.generateID();
            DBConst dbscriptID = DBConst.String(a);
            DBConst dbscript = DBConst.String(script);
            DBConst dbScriptName = DBConst.String(ScriptName);
            DBConst dbcategory = DBConst.String(category);
            DBQuery insert = DBQuery.InsertInto(TzAccount.DataScript.Table).Fields(
               TzAccount.DataScript.ScriptID.Name,
               TzAccount.DataScript.ScriptName.Name,
               TzAccount.DataScript.Category.Name,
               TzAccount.DataScript.Script.Name).Values(
               dbscriptID,
               dbScriptName,
               dbcategory,
               dbscript
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
        /// <param name="scriptID"></param>
        /// <param name="script"></param>
        /// <returns></returns>
        public bool Update(string scriptID, string script,string name,string category) {
            DBConst dbscript = DBConst.String(script);
            DBDatabase db;
            db = base.Database;
            DBComparison scriptid = DBComparison.Equal(DBField.Field(TzAccount.DataScript.ScriptID.Name), DBConst.String(scriptID));
            //DBComparison server = DBComparison.Equal(DBField.Field(TzAccount.DataScript.ServerID.Name), DBConst.String(serverID));
            DBQuery update = DBQuery.Update(TzAccount.DataScript.Table).Set(
            TzAccount.DataScript.Script.Name, dbscript
            ).Set(
            TzAccount.DataScript.ScriptName.Name, DBConst.String(name)
            ).Set(
            TzAccount.DataScript.Category.Name, DBConst.String(category)
            ).WhereAll(scriptid);
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
        /// <param name="scriptID"></param>
        /// <returns></returns>
        public bool Remove(string scriptID) {
            DBDatabase db;
            db = base.Database;
            
            DBComparison dbscriptID = DBComparison.Equal(DBField.Field(TzAccount.DataScript.ScriptID.Name), DBConst.String(scriptID));
            DBQuery del = DBQuery.DeleteFrom(TzAccount.DataScript.Table)
                                .WhereAll(dbscriptID);
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
