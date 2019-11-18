using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tz.Net;
using Tz.Security;

public class Role
{
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public virtual ICollection<IUser> Users { get; set; }
}