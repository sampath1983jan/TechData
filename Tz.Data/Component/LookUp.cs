using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Tech.Data;
using Tech.Data.Query;

namespace Tz.Data.Component
{
    public class LookUp : DataBase
    {
        public LookUp(string conn){
            InitDbs(conn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public DataTable GetLookupWithItems(string clientID)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.Select()
                .Field(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.ClientID.Name)
                .Field(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.LookUpName.Name)
                .Field(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.LookupID.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.ComponentLookupItemID.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.Label.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.ShortLabel.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.Description.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.ParentID.Name)
                       .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.Order.Name)
                       .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.IsActive.Name)
                .From(TzAccount.ComponentLookUp.Table).LeftJoin(TzAccount.ComponentLookUpItem.Table)
                .On(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.LookupID.Name,
                Compare.Equals, TzAccount.ComponentLookUpItem.Table,
                TzAccount.ComponentLookUpItem.LookupID.Name)
               .WhereField(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.ClientID.Name,
               Compare.Equals, DBConst.String(clientID));            
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public DataTable GetLookUpList(string clientID)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.Select()
                .Field(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.ClientID.Name)
                .Field(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.LookUpName.Name)
                .Field(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.LookupID.Name)                
                .From(TzAccount.ComponentLookUp.Table)
               .WhereField(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.ClientID.Name,
               Compare.Equals, DBConst.String(clientID)).OrderBy(TzAccount.ComponentLookUp.LookUpName.Name,Order.Ascending);
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="lookupid"></param>
        /// <returns></returns>
        public DataTable GetChildLookup(string clientID, string parentID) {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.Select()
                .Field(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.ClientID.Name)
                .Field(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.LookUpName.Name)
                .Field(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.LookupID.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.ComponentLookupItemID.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.Label.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.ShortLabel.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.Description.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.ParentID.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.Order.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.IsActive.Name)
                .From(TzAccount.ComponentLookUp.Table).LeftJoin(TzAccount.ComponentLookUpItem.Table)
                .On(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.LookupID.Name,
                Compare.Equals, TzAccount.ComponentLookUpItem.Table,
                TzAccount.ComponentLookUpItem.LookupID.Name)
               .WhereField(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.ClientID.Name,
               Compare.Equals, DBConst.String(clientID))
               .AndWhere(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.ParentID.Name,
               Compare.Equals, DBConst.String(parentID)); ;
            return db.GetDatatable(select);
        }
        public DataTable GetLookupItems(string clientID, string lookupid)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.Select()
                .Field(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.ClientID.Name)
                .Field(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.LookUpName.Name)
                .Field(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.LookupID.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.ComponentLookupItemID.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.Label.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.ShortLabel.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.Description.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.ParentID.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.Order.Name)
                .Field(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.IsActive.Name)
                .From(TzAccount.ComponentLookUp.Table).LeftJoin(TzAccount.ComponentLookUpItem.Table)
                .On(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.LookupID.Name,
                Compare.Equals, TzAccount.ComponentLookUpItem.Table,
                TzAccount.ComponentLookUpItem.LookupID.Name)
               .WhereField(TzAccount.ComponentLookUp.Table, TzAccount.ComponentLookUp.ClientID.Name,
               Compare.Equals, DBConst.String(clientID))
               .AndWhere(TzAccount.ComponentLookUpItem.Table, TzAccount.ComponentLookUpItem.LookupID.Name,
               Compare.Equals, DBConst.String(lookupid)).OrderBy(TzAccount.ComponentLookUpItem.Order.Name, Order.Ascending) ;
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="label"></param>
        /// <param name="shortlabel"></param>
        /// <param name="description"></param>
        /// <param name="parentid"></param>
        /// <param name="lookupid"></param>
        /// <param name="order"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public string Save(string clientid,
            string label,
            string shortlabel,
            string description,
            string parentid,
            string lookupid,
            int order,  
            bool isActive
            )
        {

            DBDatabase db;
            db = base.Database;
            string a = Shared.generateID();
            DBConst dbComponentLookupID = DBConst.String(a);
            DBConst dbclientID = DBConst.String(clientid);
            DBConst dbparentid = DBConst.String(parentid);
            DBConst dblabel = DBConst.String(label);
            DBConst dbshortlabel = DBConst.String(shortlabel);          
            DBConst dbcategory = DBConst.String(lookupid);
            DBConst dborder = DBConst.Int32(order);
            DBConst dbisactive = DBConst.Const(DbType.Boolean, isActive); ;
            DBQuery insert = DBQuery.InsertInto(TzAccount.ComponentLookUpItem.Table).Fields(
              TzAccount.ComponentLookUpItem.ClientID.Name,
              TzAccount.ComponentLookUpItem.ComponentLookupItemID.Name,
              TzAccount.ComponentLookUpItem.ParentID.Name,
              TzAccount.ComponentLookUpItem.Label.Name,
              TzAccount.ComponentLookUpItem.ShortLabel.Name,
              TzAccount.ComponentLookUpItem.LookupID.Name,
               TzAccount.ComponentLookUpItem.Order.Name,
               TzAccount.ComponentLookUpItem.IsActive.Name
              )
              .Values(
                dbclientID,
              dbComponentLookupID,
              dbparentid,
              dblabel,
              dbshortlabel,              
              dbcategory,
              dborder,
              dbisactive
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
        /// <param name="componentlookupid"></param>
        /// <param name="lookupid"></param>
        /// <param name="label"></param>
        /// <param name="shortlabel"></param>
        /// <param name="description"></param>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public bool Update(string clientid,
            string componentlookupid,
            string lookupid,
              string label,
            string shortlabel,
            string description,                        
            string parentid
            )
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbComponentLookupID = DBConst.String(componentlookupid);
            DBConst dbclientID = DBConst.String(clientid);
            DBConst dbparentid = DBConst.String(parentid);
            DBConst dblabel = DBConst.String(label);
            DBConst dbshortlabel = DBConst.String(shortlabel);
            DBConst dbdescription = DBConst.String(description);
            DBConst dbcategoryid = DBConst.String(lookupid);
            DBComparison category = DBComparison.Equal(DBField.Field(TzAccount.ComponentLookUpItem.LookupID.Name), dbcategoryid);
            DBComparison componentlookup = DBComparison.Equal(DBField.Field(TzAccount.ComponentLookUpItem.ComponentLookupItemID.Name), dbComponentLookupID);
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.ComponentLookUpItem.ClientID.Name), dbclientID);
            DBQuery update = DBQuery.Update(TzAccount.ComponentLookUpItem.Table).Set(
        TzAccount.ComponentLookUpItem.ParentID.Name, dbparentid
        ).Set(
        TzAccount.ComponentLookUpItem.Label.Name, dblabel
        ).Set(
        TzAccount.ComponentLookUpItem.ShortLabel.Name, dbshortlabel
        ).Set(
        TzAccount.ComponentLookUpItem.Description.Name, dbdescription
        ).WhereAll(componentlookup, category, Client);
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
        public bool UpdateOrder(string clientid,
            string componentlookupid,
            int order)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbComponentLookupID = DBConst.String(componentlookupid);
            DBConst dbclientID = DBConst.String(clientid);
            DBConst dborder= DBConst.Const(DbType.Int32, order);
            
            DBComparison componentlookup = DBComparison.Equal(DBField.Field(TzAccount.ComponentLookUpItem.ComponentLookupItemID.Name), dbComponentLookupID);
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.ComponentLookUpItem.ClientID.Name), dbclientID);
            DBQuery update = DBQuery.Update(TzAccount.ComponentLookUpItem.Table).Set(
        TzAccount.ComponentLookUpItem.Order.Name, dborder
        ).WhereAll(componentlookup,  Client);
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
        /// <param name="componentlookupid"></param>
        /// <param name="isactive"></param>
        /// <returns></returns>
        public bool UpateActive(string clientid,
            string componentlookupid,
            bool isactive)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbComponentLookupID = DBConst.String(componentlookupid);
            DBConst dbclientID = DBConst.String(clientid);
            DBConst dbisactive = DBConst.Const(DbType.Boolean,isactive);         
            
            DBComparison componentlookup = DBComparison.Equal(DBField.Field(TzAccount.ComponentLookUpItem.ComponentLookupItemID.Name), dbComponentLookupID);
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.ComponentLookUpItem.ClientID.Name), dbclientID);
            DBQuery update = DBQuery.Update(TzAccount.ComponentLookUpItem.Table).Set(
        TzAccount.ComponentLookUpItem.IsActive.Name, dbisactive
        ).WhereAll(componentlookup,  Client);
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
        /// <param name="lookupid"></param>
        /// <returns></returns>
        public bool RemoveLookup(string clientid,
            string lookupid)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbcomponentlookupid = DBConst.String(lookupid);            
            DBConst dbclientid = DBConst.String(clientid);            
            DBComparison component = DBComparison.Equal(DBField.Field(TzAccount.ComponentLookUp.LookupID.Name), dbcomponentlookupid);
            DBComparison client = DBComparison.Equal(DBField.Field(TzAccount.ComponentLookUp.ClientID.Name), dbclientid);

            DBQuery del = DBQuery.DeleteFrom(TzAccount.ComponentLookUp.Table)
                                .WhereAll( component, client);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="lookupid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool UpdateLookup(string clientid,
            string lookupid,
            string name)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbComponentLookupID = DBConst.String(lookupid);
            DBConst dbclientID = DBConst.String(clientid);
            DBConst dbparentid = DBConst.String(name);
                     
            DBComparison componentlookup = DBComparison.Equal(DBField.Field(TzAccount.ComponentLookUp.LookupID.Name), dbComponentLookupID);
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.ComponentLookUp.ClientID.Name), dbclientID);

            DBQuery update = DBQuery.Update(TzAccount.ComponentLookUp.Table).Set(
        TzAccount.ComponentLookUp.LookUpName.Name, dbparentid
        ).WhereAll(componentlookup,  Client);
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
        /// /
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string SaveLookup(string clientid,
            string name) {
            DBDatabase db;
            db = base.Database;
            string a = Shared.generateID();
            DBConst dbComponentLookupID = DBConst.String(a);
            DBConst dbclientID = DBConst.String(clientid);
            DBConst dbname = DBConst.String(name);
           
            DBQuery insert = DBQuery.InsertInto(TzAccount.ComponentLookUp.Table).Fields(
              TzAccount.ComponentLookUp.ClientID.Name,
              TzAccount.ComponentLookUp.LookupID.Name,
              TzAccount.ComponentLookUp.LookUpName.Name            
              )
              .Values(
              dbclientID,
              dbComponentLookupID,
              dbname             
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
        /// <param name="componentlookupid"></param>
        /// <param name="categoryid"></param>
        /// <returns></returns>
        public bool RemoveLookupItems(string clientid,
            string componentlookupid,
            string categoryid)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbcomponentlookupid = DBConst.String(componentlookupid);
            DBConst dbcategoryid = DBConst.String(categoryid);
            DBConst dbclientid = DBConst.String(clientid);
            DBComparison category = DBComparison.Equal(DBField.Field(TzAccount.ComponentLookUpItem.LookupID.Name), dbcategoryid);
            DBComparison component = DBComparison.Equal(DBField.Field(TzAccount.ComponentLookUpItem.ComponentLookupItemID.Name), dbcomponentlookupid);
            DBComparison client = DBComparison.Equal(DBField.Field(TzAccount.ComponentLookUpItem.ClientID.Name), dbclientid);

            DBQuery del = DBQuery.DeleteFrom(TzAccount.ComponentLookUpItem.Table)
                                .WhereAll(category, component, client);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="componentlookupid"></param>
        /// <param name="categoryid"></param>
        /// <returns></returns>
        public bool RemoveAllLookupItems(string clientid,
            string componentlookupid
            )
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbcomponentlookupid = DBConst.String(componentlookupid);            
            DBConst dbclientid = DBConst.String(clientid);
            
            DBComparison component = DBComparison.Equal(DBField.Field(TzAccount.ComponentLookUpItem.ComponentLookupItemID.Name), dbcomponentlookupid);
            DBComparison client = DBComparison.Equal(DBField.Field(TzAccount.ComponentLookUpItem.ClientID.Name), dbclientid);

            DBQuery del = DBQuery.DeleteFrom(TzAccount.ComponentLookUpItem.Table)
                                .WhereAll( component, client);
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
