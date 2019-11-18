using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tz.Global;
using System.Data;
using Tech.Data;
using Tech.Data.Query;
using System.Data.Common;

namespace Tz.Security.Data.Group
{
    public class SecurityGroup:DataBase
    {
        DBDatabase db;
        public SecurityGroup(string conn) {
            InitDbs(conn);
            db = base.Database;
        }

        public DataTable GetSecurityGroup(string clientid,string groupID) {
            DBQuery select;
         //   DBComparison baseType= DBComparison.Equal(DBField.Field(TzAccount.SecurityGroup.BaseType .Name), DBConst.Int32(pBaseType));
            DBComparison client = DBComparison.Equal(DBField.Field(TzAccount.SecurityGroup.ClientID.Name), DBConst.String(clientid));
            DBComparison group = DBComparison.Equal(DBField.Field(TzAccount.SecurityGroup.SecurityGroupID.Name), DBConst.String(groupID));
               
            select = DBQuery.SelectAll(TzAccount.SecurityGroup.Table).From(TzAccount.SecurityGroup.Table)
                  .WhereAll(client, group);
            return db.GetDatatable(select);
        }
         
        public string Save(string clientid,string groupName,
            string description,
            int context,
            bool isBase,
            int baseType
            )
        {
            string a = Shared.generateID();
        var insert=    DBQuery.InsertInto(TzAccount.SecurityGroup.Table)
                .Fields(TzAccount.SecurityGroup.SecurityGroupID.Name,
                TzAccount.SecurityGroup.ClientID.Name,
                TzAccount.SecurityGroup.GroupName.Name,
                TzAccount.SecurityGroup.Context.Name,
                TzAccount.SecurityGroup.IsBaseType.Name,
                TzAccount.SecurityGroup.BaseType.Name,
                TzAccount.SecurityGroup.Description.Name).
                Values(DBConst.String(a),
                DBConst.String(clientid),
                DBConst.String(groupName),
                DBConst.Int32(context),
                DBConst.Const(DbType.Boolean, isBase),
                DBConst.Int32(baseType),
                DBConst.String(description));
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

        public bool Update(string clientid,string groupID,
            string groupName,
            string description,
            int context,
            bool isBase)
        {
            DBComparison SecurityGroupID = DBComparison.Equal(DBField.Field(TzAccount.SecurityGroup.SecurityGroupID.Name), DBConst.String(groupID ));
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.SecurityGroup.ClientID.Name), DBConst.String(clientid));

            DBQuery update = DBQuery.Update(TzAccount.SecurityGroup.Table).Set(
        TzAccount.SecurityGroup.GroupName.Name, DBConst.String(groupName )
        ).Set(
        TzAccount.SecurityGroup.Description.Name, DBConst.String(description )
        ).Set(
        TzAccount.SecurityGroup.IsBaseType.Name, DBConst.Const(DbType.Boolean, isBase)
        ).Set(
        TzAccount.SecurityGroup.Context.Name, DBConst.Int32(context )).WhereAll(SecurityGroupID, Client);

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
        public bool Remove(string groupID,string clientid) {
            DBComparison SecurityGroupID = DBComparison.Equal(DBField.Field(TzAccount.SecurityGroup.SecurityGroupID.Name), DBConst.String(groupID));
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.SecurityGroup.ClientID.Name), DBConst.String(clientid));

            DBQuery del = DBQuery.DeleteFrom(TzAccount.SecurityGroup.Table)
                               .WhereAll(SecurityGroupID, Client);
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

        public bool RemoveAllPrivileges(string groupID, string clientid)
        {
            DBComparison SecurityGroupID = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.SecurityGroupID.Name), DBConst.String(groupID));
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.ClientID.Name), DBConst.String(clientid));

            DBQuery del = DBQuery.DeleteFrom(TzAccount.SecurityPrivilege.Table)
                               .WhereAll(SecurityGroupID, Client);
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
