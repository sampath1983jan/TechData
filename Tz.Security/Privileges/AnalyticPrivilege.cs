using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Security.Privileges
{
    public class AnalyticPrivilege : IPrivilege
    {
        private string key;
         
        private bool _add;
        private bool _remove;
        private bool _edit;
        private bool _view;
        private string _privilegeName;
        private string _groupID;
        private string _componentID;
     
        private string _clientid;

        public string PrivilegeID { get => key; set => key=value; }
       
        public bool Add { get => _add; set => _add=value; }
        public bool Remove { get => _remove; set => _remove = value; }
        public bool Edit { get => _edit; set =>   _edit=value; }
        public bool View { get => _view; set => _view=value; }
        public string PrivilegeName { get => _privilegeName; set => _privilegeName=value; }

     
        public string ClientID { get => _clientid; set => _clientid=value; }
        public string GroupID { get => _groupID; set => _groupID=value; }
        public string ComponentID { get => _componentID; set => _componentID=value; }
        public PrivilegeType ComponentType { get => PrivilegeType.ANALYTIC; }

        public AnalyticPrivilege(string clientid, string groupID, string componentID)
        {
            this.ClientID = clientid;
            this.GroupID = groupID;
            this.ComponentID = componentID;
        }
        public AnalyticPrivilege(string clientid, string groupID, string componentID, string key, bool add, bool remove,
              bool edit, bool view)
        {
            this.ComponentID = componentID;
            this.GroupID = groupID;
            this.Add = add;
            this.Remove = remove;
            this.Edit = edit;
            this.View = view;
            this.PrivilegeID = key;
        }

        public bool Save()
        {
            Data.Privileges.SecurityPrivilege sp = new Data.Privileges.SecurityPrivilege(SecurityGroup.GetConnection(this.ClientID));
            if (this.PrivilegeID == "")
            {
                string val = sp.Save(this.ClientID,
                               this.GroupID,
                               this.ComponentID,
                              (int)this.ComponentType,
                               this.Add,
                               this.Edit,
                               this.View,
                               this.Remove);
                if (val != "")
                {
                    return true;
                }
                else
                    return false;

            }
            else
            {
                if (sp.update(this.ClientID,
                     this.PrivilegeID,
                     this.GroupID,
                     this.ComponentID,
                     this.Add,
                     this.Edit,
                     this.View,
                     this.Remove))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        bool IPrivilege.Remove()
        {
            Data.Privileges.SecurityPrivilege sp = new Data.Privileges.SecurityPrivilege(SecurityGroup.GetConnection(this.ClientID));
            return sp.RemovePrivileges(this.GroupID,
                 this.ClientID,
                 this.ComponentID,
                 this.PrivilegeID);
        }
    }
}
