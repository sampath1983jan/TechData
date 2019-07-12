using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Tech.Data;
using Tech.Data.Query;

namespace Tz.Data.Component
{
    public class ComponentModal:DataBase
    {
        public ComponentModal(string conn) {
            InitDbs(conn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="modalID"></param>
        /// <returns></returns>
        public DataTable GetModal(string clientID,string modalID)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.Select()
                .Field(TzAccount.ComponentModal.Table,TzAccount.ComponentModal.ClientID.Name)
                .Field(TzAccount.ComponentModal.Table, TzAccount.ComponentModal.ComponentModalID.Name)
                .Field(TzAccount.ComponentModal.Table, TzAccount.ComponentModal.ParentComponent.Name)
                .Field(TzAccount.ComponentModal.Table, TzAccount.ComponentModal.Name.Name)
                .Field(TzAccount.ComponentModal.Table, TzAccount.ComponentModal.Catgory.Name)
                .Field(TzAccount.ComponentModalItem.Table, TzAccount.ComponentModalItem.ComponentModalID.Name)
                .Field(TzAccount.ComponentModalItem.Table, TzAccount.ComponentModalItem.ChildComponentID.Name)
                .Field(TzAccount.ComponentModalItem.Table, TzAccount.ComponentModalItem.ComponentID.Name)
                .Field(TzAccount.ComponentModalItem.Table, TzAccount.ComponentModalItem.ComponentModalItemID.Name)
                .Field(TzAccount.ComponentModalItem.Table, TzAccount.ComponentModalItem.ClientID.Name)
                .Field(TzAccount.ComponentModalRelation.Table, TzAccount.ComponentModalRelation.ParentField.Name)
                .Field(TzAccount.ComponentModalRelation.Table, TzAccount.ComponentModalRelation.RelatedField.Name)
                .From(TzAccount.ComponentModal.Table).LeftJoin(TzAccount.ComponentModalItem.Table)
                .On(TzAccount.ComponentModal.Table,TzAccount.ComponentModal.ComponentModalID.Name,
                Compare.Equals,TzAccount.ComponentModalItem.Table,
                TzAccount.ComponentModalItem.ComponentModalID.Name)
                .LeftJoin(TzAccount.ComponentModalRelation.Table)
                .On(TzAccount.ComponentModalRelation.Table,TzAccount.ComponentModalItem.ComponentModalItemID.Name,
                Compare.Equals, TzAccount.ComponentModalRelation.Table, 
                TzAccount.ComponentModalRelation.ComponentModalItemID.Name)
               .WhereField(TzAccount.ComponentModal.Table, TzAccount.ComponentModal.ComponentModalID.Name,
               Compare.Equals, DBConst.String(modalID))
               .AndWhere(TzAccount.ComponentModal.Table, TzAccount.ComponentModal.ClientID.Name, 
               Compare.Equals, DBConst.String(clientID));
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public DataTable GetAllModal(string clientID) {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.Select()
                .Field(TzAccount.ComponentModal.Table, TzAccount.ComponentModal.ClientID.Name)
                .Field(TzAccount.ComponentModal.Table, TzAccount.ComponentModal.ComponentModalID.Name)
                .Field(TzAccount.ComponentModal.Table, TzAccount.ComponentModal.ParentComponent.Name)
                .Field(TzAccount.ComponentModal.Table, TzAccount.ComponentModal.Name.Name)
                .Field(TzAccount.ComponentModal.Table, TzAccount.ComponentModal.Catgory.Name)
                .Field(TzAccount.ComponentModalItem.Table, TzAccount.ComponentModalItem.ComponentModalID.Name)
                .Field(TzAccount.ComponentModalItem.Table, TzAccount.ComponentModalItem.ChildComponentID.Name)
                .Field(TzAccount.ComponentModalItem.Table, TzAccount.ComponentModalItem.ComponentID.Name)
                .Field(TzAccount.ComponentModalItem.Table, TzAccount.ComponentModalItem.ComponentModalItemID.Name)
                .Field(TzAccount.ComponentModalItem.Table, TzAccount.ComponentModalItem.ClientID.Name)
                .Field(TzAccount.ComponentModalRelation.Table, TzAccount.ComponentModalRelation.ParentField.Name)
                .Field(TzAccount.ComponentModalRelation.Table, TzAccount.ComponentModalRelation.RelatedField.Name)
                .From(TzAccount.ComponentModal.Table).LeftJoin(TzAccount.ComponentModalItem.Table)
                .On(TzAccount.ComponentModal.Table, TzAccount.ComponentModal.ComponentModalID.Name,
                Compare.Equals, TzAccount.ComponentModalItem.Table,
                TzAccount.ComponentModalItem.ComponentModalID.Name)
                .LeftJoin(TzAccount.ComponentModalRelation.Table)
                .On(TzAccount.ComponentModalRelation.Table, TzAccount.ComponentModalItem.ComponentModalItemID.Name,
                Compare.Equals, TzAccount.ComponentModalRelation.Table,
                TzAccount.ComponentModalRelation.ComponentModalItemID.Name)
               .WhereField(TzAccount.ComponentModal.Table, TzAccount.ComponentModal.ClientID.Name,
               Compare.Equals, DBConst.String(clientID));
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="parentComponent"></param>
        /// <param name="name"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public string Save(string clientID,
            string parentComponent,
            string name,
            string category)
        {

            DBDatabase db;
            db = base.Database;
            string a = Shared.generateID();
            DBConst dbComponentModalID = DBConst.String(a);
            DBConst dbclientID = DBConst.String(clientID);
            DBConst dbparentComponent = DBConst.String(parentComponent);
            DBConst dbname = DBConst.String(name);
            DBConst dbcategory = DBConst.String(category);             
            DBQuery insert = DBQuery.InsertInto(TzAccount.ComponentModal.Table).Fields(
              TzAccount.ComponentModal.ClientID.Name,
              TzAccount.ComponentModal.ComponentModalID.Name,
              TzAccount.ComponentModal.ParentComponent.Name,
              TzAccount.ComponentModal.Name.Name,
              TzAccount.ComponentModal.Catgory.Name            
              )
              .Values(
              dbclientID,
              dbComponentModalID,
              dbparentComponent,
              dbname,
              dbcategory            
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
        /// <param name="clientID"></param>
        /// <param name="name"></param>
        /// <param name="category"></param>
        /// <param name="ComponentModalID"></param>
        /// <returns></returns>
        public bool Update(string clientID, string name,
            string category, string ComponentModalID)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbclientID = DBConst.String(clientID);
            DBConst dbname = DBConst.String(name);
            DBConst dbcategory = DBConst.String(category);
            DBConst dbComponentModalID = DBConst.String(ComponentModalID);
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.ComponentModal.ClientID.Name), dbclientID);
            DBComparison compModalID = DBComparison.Equal(DBField.Field(TzAccount.ComponentModal.ComponentModalID.Name), dbComponentModalID);

            DBQuery update = DBQuery.Update(TzAccount.ComponentModal.Table).Set(
            TzAccount.ComponentModal.Name.Name, dbname
            ).Set(
            TzAccount.ComponentModal.Catgory.Name, dbcategory
            ).WhereAll(compModalID, Client);

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
        /// <param name="componentModalID"></param>
        /// <param name="componentID"></param>
        /// <param name="childComponentID"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public string SaveModalItem(string clientid,
            string componentModalID,            
            string componentID,
            string childComponentID          
            ) {

            DBDatabase db;
            db = base.Database;
            string a = Shared.generateID();            
            DBConst dbComponentModalItemID = DBConst.String(a);
            DBConst dbComponentModalID = DBConst.String(componentModalID);
            DBConst dbclientID = DBConst.String(clientid);
            DBConst dbcomponentID = DBConst.String(componentID);
            DBConst dbchildComponentID = DBConst.String(childComponentID);            

            DBQuery insert = DBQuery.InsertInto(TzAccount.ComponentModalItem.Table).Fields(
              TzAccount.ComponentModalItem.ClientID.Name,
              TzAccount.ComponentModalItem.ComponentModalID.Name,
              TzAccount.ComponentModalItem.ComponentID.Name,
              TzAccount.ComponentModalItem.ChildComponentID.Name,
              TzAccount.ComponentModalItem.ComponentModalItemID.Name
              //TzAccount.ComponentModalItem.Depth.Name,
              //TzAccount.ComponentModalItem.Right.Name
              )
              .Values(
              dbclientID,
              dbComponentModalID,
              dbcomponentID,
              dbchildComponentID,
              dbComponentModalItemID
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

        public bool RemoveModalItem(string clientid,
            string componentModalID,
            string ComponentModalItemID)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbclientID = DBConst.String(clientid);
            DBConst dbComponentModalID = DBConst.String(componentModalID);
            DBConst dbComponentModalItemID = DBConst.String(ComponentModalItemID);
            
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.ComponentModalItem.ClientID.Name), dbclientID);
            DBComparison compModalID = DBComparison.Equal(DBField.Field(TzAccount.ComponentModalItem.ComponentModalID.Name), dbComponentModalID);
            DBComparison childComponent = DBComparison.Equal(DBField.Field(TzAccount.ComponentModalItem.ComponentModalID.Name), dbComponentModalItemID);
            
            DBQuery del = DBQuery.DeleteFrom(TzAccount.ComponentModalItem.Table)
                                .WhereAll(Client, compModalID, childComponent);
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

        public bool RemoveAllModalItem(string clientid,
           string componentModalID)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbclientID = DBConst.String(clientid);
            DBConst dbComponentModalID = DBConst.String(componentModalID);
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.ComponentModalItem.ClientID.Name), dbclientID);
            DBComparison compModalID = DBComparison.Equal(DBField.Field(TzAccount.ComponentModalItem.ComponentModalID.Name), dbComponentModalID);
            DBQuery del = DBQuery.DeleteFrom(TzAccount.ComponentModalItem.Table)
                                .WhereAll(Client, compModalID);
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
        /// <param name="componentModalID"></param>
        /// <returns></returns>
        public bool Remove(string clientid, string componentModalID)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbclientID = DBConst.String(clientid);
            DBConst dbComponentModalID = DBConst.String(componentModalID);
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.ComponentModal.ClientID.Name), dbclientID);
            DBComparison compModalID = DBComparison.Equal(DBField.Field(TzAccount.ComponentModal.ComponentModalID.Name), dbComponentModalID);
            DBQuery del = DBQuery.DeleteFrom(TzAccount.ComponentModal.Table)
                                .WhereAll(Client, compModalID);
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
        /// <param name="componentModalItemID"></param>
        /// <returns></returns>
        public bool RemoveAllItemRelation(string clientid,
           string componentModalItemID)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbclientID = DBConst.String(clientid);
            DBConst dbcomponentModalItemID = DBConst.String(componentModalItemID);

            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.ComponentModalRelation.ClientID.Name), dbclientID);
            DBComparison componentModalItem = DBComparison.In(DBField.Field(TzAccount.ComponentModalRelation.ComponentModalItemID.Name), dbcomponentModalItemID);


            DBQuery del = DBQuery.DeleteFrom(TzAccount.ComponentModalItem.Table)
                                .WhereAll(Client, componentModalItem);
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
        /// <param name="componentitemrelationid"></param>
        /// <returns></returns>
        public bool RemoveItemRelation(string clientid,
           string componentitemrelationid)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbclientID = DBConst.String(clientid);
            DBConst dbcomponentitemrelationid = DBConst.String(componentitemrelationid);

            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.ComponentModalRelation.ClientID.Name), dbclientID);
            DBComparison compModalID = DBComparison.Equal(DBField.Field(TzAccount.ComponentModalRelation.ComponentModalRelationID.Name), dbcomponentitemrelationid);


            DBQuery del = DBQuery.DeleteFrom(TzAccount.ComponentModalItem.Table)
                                .WhereAll(Client, compModalID);
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
        /// <param name="componentmodalitemid"></param>
        /// <param name="parentfield"></param>
        /// <param name="childfield"></param>
        /// <returns></returns>
        public bool SaveItemRelation(string clientid,
            string componentmodalitemid,
            string parentfield,
            string childfield
            )
        {
            DBDatabase db;
            db = base.Database;
            string a = Shared.generateID();
            DBConst dbcompoenntitemrelationid = DBConst.String(a);
            DBConst dbclientID = DBConst.String(clientid);
            DBConst dbparentfield = DBConst.String(parentfield);
            DBConst dbrelated = DBConst.String(childfield);
            DBConst dbcomponentmodalitemid = DBConst.String(componentmodalitemid);
            DBQuery insert = DBQuery.InsertInto(TzAccount.ComponentModalRelation.Table).Fields(
              TzAccount.ComponentModalRelation.ClientID.Name,
              TzAccount.ComponentModalRelation.ComponentModalItemID.Name,
              TzAccount.ComponentModalRelation.ComponentModalRelationID.Name,
              TzAccount.ComponentModalRelation.ParentField.Name,
              TzAccount.ComponentModalRelation.RelatedField.Name
              )
              .Values(
              dbclientID,
              dbcomponentmodalitemid,
              dbcompoenntitemrelationid,
              dbparentfield,
              dbrelated              
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="compoenntitemrelationid"></param>
        /// <param name="componentmodalitemid"></param>
        /// <param name="parentfield"></param>
        /// <param name="childfield"></param>
        /// <returns></returns>
        public bool UpdateItemRelation(string clientid,
          string compoenntitemrelationid,
          string componentmodalitemid,
          string parentfield,
          string childfield
          )
        {
            DBDatabase db;
            db = base.Database;           
            DBConst dbcompoenntitemrelationid = DBConst.String(compoenntitemrelationid);
            DBConst dbclientID = DBConst.String(clientid);
            DBConst dbparentfield = DBConst.String(parentfield);
            DBConst dbrelated = DBConst.String(childfield);
            DBConst dbcomponentmodalitemid = DBConst.String(componentmodalitemid);



            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.ComponentModalRelation.ClientID.Name),
                dbclientID);
            DBComparison compModalID = DBComparison.Equal(DBField.Field(TzAccount.ComponentModalRelation.ComponentModalRelationID.Name),
                dbcompoenntitemrelationid);

            DBComparison componentmodalitem = DBComparison.Equal(DBField.Field(TzAccount.ComponentModalRelation.ComponentModalRelationID.Name),
             dbcomponentmodalitemid);

            DBQuery update = DBQuery.Update(TzAccount.ComponentModalRelation.Table).Set(
            TzAccount.ComponentModalRelation.ParentField.Name, dbparentfield
            ).Set(
            TzAccount.ComponentModalRelation.RelatedField.Name, dbrelated
            ).WhereAll(compModalID, Client, componentmodalitem);

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

       


    }
}
