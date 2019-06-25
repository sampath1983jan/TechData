
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Tech.Data;
using Tech.Data.Query;

namespace Tz.Data.Component
{
   public class Component:DataBase
    {
        public Component() {
            InitDbs("");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public DataTable GetCompnents(string clientid)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;

            select = DBQuery.SelectAll(TzAccount.Component.Table).From(TzAccount.Component.Table)
                .WhereField(TzAccount.Component.Table, TzAccount.Component.ClientID.Name, Compare.Equals, DBConst.String(clientid));
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public DataTable GetCompnentWithFields(string clientid)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            
            select = DBQuery.SelectAll(TzAccount.Component.Table).From(TzAccount.Component.Table)
                .WhereField(TzAccount.Component.Table, TzAccount.Component.ClientID.Name, Compare.Equals, DBConst.String(clientid));
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="componentid"></param>
        /// <returns></returns>
        public DataTable GetComponent(string clientid,
            string componentid) {
            DBDatabase db;
            db = base.Database;
            DBQuery select;

            select = DBQuery.SelectAll(TzAccount.Component.Table).From(TzAccount.Component.Table)
                .WhereField(TzAccount.Component.Table, TzAccount.Component.ClientID.Name, Compare.Equals, DBConst.String(clientid))
                .AndWhere(TzAccount.Component.Table, TzAccount.Component.ComponentID.Name,Compare.Equals,DBConst.String(componentid));
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="componentName"></param>
        /// <param name="componenttype"></param>
        /// <param name="tableid"></param>
        /// <param name="title"></param>
        /// <param name="primarykeys"></param>
        /// <param name="titleformat"></param>
        /// <param name="newlayout"></param>
        /// <param name="readlayout"></param>
        /// <returns></returns>
        public string SaveComponent(string clientid,            
            string componentName,
            int componenttype,
            string tableid,
            string title,
            string primarykeys,
            string titleformat,
            string newlayout,
            string readlayout
            )
        {
            DBDatabase db;
            db = base.Database;
            string a = Shared.generateID();
            DBConst dbcomponentid = DBConst.String(a);
            DBConst dbclientid = DBConst.String(clientid);
            DBConst dbcomponentName = DBConst.String(componentName);
            DBConst dbcomponenttype = DBConst.Int32(componenttype);
            DBConst dbtableid = DBConst.String(tableid);
            DBConst dbtitle = DBConst.String(title);
            DBConst dbprimarykeys = DBConst.String(primarykeys);
            DBConst dbtitleformat = DBConst.String(titleformat);
            DBConst dbnewlayout = DBConst.String(newlayout);
            DBConst dbreadlayout = DBConst.String(readlayout);

            DBQuery insert = DBQuery.InsertInto(TzAccount.Component.Table).Fields(
              TzAccount.Component.ComponentID.Name,
              TzAccount.Component.ComponentName.Name,
              TzAccount.Component.ComponentType.Name,
              TzAccount.Component.Title.Name,
              TzAccount.Component.TitleFormat.Name,
              TzAccount.Component.PrimaryKeys.Name,
              TzAccount.Component.NewLayout.Name,
              TzAccount.Component.ReadLayout.Name,
              TzAccount.Component.TableID.Name,
              TzAccount.Component.ClientID .Name 
              )
              .Values(
              dbcomponentid,
              dbcomponentName,
              dbcomponenttype,
              dbtitle,
              dbtitleformat,
              dbprimarykeys,
              dbnewlayout,
              dbreadlayout,
              dbtableid,
              dbclientid
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
        /// <param name="clientid"></param>
        /// <param name="componentID"></param>
        /// <param name="componentName"></param>
        /// <param name="componenttype"></param>
        /// <param name="tableid"></param>
        /// <param name="title"></param>
        /// <param name="primarykeys"></param>
        /// <param name="titleformat"></param>
        /// <param name="newlayout"></param>
        /// <param name="readlayout"></param>
        /// <returns></returns>
        public bool UpdateComponent(string clientid,string componentID,
            string componentName,
            int componenttype,
            string tableid,
            string title,
            string primarykeys,
            string titleformat,
            string newlayout,
            string readlayout
            )
        {
            DBDatabase db;
            db = base.Database;            
            DBConst dbcomponentid = DBConst.String(componentID);
            DBConst dbcomponentName = DBConst.String(componentName);
            DBConst dbcomponenttype = DBConst.Int32(componenttype);
            DBConst dbtableid = DBConst.String(tableid);
            DBConst dbtitle = DBConst.String(title);
            DBConst dbprimarykeys = DBConst.String(primarykeys);
            DBConst dbtitleformat = DBConst.String(titleformat);
            DBConst dbnewlayout = DBConst.String(newlayout);
            DBConst dbreadlayout = DBConst.String(readlayout);
            DBComparison componentid = DBComparison.Equal(DBField.Field(TzAccount.Component.ComponentID.Name), dbcomponentid);

            DBQuery update = DBQuery.Update(TzAccount.Component.Table).Set(
            TzAccount.Component.ComponentName.Name, dbcomponentName
            ).Set(
            TzAccount.Component.PrimaryKeys.Name, dbprimarykeys
            ).Set(
            TzAccount.Component.Title.Name, dbtitle
            ).Set(
            TzAccount.Component.TitleFormat.Name, dbtitleformat
            ).Set(
            TzAccount.Component.NewLayout.Name, dbnewlayout
            ).Set(
            TzAccount.Component.ReadLayout.Name, dbreadlayout
            ).WhereAll(componentid);              
         
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
            string componentID)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbClientID = DBConst.String(clientID);
            DBConst dbcomponentID = DBConst.String(componentID);
            DBComparison client = DBComparison.Equal(DBField.Field(TzAccount.Component.ClientID.Name), dbClientID);
            DBComparison component = DBComparison.Equal(DBField.Field(TzAccount.Component.ComponentID.Name), dbcomponentID);

            DBQuery del = DBQuery.DeleteFrom(TzAccount.Component.Table)                                
                                .WhereAll(client,component);
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
