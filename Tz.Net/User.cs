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

    public abstract class IUser {     
        public abstract string UserID { get; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public bool IsActive { get; set; }
        public Guid ActivationCode { get; set; }
        public abstract bool isAuthenticateUser { get; }
    }
    public class User : IUser, INetImplimentor
    {
        private string _userid;
        private bool _isauth;
        private Data.User dUser;
        public override string UserID => _userid;
        public override bool isAuthenticateUser => _isauth;
        public User() {
            _userid = "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
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
            this.LastName = "";
            this.FirstName = "";
            this.Email = "";            
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
            bool status,string firstName,string lastName,string email)
        {
            dUser = new Data.User();
            _userid = "";
            this.UserName = userName;
            this.Password = password;
            this.UserType = usertype;
            this.IsActive = status;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
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
                   .Add(Tz.Data.TzAccount.User.FirstName.Name, "FirstName")
                   .Add(Tz.Data.TzAccount.User.LastName.Name, "LastName")
                   .Add(Tz.Data.TzAccount.User.Email.Name, "Email")
                   //.Add(Tz.Data.TzAccount.User.UserType.Name, "UserType")
                   .Add(Tz.Data.TzAccount.User.Status.Name, "Status")
                   .Add(Tz.Data.TzAccount.User.Password.Name, "Password")
                   , null, null).FirstOrDefault();
                this.Merge<User>(c);
                if (dt.Rows[0]["UserType"] == null) {
                    this.UserType = (UserType)dt.Rows[0]["UserType"];
                }
                _isauth = true;
            }
            else {
                _isauth = false;
            }
        }
        public User GetUserDetailByEmail() {
            DataTable dt = new DataTable();
            User c = null;
            dt = dUser.GetUserByEmail(this.UserName);
            if (dt.Rows.Count > 0)
            {
                  c = dt.toList<User>(new DataFieldMappings()
                   .Add(Tz.Data.TzAccount.User.UserName.Name, "UserName")
                     .Add(Tz.Data.TzAccount.User.FirstName.Name, "FirstName")
                   .Add(Tz.Data.TzAccount.User.LastName.Name, "LastName")
                   .Add(Tz.Data.TzAccount.User.Email.Name, "Email")
                   // .Add(Tz.Data.TzAccount.User.UserType.Name, "UserType")
                   .Add(Tz.Data.TzAccount.User.Status.Name, "Status")
                   .Add(Tz.Data.TzAccount.User.Password.Name, "Password")
                   , null, null).FirstOrDefault();
              //  this.Merge<User>(c);
                if (dt.Rows[0]["UserType"] != null)
                {
                    c.UserType = (UserType)dt.Rows[0]["UserType"];
                }
                _isauth = true;
            }
            else
            {
                _isauth = false;
            }
            return c;
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
                     .Add(Tz.Data.TzAccount.User.FirstName.Name, "FirstName")
                   .Add(Tz.Data.TzAccount.User.LastName.Name, "LastName")
                   .Add(Tz.Data.TzAccount.User.Email.Name, "Email")
                   // .Add(Tz.Data.TzAccount.User.UserType.Name, "UserType")
                   .Add(Tz.Data.TzAccount.User.Status.Name, "Status")
                   .Add(Tz.Data.TzAccount.User.Password.Name, "Password")
                   , null, null).FirstOrDefault();
                this.Merge<User>(c);
                if (dt.Rows[0]["UserType"] == null)
                {
                    this.UserType = (UserType)dt.Rows[0]["UserType"];
                }
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

        public bool Update() {
            try
            {
                dUser.Update(this.UserID, this.FirstName, this.LastName, this.Email, this.IsActive);
            }
            catch (System.Exception ex) {
                return false;
            }            
            return true;
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
                this.IsActive,this.FirstName,this.LastName,this.Email);
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
