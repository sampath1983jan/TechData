using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data;
using Tech.Data.Query;
using Tz.Global;

namespace Tz.Security.Data.Privileges
{
 public   class SecurityPrivilege: DataBase
    {
        DBDatabase db;
        public SecurityPrivilege(string conn)
        {
            InitDbs(conn);
            db = base.Database;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="groupid"></param>
        /// <param name="privilegeid"></param>
        /// <returns></returns>
        public DataTable GetPrivilege(string clientid, string groupid, string privilegeid) {
            DBQuery select;
            DBComparison privilege = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.PrivilegeID.Name), DBConst.String(privilegeid));
        //    DBComparison user = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.UserID.Name), DBConst.String(userid));
            DBComparison SecurityGroupID = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.SecurityGroupID.Name), DBConst.String(groupid));
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.ClientID.Name), DBConst.String(clientid));
            select = DBQuery.SelectAll(TzAccount.SecurityGroup.Table).From(TzAccount.SecurityGroup.Table)
                  .WhereAll(privilege, SecurityGroupID, Client);
            return db.GetDatatable(select);
        }
        public DataTable GetAllPrivileges(string clientid, string groupid)
        {
            DBQuery select;
         //   DBComparison dbcomponentType = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.ComponentType.Name), DBConst.Int32(0));
            //    DBComparison user = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.UserID.Name), DBConst.String(userid));
            DBComparison SecurityGroupID = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.SecurityGroupID.Name), DBConst.String(groupid));
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.ClientID.Name), DBConst.String(clientid));
            select = DBQuery.SelectAll(TzAccount.SecurityGroup.Table).From(TzAccount.SecurityGroup.Table)
                  .WhereAll( SecurityGroupID, Client);
            return db.GetDatatable(select);
        }

        public DataTable GetAnalyticPrivilege(string clientid, string groupid)
        {
            DBQuery select;
            DBComparison dbcomponentType = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.ComponentType.Name), DBConst.Int32(0));
            //    DBComparison user = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.UserID.Name), DBConst.String(userid));
            DBComparison SecurityGroupID = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.SecurityGroupID.Name), DBConst.String(groupid));
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.ClientID.Name), DBConst.String(clientid));
            select = DBQuery.SelectAll(TzAccount.SecurityGroup.Table).From(TzAccount.SecurityGroup.Table)
                  .WhereAll(dbcomponentType, SecurityGroupID, Client);
            return db.GetDatatable(select);
        }

        public DataTable GetDashboardPrivilege(string clientid, string groupid)
        {
            DBQuery select;
            DBComparison dbcomponentType = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.ComponentType.Name), DBConst.Int32(2));
            //    DBComparison user = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.UserID.Name), DBConst.String(userid));
            DBComparison SecurityGroupID = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.SecurityGroupID.Name), DBConst.String(groupid));
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.ClientID.Name), DBConst.String(clientid));
            select = DBQuery.SelectAll(TzAccount.SecurityGroup.Table).From(TzAccount.SecurityGroup.Table)
                  .WhereAll(dbcomponentType, SecurityGroupID, Client);
            return db.GetDatatable(select);
        }

        public DataTable GetFeaturePrivilege(string clientid, string groupid)
        {
            DBQuery select;
            DBComparison dbcomponentType = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.ComponentType.Name), DBConst.Int32(3));
            //    DBComparison user = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.UserID.Name), DBConst.String(userid));
            DBComparison SecurityGroupID = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.SecurityGroupID.Name), DBConst.String(groupid));
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.ClientID.Name), DBConst.String(clientid));
            select = DBQuery.SelectAll(TzAccount.SecurityGroup.Table).From(TzAccount.SecurityGroup.Table)
                  .WhereAll(dbcomponentType, SecurityGroupID, Client);
            return db.GetDatatable(select);
        }

        public DataTable GetComponentPrivilege(string clientid, string groupid)
        {
            DBQuery select;
            DBComparison dbcomponentType = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.ComponentType.Name), DBConst.Int32(1));
            //    DBComparison user = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.UserID.Name), DBConst.String(userid));
            DBComparison SecurityGroupID = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.SecurityGroupID.Name), DBConst.String(groupid));
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.ClientID.Name), DBConst.String(clientid));
            select = DBQuery.SelectAll(TzAccount.SecurityGroup.Table).From(TzAccount.SecurityGroup.Table)
                  .WhereAll(dbcomponentType, SecurityGroupID, Client);
            return db.GetDatatable(select);
        }

        public DataTable GetReportPrivilege(string clientid, string groupid)
        {
            DBQuery select;
            DBComparison dbcomponentType = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.ComponentType.Name), DBConst.Int32(4));
            //    DBComparison user = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.UserID.Name), DBConst.String(userid));
            DBComparison SecurityGroupID = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.SecurityGroupID.Name), DBConst.String(groupid));
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.ClientID.Name), DBConst.String(clientid));
            select = DBQuery.SelectAll(TzAccount.SecurityGroup.Table).From(TzAccount.SecurityGroup.Table)
                  .WhereAll(dbcomponentType, SecurityGroupID, Client);
            return db.GetDatatable(select);
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="groupid"></param>
        /// <param name="userid"></param>
        /// <param name="componentid"></param>
        /// <param name="componentype"></param>
        /// <param name="add"></param>
        /// <param name="edit"></param>
        /// <param name="view"></param>
        /// <param name="remove"></param>
        /// <returns></returns>
        public string Save(string clientid,             
            string groupid,         
             string componentid,
             int componentype,
            bool add,
            bool edit,
            bool view,
            bool remove
            )
        {
            string a = Shared.generateID();
            var insert = DBQuery.InsertInto(TzAccount.SecurityPrivilege.Table)
                    .Fields(TzAccount.SecurityPrivilege.PrivilegeID.Name,
                    TzAccount.SecurityPrivilege.ClientID.Name,
                    TzAccount.SecurityPrivilege.SecurityGroupID.Name,
                    //TzAccount.SecurityPrivilege.UserID.Name,
                    TzAccount.SecurityPrivilege.SecurityComponentID.Name,
                    TzAccount.SecurityPrivilege.ComponentType.Name,
                    TzAccount.SecurityPrivilege.Add.Name,
                    TzAccount.SecurityPrivilege.Edit.Name,
                    TzAccount.SecurityPrivilege.Remove .Name,
                    TzAccount.SecurityPrivilege.View.Name
                    ).
                    Values(DBConst.String(a),
                    DBConst.String(clientid),
                    DBConst.String(groupid),
                //    DBConst.String(userid),
                    DBConst.String(componentid),
                    DBConst.Int32(componentype),
                    DBConst.Const(DbType.Boolean, add),
                    DBConst.Const(DbType.Boolean, edit),
                    DBConst.Const(DbType.Boolean, view),                     
                    DBConst.Const(DbType.Boolean, remove));
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

        //  public string Save( DataTable dt
        //   )
        //  {
        //      string clientid;
        //   string groupid;
        ////   string userid,
        //    string componentid;
        //    int componentype;
        //   bool add;
        //   bool edit;
        //   bool view;
        //   bool remove;

        //      string a = Shared.generateID();
        //      var insert = DBQuery.InsertInto(TzAccount.SecurityPrivilege.Table)
        //              .Fields(TzAccount.SecurityPrivilege.PrivilegeID.Name,
        //              TzAccount.SecurityPrivilege.ClientID.Name,
        //              TzAccount.SecurityPrivilege.SecurityGroupID.Name,
        //              //TzAccount.SecurityPrivilege.UserID.Name,
        //              TzAccount.SecurityPrivilege.SecurityComponentID.Name,
        //              TzAccount.SecurityPrivilege.ComponentType.Name,
        //              TzAccount.SecurityPrivilege.Add.Name,
        //              TzAccount.SecurityPrivilege.Edit.Name,
        //              TzAccount.SecurityPrivilege.Remove.Name,
        //              TzAccount.SecurityPrivilege.View.Name
        //              ).
        //              Values(DBConst.String(a),
        //              DBConst.String(clientid),
        //              DBConst.String(groupid),
        //              //    DBConst.String(userid),
        //              DBConst.String(componentid),
        //              DBConst.Int32(componentype),
        //              DBConst.Const(DbType.Boolean, add),
        //              DBConst.Const(DbType.Boolean, edit),
        //              DBConst.Const(DbType.Boolean, view),
        //              DBConst.Const(DbType.Boolean, remove));
        //      int val = 0;
        //      using (DbTransaction trans = db.BeginTransaction())
        //      {
        //          val = db.ExecuteNonQuery(trans, insert);
        //          trans.Commit();
        //      }
        //      if (val > 0)
        //      {
        //          return a;
        //      }
        //      else
        //      {
        //          return "";
        //      }
        //  }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="privilegeid"></param>
        /// <param name="groupid"></param>
        /// <param name="componentID"></param>
        /// <param name="add"></param>
        /// <param name="edit"></param>
        /// <param name="view"></param>
        /// <param name="remove"></param>
        /// <returns></returns>
        public bool update(string clientid,
            string privilegeid,
            string groupid,
            string componentID,            
            bool add,
            bool edit,
            bool view,
            bool remove) {
            DBComparison privilege = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.PrivilegeID.Name), DBConst.String(privilegeid));
             DBComparison component = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.SecurityComponentID.Name), DBConst.String(componentID));
            DBComparison SecurityGroupID = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.SecurityGroupID.Name), DBConst.String(groupid));
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.ClientID.Name), DBConst.String(clientid));

            DBQuery update = DBQuery.Update(TzAccount.SecurityPrivilege.Table).Set(
        TzAccount.SecurityPrivilege.Add.Name, DBConst.Const( DbType.Boolean, add)
        ).Set(
        TzAccount.SecurityPrivilege.Edit.Name, DBConst.Const(DbType.Boolean, edit)
        ).Set(
        TzAccount.SecurityPrivilege.View.Name, DBConst.Const(DbType.Boolean, view)
        ).Set(
        TzAccount.SecurityPrivilege.Remove.Name, DBConst.Const(DbType.Boolean, remove)).
        WhereAll(SecurityGroupID, Client,privilege, component);
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

        public bool RemovePrivileges(string groupID, string clientid,string componentid,string privilegeid)
        {

            DBComparison privilege = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.PrivilegeID.Name), DBConst.String(groupID));
            DBComparison component = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.SecurityComponentID.Name), DBConst.String(componentid));

            DBComparison SecurityGroupID = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.SecurityGroupID.Name), DBConst.String(groupID));
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.SecurityPrivilege.ClientID.Name), DBConst.String(clientid));

            DBQuery del = DBQuery.DeleteFrom(TzAccount.SecurityPrivilege.Table)
                               .WhereAll(SecurityGroupID, Client, privilege,component);
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
