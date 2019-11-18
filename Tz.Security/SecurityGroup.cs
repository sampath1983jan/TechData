using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tz.ClientManager;
using Tz.Security.Group;
using Tz.Security.Privileges;
namespace Tz.Security
{
    public enum Context {
        My=1,
        Team=2,
        Others=3
    }
    public enum GroupBaseType
    {
        user=0,
        manager=1,
        executemanager=2,
        admin=3,
        superadmin=4,
        systemadmin=5,
        unknown=-1
    }
    public enum PrivilegeComponent
    {
        Analytics=0,
        Component=1,
        Dashboard=2,
        Feature=3,
        Report=4
    }
    public abstract class baseScurityGroup
    {
        /// <summary>
        /// 
        /// </summary>
        public string ClientID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string GroupID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Context UserContext { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public GroupBaseType GroupType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsBaseType { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public List<Security.Privileges.AnalyticPrivilege> AnalyticPrivileges { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ComponentPrivilege> ComponentPrivileges { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DashboardPrivilege> DashboardPrivileges { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<FeaturePrivilege> FeaturePrivileges { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ReportPrivilege> ReportPrivileges { get; set; }

        public void LoadPrivileges() { }
        public void Check() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentid"></param>
        /// <param name="add"></param>
        /// <param name="edit"></param>
        /// <param name="remove"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public bool AddAnalyticPrivilege(string componentid,bool add,bool edit,bool remove,bool view) {
           return GroupImplimentor.AddPrivilege(this.ClientID,
               this.GroupID,
                componentid,
                (int)PrivilegeType.ANALYTIC,
                add,
                edit,
                remove,
                view
               );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentid"></param>
        /// <param name="add"></param>
        /// <param name="edit"></param>
        /// <param name="remove"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public bool AddDashboardPrivilege(string componentid, bool add, bool edit, bool remove, bool view)
        {
            return GroupImplimentor.AddPrivilege(this.ClientID,
                this.GroupID,
                 componentid,
                 (int)PrivilegeType.DASHBOARD,
                 add,
                 edit,
                 remove,
                 view
                );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentid"></param>
        /// <param name="add"></param>
        /// <param name="edit"></param>
        /// <param name="remove"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public bool AddComponentPrivilege(string componentid, bool add, bool edit, bool remove, bool view)
        {
            return GroupImplimentor.AddPrivilege(this.ClientID,
                this.GroupID,
                 componentid,
                 (int)PrivilegeType.COMPONENT,
                 add,
                 edit,
                 remove,
                 view
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentid"></param>
        /// <param name="add"></param>
        /// <param name="edit"></param>
        /// <param name="remove"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public bool AddFeaturePrivilege(string componentid, bool add, bool edit, bool remove, bool view)
        {
            return GroupImplimentor.AddPrivilege(this.ClientID,
                this.GroupID,
                 componentid,
                 (int)PrivilegeType.FEATURE,
                 add,
                 edit,
                 remove,
                 view
                );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentid"></param>
        /// <param name="add"></param>
        /// <param name="edit"></param>
        /// <param name="remove"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public bool AddReportPrivilege(string componentid, bool add, bool edit, bool remove, bool view)
        {
            return GroupImplimentor.AddPrivilege(this.ClientID,
                this.GroupID,
                 componentid,
                 (int)PrivilegeType.REPORT,
                 add,
                 edit,
                 remove,
                 view
                );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            string key = GroupImplimentor.SaveGroup(this.ClientID,
                                this.GroupID,
                                this.Name,
                                this.Description,
                                this.UserContext,
                                this.IsBaseType);
            if (key != "")
            {
                this.GroupID = key;
                return true;
            }
            else return false;
        }
        public bool Remove() {
            if (GroupImplimentor.Remove(this.ClientID, this.GroupID))
            {
                return true;
            }
            else
                return false;
        }
        public IGroupImplementor GroupImplimentor { get; set; }
    }

    public class SecurityGroup: baseScurityGroup
    {
        private string Conn;
              
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupBaseType"></param>
        /// <param name="conn"></param>
        public SecurityGroup(string clientID,
            string groupID,
            string conn) {
            Conn = conn;
            this.ClientID = clientID;
              
    
            this.GroupID = groupID;
            Data.Group.SecurityGroup dataGroup = new Data.Group.SecurityGroup(conn);
            System.Data.DataTable dt = dataGroup.GetSecurityGroup(this.ClientID, this.GroupID);
            foreach (DataRow dr in dt.Rows)
            {
                this.Name = dr["GroupName"] == null ? "" : (string)dr["GroupName"];
                this.Description = dr["Description"] == null ? "" : (string)dr["Description"];
                this.UserContext = dr["Context"] == null ? Context.My : (Context)dr["Context"];
                this.IsBaseType = dr["IsBaseType"] == null ? false : (bool)dr["IsBaseType"];
                this.GroupType = dr["BaseType"] == null ? GroupBaseType.user : (GroupBaseType)dr["BaseType"];
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        public SecurityGroup(string clientID,string conn) {
            Conn = conn;
            this.ClientID = clientID;
            this.GroupID = "";
            this.UserContext = Context.My;
            this.IsBaseType = true;
            this.GroupType = GroupBaseType.user;
        }
        /// <summary>
        /// 
        /// </summary>
        public void LoadAnalyticPrivilege()
        {
            Data.Privileges.SecurityPrivilege sp = new Data.Privileges.SecurityPrivilege(Conn);
            DataTable dt = sp.GetAnalyticPrivilege(this.ClientID, this.GroupID);      
            foreach (DataRow dr in dt.Rows)
            {
                AnalyticPrivilege item = new AnalyticPrivilege(this.ClientID, this.GroupID);
                AnalyticPrivileges.Add(item);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void LoadComponentPrivilege() {
            Data.Privileges.SecurityPrivilege sp = new Data.Privileges.SecurityPrivilege(Conn);
            DataTable dt = sp.GetDashboardPrivilege(this.ClientID, this.GroupID);
            foreach (DataRow dr in dt.Rows)
            {
                AnalyticPrivilege item = new AnalyticPrivilege(this.ClientID, this.GroupID);
                AnalyticPrivileges.Add(item);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void LoadFeaturePrivilege()
        {
            Data.Privileges.SecurityPrivilege sp = new Data.Privileges.SecurityPrivilege(Conn);
            DataTable dt = sp.GetFeaturePrivilege(this.ClientID, this.GroupID);
            foreach (DataRow dr in dt.Rows)
            {
                AnalyticPrivilege item = new AnalyticPrivilege(this.ClientID, this.GroupID);
                AnalyticPrivileges.Add(item);
            }
        }
        /// <summary>
        /// 
        /// </summary>      
        public void LoadDashboardPrivilege()
        {
            Data.Privileges.SecurityPrivilege sp = new Data.Privileges.SecurityPrivilege(Conn);
            DataTable dt = sp.GetDashboardPrivilege(this.ClientID, this.GroupID);
            foreach (DataRow dr in dt.Rows)
            {
                AnalyticPrivilege item = new AnalyticPrivilege(this.ClientID, this.GroupID);
                AnalyticPrivileges.Add(item);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void LoadReportPrivilege()
        {
            Data.Privileges.SecurityPrivilege sp = new Data.Privileges.SecurityPrivilege(Conn);
            DataTable dt = sp.GetReportPrivilege(this.ClientID, this.GroupID);
            foreach (DataRow dr in dt.Rows)
            {
                AnalyticPrivilege item = new AnalyticPrivilege(this.ClientID, this.GroupID);
                AnalyticPrivileges.Add(item);
            }
        }               
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public static string GetConnection(string clientID)
        {
            string conn;
            ClientServer ck;
            ck = new ClientServer(clientID);
            conn = ck.GetServer().Connection();
            return conn;
        }
    }

}



