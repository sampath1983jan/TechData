using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tz.Global;
using Tz.Data.Security;
namespace Tz.Security
{
    //http://ryankirkman.com/2013/01/31/activity-based-authorization.html
   
    public enum UserType
    {
        User,
        SuperUser,
        Admin,
        SuperAdmin,
    }
    public abstract class IUser
    {
        public abstract string UserID { get; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public bool IsActive { get; set; }
        public Guid ActivationCode { get; set; }
        public UserGroup UserGroup;
        public abstract bool isAuthenticateUser { get; }
        public string ClientID { get; set; }
    }
    public class User : IUser
    {
        private string _userid;
        private bool _isauth;
        private Tz.Data.Security.User  dUser;
        public override string UserID => _userid;
        public override bool isAuthenticateUser => _isauth;
        public User()
        {
            _userid = "";
        //    UserRole = new UserSecurityGroup();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        public User(string userID,string clientID)
        {
            _userid = userID;
            this.ClientID = clientID;
            dUser = new Tz.Data.Security.User();
         //   UserRole = new UserSecurityGroup();
            Load();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public User(string clientid,string userName,
            string password)
        {
            this.ClientID = clientid;
            this.UserName = userName;
            this.Password = password;
            _userid = "";
            this.LastName = "";
            this.FirstName = "";
            this.Email = "";        
            dUser = new Tz.Data.Security.User();
            Authenticate();
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="usertype"></param>
        /// <param name="status"></param>
        public User(string clientid,string userName,
            string password,
            UserType usertype,
            bool status,
            string firstName, 
            string lastName, 
            string email)
        {
            dUser = new Tz.Data.Security.User();
            _userid = "";
            this.UserName = userName;
            this.Password = password;
            this.UserType = usertype;
            this.IsActive = status;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            _isauth = true;
            this.ClientID = clientid;
        }
        private void LoadSecurityGroup() {
            
        }

        /// <summary>
        /// 
        /// </summary>
        private void Authenticate()
        {
            DataTable dt = new DataTable();
            dt = dUser.GetUser(this.UserName, this.Password);
            if (dt.Rows.Count > 0)
            {
                User c = dt.toList<User>(new DataFieldMappings()
                   .Add(Tz.Global.TzAccount.User.UserName.Name, "UserName")
                   .Add(Tz.Global.TzAccount.User.FirstName.Name, "FirstName")
                   .Add(Tz.Global.TzAccount.User.LastName.Name, "LastName")
                   .Add(Tz.Global.TzAccount.User.Email.Name, "Email")
                   //.Add(Tz.Global.TzAccount.User.UserType.Name, "UserType")
                   .Add(Tz.Global.TzAccount.User.Status.Name, "Status")
                   .Add(Tz.Global.TzAccount.User.Password.Name, "Password")
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
        public User GetUserDetailByEmail()
        {
            DataTable dt = new DataTable();
            User c = null;
            dt = dUser.GetUserByEmail(this.UserName);
            if (dt.Rows.Count > 0)
            {
                c = dt.toList<User>(new DataFieldMappings()
                 .Add(Tz.Global.TzAccount.User.UserName.Name, "UserName")
                   .Add(Tz.Global.TzAccount.User.FirstName.Name, "FirstName")
                 .Add(Tz.Global.TzAccount.User.LastName.Name, "LastName")
                 .Add(Tz.Global.TzAccount.User.Email.Name, "Email")
                 // .Add(Tz.Global.TzAccount.User.UserType.Name, "UserType")
                 .Add(Tz.Global.TzAccount.User.Status.Name, "Status")
                 .Add(Tz.Global.TzAccount.User.Password.Name, "Password")
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
                   .Add(Tz.Global.TzAccount.User.UserName.Name, "UserName")
                     .Add(Tz.Global.TzAccount.User.FirstName.Name, "FirstName")
                   .Add(Tz.Global.TzAccount.User.LastName.Name, "LastName")
                   .Add(Tz.Global.TzAccount.User.Email.Name, "Email")
                   // .Add(Tz.Global.TzAccount.User.UserType.Name, "UserType")
                   .Add(Tz.Global.TzAccount.User.Status.Name, "Status")
                   .Add(Tz.Global.TzAccount.User.Password.Name, "Password")
                   , null, null).FirstOrDefault();
                this.Merge<User>(c);
                if (dt.Rows[0]["UserType"] == null)
                {
                    this.UserType = (UserType)dt.Rows[0]["UserType"];
                }
                _isauth = true;
                UserGroup = new UserGroup(this.UserID,this.ClientID);
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
        public bool ChangePassword(string newPass)
        {
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
        public bool ChangeStatus(bool status)
        {
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

        public bool Update()
        {
            try
            {
                dUser.Update(this.UserID, 
                    this.FirstName, 
                    this.LastName, 
                    this.Email, 
                    this.IsActive);
            }
            catch (System.Exception)
            {
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
                this.IsActive, this.FirstName, this.LastName, this.Email);
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
