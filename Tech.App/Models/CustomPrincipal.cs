using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Tz.Net;
using Tz.Security;

namespace CustomAuthentication
{
    public class CustomPrincipal : IPrincipal
    {
        #region Identity Properties

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        #endregion

        public IIdentity Identity
        {
            get; private set;
        }

        public bool IsInRole(string role)
        {
            return true;
            //if (role == "Admin")
            //{
            //    if (Roles == UserType.Admin || Roles == UserType.SuperAdmin)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //else if (role == "User") {
            //    if (Roles == UserType.User || Roles == UserType.SuperAdmin || Roles == UserType.Admin || Roles == UserType.SuperUser)
            //    {
            //        return true;
            //    }
            //    else {
            //        return false;
            //    }
            //}
            //return false;
        }

        
        public CustomPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }
    }
}