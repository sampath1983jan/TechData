﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Security
{
    public enum PrivilegeType {
        ANALYTIC=0,
        COMPONENT=1,
        DASHBOARD=2,
        FEATURE=3,
        REPORT=4
    }


    public interface IPrivilege
    {
        string PrivilegeID { get; set; }
        string ClientID { get; set; }
        string GroupID { get; set; }
        string ComponentID { get; set; }
        PrivilegeType ComponentType { get; }
        //string UserID { get; set; }
        bool Add { get; set; }
        bool Remove { get; set; }
        bool Edit { get; set; }
        bool View { get; set; }
        bool Save();
        bool Remove();
         
    }
}
