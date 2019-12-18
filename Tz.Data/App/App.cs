
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Tech.Data;
using Tech.Data.Query;
using Tz.Global;
namespace Tz.Data.App
{
   public class App:DataBase
    {
        public App(string conn)
        {
            InitDbs(conn);
        }

        public DataTable GetApps(string clientid) {
            DBDatabase db;
            db = base.Database;
            DBQuery select;

            select = DBQuery.SelectAll(TzAccount.App.Table).From(TzAccount.App.Table)
                .WhereField(TzAccount.App.Table, TzAccount.App.ClientID.Name, Compare.Equals, DBConst.String(clientid));
            return db.GetDatatable(select);
        }
        public DataTable GetApp(string clientid,string appid)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;

            select = DBQuery.SelectAll(TzAccount.App.Table).From(TzAccount.App.Table)
                .WhereField(TzAccount.App.Table, TzAccount.App.ClientID.Name, Compare.Equals, DBConst.String(clientid))
                .AndWhere(TzAccount.App.Table, TzAccount.App.AppID.Name, Compare.Equals, DBConst.String(appid));
            return db.GetDatatable(select);
        }
        public DataTable GetAppComponent(string clientid, string appid)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.Select()
                 .Field(TzAccount.App.Table, TzAccount.App.AppID.Name)
                .Field(TzAccount.AppElements.Table, TzAccount.AppElements.CreatedOn.Name)
                 .Field(TzAccount.Component.Table, TzAccount.Component.ComponentID.Name)
                .Field(TzAccount.Component.Table, TzAccount.Component.ComponentName.Name)
                .Field(TzAccount.Component.Table, TzAccount.Component.Category.Name)
                .Field(TzAccount.Component.Table, TzAccount.Component.Title.Name)
                .Field(TzAccount.Component.Table, TzAccount.Component.TitleFormat.Name)
                .Field(TzAccount.Component.Table, TzAccount.Component.PrimaryKeys.Name)
                .Field(TzAccount.Component.Table, TzAccount.Component.ComponentType.Name)
                .Field(TzAccount.Component.Table, TzAccount.Component.TableID.Name)
                .Field(TzAccount.Component.Table, TzAccount.Component.IsGlobal.Name)
                 .From(TzAccount.App.Table).LeftJoin(TzAccount.AppElements.Table)
                .On(TzAccount.App.Table, TzAccount.App.AppID.Name,
                Compare.Equals, TzAccount.AppElements.Table, TzAccount.AppElements.AppID.Name)
                .LeftJoin(TzAccount.Component.Table).On(TzAccount.AppElements.Table, TzAccount.AppElements.ElementID.Name, Compare.Equals,
                TzAccount.Component.Table, TzAccount.Component.ComponentID.Name)
                .WhereField(TzAccount.App.Table, TzAccount.App.ClientID.Name,
               Compare.Equals, DBConst.String(clientid))
               .AndWhere(TzAccount.App.Table, TzAccount.App.AppID.Name, Compare.Equals, DBConst.String(appid))
               .AndWhere(TzAccount.AppElements.Table, TzAccount.AppElements.ElementType.Name, Compare.Equals, DBConst.String("1"))
               .OrderBy(TzAccount.Component.Table, TzAccount.Component.ComponentName.Name , Order.Ascending);
            return db.GetDatatable(select);
        }
        public DataTable GetAppForm(string clientid, string appid) {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.Select()
                ;
                   return db.GetDatatable(select);
        }
        public DataTable GetAppFeature(string clientid, string appid)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.Select()
                ;
            return db.GetDatatable(select);
        }
        public DataTable GetAppReport(string clientid, string appid)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.Select()
                ;
            return db.GetDatatable(select);
        }
        public DataTable GetAppPage(string clientid, string appid)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.Select()
                ;
            return db.GetDatatable(select);
        }
        public DataTable GetAppDashboard(string clientid, string appid)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.Select()
                ;
            return db.GetDatatable(select);
        }
        public string Save(string clientid,
            string appName,
            string description,
            string category
            )
        {
            DBDatabase db;
            db = base.Database;
            string a = Shared.generateID();
            DBQuery insert = DBQuery.InsertInto(TzAccount.App.Table).Fields(
              TzAccount.App.ClientID.Name,
              TzAccount.App.AppID.Name,
              TzAccount.App.Name.Name,
              TzAccount.App.Description.Name,
               TzAccount.App.Category.Name, 
              TzAccount.App.CreatedOn.Name)
              .Values(
              DBConst.String(clientid),
               DBConst.String(a),
               DBConst.String(appName),
               DBConst.String(description),
               DBConst.String(category),
               DBConst.DateTime(DateTime.Now));
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
        public bool Update(string clientid,
            string appid,
            string appName,
            string description,
            string category) {
            DBDatabase db;
            db = base.Database;
            DBComparison client = DBComparison.Equal(DBField.Field(TzAccount.App.ClientID.Name), DBConst.String(clientid));
            DBComparison app = DBComparison.Equal(DBField.Field(TzAccount.App.AppID.Name), DBConst.String(appid));

            DBQuery update = DBQuery.Update(TzAccount.App.Table).Set(
            TzAccount.App.Name.Name, DBConst.String(appName)
            ).Set(
            TzAccount.App.Description.Name, DBConst.String(description)
            )
            //.Set(
            //TzAccount.Component.PrimaryKeys.Name, dbprimarykeys
            //)
            .Set(
            TzAccount.App.Category.Name, DBConst.String(category)
            ).WhereAll(client, app);

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
        public bool Remove(string clientID,
           string appid)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbClientID = DBConst.String(clientID);
            DBConst dbappid = DBConst.String(appid);
            DBComparison client = DBComparison.Equal(DBField.Field(TzAccount.App.ClientID.Name), dbClientID);
            DBComparison app = DBComparison.Equal(DBField.Field(TzAccount.App.AppID.Name), dbappid);

            DBQuery del = DBQuery.DeleteFrom(TzAccount.App.Table)
                                .WhereAll(client, app);
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
        public bool AssignElement(string clientid, string appid, int elementtype, string elementid) {
            DBDatabase db;
            db = base.Database;
            DBQuery insert = DBQuery.InsertInto(TzAccount.AppElements.Table).Fields(
              TzAccount.AppElements.ClientID.Name,
              TzAccount.AppElements.AppID.Name,
              TzAccount.AppElements.ElementID.Name,
              TzAccount.AppElements.ElementType.Name,
              TzAccount.AppElements.CreatedOn.Name)
              .Values(
              DBConst.String(clientid),
               DBConst.String(appid),
               DBConst.String(elementid),
               DBConst.Int32(elementtype),
               DBConst.DateTime(DateTime.Now));
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
        public bool RemoveAppComponent(string clientID, string appid, string elementid) {
            DBDatabase db;
            db = base.Database;
            DBConst dbClientID = DBConst.String(clientID);
            DBConst dbappid = DBConst.String(appid);
            DBConst dbelement = DBConst.String(elementid);
            DBComparison client = DBComparison.Equal(DBField.Field(TzAccount.AppElements.ClientID.Name), dbClientID);
            DBComparison app = DBComparison.Equal(DBField.Field(TzAccount.AppElements.AppID.Name), dbappid);
            DBComparison element = DBComparison.Equal(DBField.Field(TzAccount.AppElements.ElementID.Name), dbelement);

            DBQuery del = DBQuery.DeleteFrom(TzAccount.AppElements.Table)
                                .WhereAll(client, app, element);
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
