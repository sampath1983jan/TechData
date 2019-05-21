using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Tz.Net
{
    public enum UserType {
        User,
        SuperUser,
        Admin,
        SuperAdmin,
    }
    class User:INetImplimentor
    {
       private string _userid;
       private bool _isauth;
        public string UserID { get { return _userid; } }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public bool Status { get; set; }
        public bool isAuthenticateUser { get { return _isauth; } }
        private Data.User dUser;
        public User(string userID) {
            _userid= userID;
            dUser = new Data.User();
            Load();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public User(string userName, string password) {
            this.UserName = userName;
            this.Password = password;
            _userid = "";
            dUser = new Data.User();
            Authenticate();
           
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="usertype"></param>
        /// <param name="status"></param>
        public User(string userName,
            string password,
            UserType usertype,
            bool status)
        {
            dUser = new Data.User();
            _userid = "";
            this.UserName = userName;
            this.Password = password;
            this.UserType = usertype;
            this.Status = status;
            _isauth = true;
        }
        /// <summary>
        /// 
        /// </summary>
        private void Authenticate() {
            DataTable dt = new DataTable();
            dt =dUser.GetUser(this.UserName, this.Password);
            if (dt.Rows.Count > 0)
            {
                User c = dt.toList<User>(new DataFieldMappings()
                   .Add(Tz.Data.TzAccount.User.UserName.Name, "UserName")
                   .Add(Tz.Data.TzAccount.User.UserType.Name, "UserType")
                   .Add(Tz.Data.TzAccount.User.Status.Name, "Status")
                   .Add(Tz.Data.TzAccount.User.Password.Name, "Password")
                   , null).FirstOrDefault();
                this.Merge<User>(c);
                _isauth = true;
            }
            else {
                _isauth = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Load()
        {
            DataTable dt = new DataTable();
            dt = dUser.GetUser(this.UserID);
            if (dt.Rows.Count > 0)
            {
                User c = dt.toList<User>(new DataFieldMappings()
                   .Add(Tz.Data.TzAccount.User.UserName.Name, "UserName")
                   .Add(Tz.Data.TzAccount.User.UserType.Name, "UserType")
                   .Add(Tz.Data.TzAccount.User.Status.Name, "Status")
                   .Add(Tz.Data.TzAccount.User.Password.Name, "Password")
                   , null).FirstOrDefault();
                this.Merge<User>(c);
                _isauth = true;
            }
            else
            {
                _isauth = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPass"></param>
        /// <returns></returns>
        public bool ChangePassword(string newPass) {
            if (_userid != "")
            {
                if (dUser.UpdateChangePassword(this.UserID, newPass))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool ChangeStatus(bool status) {
            if (_userid != "")
            {
                if (dUser.UpdateStatus(this.UserID, status))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            if (_userid == "")
            {
                _userid = dUser.Save(this.UserName,
                this.Password,
                (int)this.UserType,
                this.Status);
                if (_userid == "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
                return false;
            
        }
        public bool Remove()
        {             
            return dUser.Remove(UserID);
        }
    }
}
