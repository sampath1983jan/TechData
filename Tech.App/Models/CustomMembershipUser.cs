using System;
using CustomAuthentication;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Tz.Net;
using Tz.Security;

namespace CustomAuthentication
{
    public class CustomMembershipUser : MembershipUser
    {
        #region User Properties

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Roles { get; set; }
        #endregion

        public CustomMembershipUser(IUser user) : base("CustomMembership", user.UserName, user.UserID, user.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            UserId = user.UserID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Roles = user.UserGroupID;
        }
    }
}