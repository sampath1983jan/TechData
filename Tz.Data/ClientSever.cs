using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Tech.Data.Query;
using Tech.Data;
using System.Data.Common;

namespace Tz.Data
{
   public class ClientSever:DataBase
    {
        public ClientSever() {
            InitDbs("");
        }

        public bool AssignClientServer(string clientid,string serverid) {
            DBDatabase db;
            db = base.Database;
            DBConst dbClientID = DBConst.String(clientid);
            DBConst dbServerID = DBConst.String(serverid);
            DBQuery insert = DBQuery.InsertInto(TzAccount.ClientServer.Table).Fields(
                TzAccount.ClientServer.ClientID.Name,
                TzAccount.ClientServer.ServerID.Name).Values(
                dbClientID,
                dbServerID
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

        public bool UpdateClientSever(string clientid, string serverid) {
            DBDatabase db;
            db = base.Database;
            DBConst dbClientID = DBConst.String(clientid);
            DBConst dbServerID = DBConst.String(serverid);

            DBQuery update = DBQuery.Update(TzAccount.ClientServer.Table).Set(
               TzAccount.ClientServer.ServerID.Name, dbServerID
               ).WhereField(TzAccount.ClientServer.ClientID.Name, Compare.Equals, dbClientID);
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

            public DataTable GetServer(string clientid) {
                DBDatabase db;
                db = base.Database;
                DBQuery select;

                select = DBQuery.SelectAll(TzAccount.ClientServer.Table).From(TzAccount.ClientServer.Table)
                    .WhereField(TzAccount.ClientServer.Table, TzAccount.ClientServer.ClientID.Name, Compare.Equals, DBConst.String(clientid));
                return db.GetDatatable(select);
            }

        public bool Remove(string clientID, string serverID) {
            DBDatabase db;
            db = base.Database;            
            DBComparison client = DBComparison.Equal(DBField.Field(TzAccount.ClientServer.ClientID.Name ), DBConst.String(clientID));
            DBComparison server = DBComparison.Equal(DBField.Field(TzAccount.ClientServer.ServerID.Name), DBConst.String(serverID));
            DBQuery del = DBQuery.DeleteFrom(TzAccount.ClientServer.Table)
                                .WhereAll(client, server);
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
