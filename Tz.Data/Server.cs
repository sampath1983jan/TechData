using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data;
using System.Data;
using Tech.Data.Query;
using System.Data.Common;

namespace Tz.Data
{
    public class Server:DataBase
    {
        DBDatabase db;
        public Server() {
            InitDbs("");
            db = base.Database;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverid"></param>
        /// <returns></returns>
        public DataTable GetServer(string serverid) {
            DBDatabase db;
            db = base.Database;
            DBQuery select;            
            select = DBQuery.SelectAll(TzAccount.Server.Table).From(TzAccount.Server.Table)
                .WhereField(TzAccount.Server.Table, TzAccount.Server.ServerID.Name, Compare.Equals, DBConst.String(serverid));
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="dbname"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public string Save(string host,
            string dbname,
            string userName,
            string password,
            int port
            )
        {
            DBDatabase db;
            db = base.Database;
            string a = Shared.generateID();
            DBConst dbServerID = DBConst.String(a);
            DBConst dbHostName = DBConst.String(host);
            DBConst dbDBname = DBConst.String(dbname);
            DBConst dbPassword = DBConst.String(password);
            DBConst dbUserName = DBConst.String(password);
            DBConst dbPort = DBConst.Int32(port);

            DBQuery insert = DBQuery.InsertInto(TzAccount.Server.Table).Fields(
                TzAccount.Server.ServerID.Name,
               TzAccount.Server.Host.Name,
               TzAccount.Server.UserID.Name,
               TzAccount.Server.Password.Name,
               TzAccount.Server.Port.Name,
               TzAccount.Server.DB.Name).Values(
               dbServerID,
               dbHostName,
               dbUserName,
               dbPassword,
               dbPort,
               dbDBname
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
        /// <param name="serverid"></param>
        /// <param name="host"></param>
        /// <param name="dbname"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool Update(string serverid, string host,
            string dbname,
            string userName,
            string password,
            int port
            )
        {
                DBDatabase db;
                db = base.Database;            
                DBConst dbServerID = DBConst.String(serverid);
                DBConst dbHostName = DBConst.String(host);
                DBConst dbDBname = DBConst.String(dbname);
                DBConst dbPassword = DBConst.String(password);
                DBConst dbUserName = DBConst.String(userName);
                DBConst dbPort = DBConst.Int32(port);

                DBQuery update = DBQuery.Update(TzAccount.Server.Table).Set(
                    TzAccount.Server.Host.Name, dbHostName).Set(
                   TzAccount.Server.UserID.Name, dbUserName).Set(
                   TzAccount.Server.Password.Name, dbPassword).Set(
                   TzAccount.Server.Port.Name, dbPort).WhereField(TzAccount.Server.ServerID.Name, Compare.Equals, dbServerID);
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

        public bool Remove(string serverid) {
            DBDatabase db;
            db = base.Database;
            DBConst dbServerID = DBConst.String(serverid);

            DBQuery del = DBQuery.DeleteFrom(TzAccount.Server.Table)
                                .WhereFieldEquals(TzAccount.Server.ServerID.Name, dbServerID);
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
