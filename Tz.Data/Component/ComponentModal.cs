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
        public ComponentModal() {
            InitDbs("");
        }

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
                .Field(TzAccount.ComponentModalItem.Table, TzAccount.ComponentModalItem.ComponentModalID.Name)
                .Field(TzAccount.ComponentModalItem.Table, TzAccount.ComponentModalItem.ComponentModalID.Name)
                .Field(TzAccount.ComponentModalItem.Table, TzAccount.ComponentModalItem.ComponentModalID.Name)
                .Field(TzAccount.ComponentModalItem.Table, TzAccount.ComponentModalItem.ComponentModalID.Name)
                .From(TzAccount.ComponentModal.Table).LeftJoin(TzAccount.ComponentModalItem.Table)
                .On(TzAccount.ComponentModal.Table,TzAccount.ComponentModalItem.ComponentModalID.Name,
                Compare.Equals,TzAccount.ComponentModalItem.Table,TzAccount.ComponentModalItem.ComponentModalID.Name)
               .WhereField(TzAccount.ComponentModal.Table, TzAccount.ComponentModal.ComponentModalID.Name,
               Compare.Equals, DBConst.String(modalID))
               .AndWhere(TzAccount.ComponentModal.Table, TzAccount.ComponentModal.ClientID.Name, 
               Compare.Equals, DBConst.String(clientID));
            return db.GetDatatable(select);
        }

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
                .From(TzAccount.ComponentModal.Table)
               .WhereField(TzAccount.ComponentModal.Table, TzAccount.ComponentModal.ClientID.Name,
               Compare.Equals, DBConst.String(clientID));
            return db.GetDatatable(select);
        }

        //public string Save(string clientID,
        //    string parentComponent,
        //    string name,
        //    string category)
        //{

        //}
    }
}
