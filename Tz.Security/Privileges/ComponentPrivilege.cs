using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Security.Privileges
{
    public class ComponentPrivilege : IPrivilege
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

        public string PrivilegeID { get => key; set => key = value; }
   
        public bool IsAdd { get => _add; set => _add = value; }
        public bool IsRemove { get => _remove; set => _remove = value; }
        public bool IsEdit { get => _edit; set => _edit = value; }
        public bool IsView { get => _view; set => _view = value; }
        public string PrivilegeName { get => _privilegeName; set => _privilegeName = value; }


        public string ClientID { get => _clientid; set => _clientid = value; }
        public string GroupID { get => _groupID; set => _groupID = value; }
        public string ComponentID { get => _componentID; set => _componentID = value; }
        public PrivilegeType ComponentType { get =>  PrivilegeType.COMPONENT; }

        public ComponentPrivilege(string clientid, string groupID, string componentID)
        {
            this.ClientID = clientid;
            this.GroupID = groupID;
            this.ComponentID = componentID;
        }
        public ComponentPrivilege(string clientid, string groupID, string componentID, string key, bool add, bool remove,
            bool edit, bool view)
        {
            this.ComponentID = componentID;
            this.GroupID = groupID;
            this.IsAdd = add;
            this.IsRemove = remove;
            this.IsEdit = edit;
            this.IsView = view;
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
                               this.IsAdd,
                               this.IsEdit,
                               this.IsView,
                               this.IsRemove);
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
                     this.IsAdd,
                     this.IsEdit,
                     this.IsView,
                     this.IsRemove))
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
