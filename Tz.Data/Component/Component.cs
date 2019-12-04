
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Tech.Data;
using Tech.Data.Query;
using Tz.Global;
namespace Tz.Data.Component
{
   public class Component:DataBase
    {
        public Component(string conn) {
            InitDbs(conn);
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
            select = DBQuery.Select()
                .Field(TzAccount.Component.Table, TzAccount.Component.ComponentID.Name)
                .Field(TzAccount.Component.Table, TzAccount.Component.ComponentName.Name)
                .Field(TzAccount.Component.Table, TzAccount.Component.Category.Name)
                .Field(TzAccount.Component.Table, TzAccount.Component.Title.Name)
                .Field(TzAccount.Component.Table, TzAccount.Component.TitleFormat.Name)
                .Field(TzAccount.Component.Table, TzAccount.Component.PrimaryKeys.Name)
                .Field(TzAccount.Component.Table, TzAccount.Component.ComponentType.Name)
                .Field(TzAccount.Component.Table, TzAccount.Component.TableID.Name)
                .Field(TzAccount.Component.Table, TzAccount.Component.IsGlobal.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.FieldID.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.AttributeName.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.AttributeType.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.IsRequired.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.IsCore.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.IsUnique.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.IsAuto.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.IsSecured.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.IsReadOnly.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.FileExtension.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.LookUpID.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.RegExp.Name)                
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.DefaultValue.Name)
                .From(TzAccount.Component.Table).LeftJoin(TzAccount.ComponentAttribute.Table)
                .On(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.ComponentID.Name,
                Compare.Equals,TzAccount.Component.Table, TzAccount.Component.ComponentID.Name)
               .WhereField(TzAccount.Component.Table, TzAccount.Component.ClientID.Name, 
               Compare.Equals, DBConst.String(clientid));
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
            string readlayout,
            string category
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
            DBConst dbcategory = DBConst.String(category);
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
                  TzAccount.Component.ClientID .Name ,
                  TzAccount.Component.Category.Name,
                  TzAccount.Component.ComponentState.Name
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
                  dbclientid,
                  dbcategory,
                  DBConst.Int32(0) // pending
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

        public bool ChangeState(string clientid, string componentid,int state) {
            DBDatabase db;
            db = base.Database;
            DBComparison dbcompo = DBComparison.Equal(DBField.Field(TzAccount.Component.ComponentID.Name), DBConst.String(componentid));
            DBComparison dbclient = DBComparison.Equal(DBField.Field(TzAccount.Component.ClientID.Name), DBConst.String(clientid));
            DBQuery update = DBQuery.Update(TzAccount.Component.Table).Set(
            TzAccount.Component.ComponentState.Name, DBConst.Int32(state)
            ).WhereAll(dbcompo, dbclient);

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
            string title,
            string primarykeys,
            string titleformat,
            string newlayout,
            string readlayout,
            string category
            )
        {
            DBDatabase db;
            db = base.Database;            
            DBConst dbcomponentid = DBConst.String(componentID);
            DBConst dbcomponentName = DBConst.String(componentName);
            DBConst dbcomponenttype = DBConst.Int32(componenttype);
         //   DBConst dbtableid = DBConst.String(tableid);
            DBConst dbtitle = DBConst.String(title);
            DBConst dbprimarykeys = DBConst.String(primarykeys);
            DBConst dbtitleformat = DBConst.String(titleformat);
            DBConst dbnewlayout = DBConst.String(newlayout);
            DBConst dbreadlayout = DBConst.String(readlayout);
            DBConst dbcategory = DBConst.String(category);
            DBComparison componentid = DBComparison.Equal(DBField.Field(TzAccount.Component.ComponentID.Name), dbcomponentid);

            DBQuery update = DBQuery.Update(TzAccount.Component.Table).Set(
            TzAccount.Component.ComponentName.Name, dbcomponentName
            ).Set(
            TzAccount.Component.Category.Name, dbcategory
            )
            //.Set(
            //TzAccount.Component.PrimaryKeys.Name, dbprimarykeys
            //)
            .Set(
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
