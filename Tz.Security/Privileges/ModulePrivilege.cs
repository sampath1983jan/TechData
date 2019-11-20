using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Security.Privileges
{
    public class ModulePrivilege : IPrivilege

    {

        private string key;
        private string userid;
        private bool _add;
        private bool _remove;
        private bool _edit;
        private bool _view;
        private string _privilegeName;
        private string _groupID;
        private string _componentID;
        private int _comptype;
        private string _clientid;

        public string PrivilegeID { get => key; set => key = value; }
        public string UserID { get => userid; set => userid = value; }
        public bool IsAdd { get => _add; set => _add = value; }
        public bool IsRemove { get => _remove; set => _remove = value; }
        public bool IsEdit { get => _edit; set => _edit = value; }
        public bool IsView { get => _view; set => _view = value; }
        public string PrivilegeName { get => _privilegeName; set => _privilegeName = value; }


        public string ClientID { get => _clientid; set => _clientid = value; }
        public string GroupID { get => _groupID; set => _groupID = value; }
        public string ComponentID { get => _componentID; set => _componentID = value; }
        public Int32 ComponentType { get => _comptype; set => _comptype = value; }

        PrivilegeType IPrivilege.ComponentType => throw new NotImplementedException();

        public ModulePrivilege(string clientid, string userID, string key)
        {

        }
        public ModulePrivilege(string clientid, string userID, string key, bool add, bool remove,
            bool edit, bool view)
        {
            this.UserID = userID;
            this.IsAdd = add;
            this.IsRemove = remove;
            this.IsEdit = edit;
            this.IsView = view;
            this.PrivilegeID = key;
        }

     

        public bool Save()
        {
            throw new NotImplementedException();
        }

        bool IPrivilege.Remove()
        {
            throw new NotImplementedException();
        }
    }
}
