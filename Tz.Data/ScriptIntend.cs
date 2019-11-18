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
   public class ScriptIntend:DataBase
    {
        public ScriptIntend() {
            InitDbs("");
        }
        public DataTable GetIntend(string scriptID)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.SelectAll(TzAccount.ScriptIntend.Table).From(TzAccount.ScriptIntend.Table)
                .WhereField(TzAccount.ScriptIntend.Table, TzAccount.ScriptIntend.ScriptID.Name, Compare.Equals, DBConst.String(scriptID));
            return db.GetDatatable(select);
        }
        public DataTable GetScript(string intend) {             
                DBDatabase db;
                db = base.Database;
                DBQuery select;
                select = DBQuery.SelectAll(TzAccount.ScriptIntend.Table).From(TzAccount.ScriptIntend.Table)
                    .WhereField(TzAccount.ScriptIntend.Table, TzAccount.ScriptIntend.Intend.Name, Compare.Equals, DBConst.String(intend));
                return db.GetDatatable(select);             
        }

        public bool Save(string scriptID, string intend)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbscriptid = DBConst.String(scriptID);
            DBConst dbIntend = DBConst.String(intend);
            DBQuery insert = DBQuery.InsertInto(TzAccount.ScriptIntend.Table).Fields(
                TzAccount.ScriptIntend.ScriptID.Name,
                TzAccount.ScriptIntend.Intend.Name).Values(
                dbscriptid,
                dbIntend
                );
            int val = 0;
            using (DbTransaction trans = db.BeginTransaction())
            {
                val = db.ExecuteNonQuery(trans, insert);
                trans.Commit();
            }
            if (val > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(string scriptID, string intend)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbscriptid = DBConst.String(scriptID);
            DBConst dbIntend = DBConst.String(intend);

            DBQuery update = DBQuery.Update(TzAccount.ScriptIntend.Table).Set(
               TzAccount.ScriptIntend.Intend.Name, dbIntend
               ).WhereField(TzAccount.ScriptIntend.ScriptID.Name, Compare.Equals, dbscriptid);
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

        public bool Remove(string scriptID, string intend)
        {
            DBDatabase db;
            db = base.Database;
            DBComparison script = DBComparison.Equal(DBField.Field(TzAccount.ScriptIntend.ScriptID.Name), DBConst.String(scriptID));
            DBComparison intent = DBComparison.Equal(DBField.Field(TzAccount.ScriptIntend.Intend.Name), DBConst.String(intend));
            DBQuery del = DBQuery.DeleteFrom(TzAccount.ScriptIntend.Table)
                                .WhereAll(script, intent);
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
