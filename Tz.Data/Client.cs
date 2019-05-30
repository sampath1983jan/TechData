using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Tech.Data;
using Tech.Data.Query;
using System.Data.Common;

namespace Tz.Data
{
    public class Client : DataBase
    {
        public Client(string conn) {
            InitDbs(conn);
        }

        public DataTable GetClient(string ClientID){
            DBDatabase db;
            db = base.Database;
            DBQuery select;
         
            select = DBQuery.SelectAll(TzAccount.Client.Table).From(TzAccount.Client.Table)
                .WhereField(TzAccount.Client.Table,TzAccount.Client.ClientID.Name , Compare.Equals, DBConst.String(ClientID));
            return db.GetDatatable(select);
        }


        public DataTable GetClients()
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;

            select = DBQuery.SelectAll(TzAccount.Client.Table).From(TzAccount.Client.Table);
               // .WhereField(TzAccount.Client.Table, TzAccount.Client.ClientID.Name, Compare.Equals, DBConst.String(ClientID));
            return db.GetDatatable(select);
        }


        public string Save(string clientName,
            string clientNo,
            string address,
            string state,
            string country,
            string email,
            string phone,
            string orgName,
            bool status,
            string host)
        {
            DBDatabase db;
            db = base.Database;
            string a = Shared.generateID();
            DBConst dbClientID= DBConst.String(a);
            DBConst dbClientName = DBConst.String(clientName);
            DBConst dbClientNo = DBConst.String(clientNo);
            DBConst dbAddress = DBConst.String(address);
            DBConst dbState = DBConst.String(state);
            DBConst dbCountry = DBConst.String(country);
            DBConst dbstatus = DBConst.Const(DbType.Boolean, status);
            DBConst dbEmail = DBConst.String(email);
            DBConst dbPhone = DBConst.String(phone);
            DBConst dbHost = DBConst.String(host);
            DBConst dbOrgName = DBConst.String(orgName);
                       
            DBQuery insert = DBQuery.InsertInto(TzAccount.Client.Table).Fields(
                TzAccount.Client.ClientID.Name,
                TzAccount.Client.ClientName.Name,
                TzAccount.Client.ClientNo.Name,
                TzAccount.Client.Country.Name,
                TzAccount.Client.State.Name,
                TzAccount.Client.PhoneNo.Name,
                TzAccount.Client.Email.Name,
                TzAccount.Client.Status.Name,
                TzAccount.Client.OrganizationName.Name,
                TzAccount.Client.Host.Name,
                TzAccount.Client.Address.Name).Values(
                dbClientID,
                dbClientName,
                dbClientNo,                
                dbCountry,
                dbState,
                dbPhone,
                dbEmail ,
                dbstatus,
                dbOrgName,
                dbHost,
                dbAddress 
                );
            int val = 0;
            using (DbTransaction trans = db.BeginTransaction()) {
            val=    db.ExecuteNonQuery(trans, insert);
                trans.Commit();
            }
            if (val > 0)
            {
                return a;
            }
            else {
                return "";
            }           

        }

        public bool Update(string ClientID,
           string clientName,
            string clientNo,
            string address,
            string state,
            string country,
            string email,
            string phone,
            string orgName,
            bool status,
            string host)
        {
            DBDatabase db;
            db = base.Database;

            DBConst dbClientID = DBConst.String(ClientID);
            DBConst dbClientName = DBConst.String(clientName);
            DBConst dbClientNo = DBConst.String(clientNo);
            DBConst dbAddress = DBConst.String(address);
            DBConst dbState = DBConst.String(state);
            DBConst dbCountry = DBConst.String(country);
            DBConst dbstatus = DBConst.Const(DbType.Boolean, status);
            DBConst dbEmail = DBConst.String(email);
            DBConst dbPhone = DBConst.String(phone);
            DBConst dbHost = DBConst.String(host);
            DBConst dbOrgName = DBConst.String(orgName);
            DBQuery update = DBQuery.Update(TzAccount.Client.Table).Set(
                TzAccount.Client.ClientName.Name, dbClientName
                ).Set(
                TzAccount.Client.ClientNo.Name, dbClientNo
                ).Set(
                TzAccount.Client.Country.Name, dbCountry
                ).Set(
                TzAccount.Client.State.Name, dbState
                ).Set(
                TzAccount.Client.Email.Name, dbEmail
                ).Set(
                TzAccount.Client.Address.Name, dbAddress
                ).Set(
                TzAccount.Client.PhoneNo.Name, dbPhone
                ).Set(
                TzAccount.Client.OrganizationName.Name, dbOrgName
                ).Set(
                TzAccount.Client.Status.Name, dbstatus
                ).Set(
                TzAccount.Client.Host.Name, dbHost
                ).WhereField(TzAccount.Client.ClientID.Name, Compare.Equals, dbClientID);
         int i=   db.ExecuteNonQuery(update);
            if (i > 0)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public bool Remove(string clientID) {
            DBDatabase db;
            db = base.Database;
            DBConst dbClientID = DBConst.String(clientID);

            DBQuery del = DBQuery.DeleteFrom(TzAccount.Client.Table)
                                .WhereFieldEquals(TzAccount.Client.ClientID.Name, dbClientID);
            int i = db.ExecuteNonQuery(del);
            if (i > 0)
            {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
