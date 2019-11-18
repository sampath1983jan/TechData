using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data;
using Tech.Data.Query;
using Tz.Global;
namespace Tz.Data.Security
{
    public class User : DataBase
    {
        /// <summary>
        /// 
        /// </summary>
        public User(string conn="")
        {
            InitDbs(conn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataTable GetUser(string UserID)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.SelectAll(TzAccount.User.Table).From(TzAccount.User.Table)
                .WhereField(TzAccount.User.Table, TzAccount.User.UserID.Name, Compare.Equals, DBConst.String(UserID));
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public DataTable GetUser(string UserName,
            string Password)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.SelectAll(TzAccount.User.Table).From(TzAccount.User.Table)
                .WhereField(TzAccount.User.Table, TzAccount.User.UserName.Name, Compare.Equals, DBConst.String(UserName))
                .AndWhere(TzAccount.User.Table, TzAccount.User.Password.Name, Compare.Equals, DBConst.String(Password));
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public DataTable GetUserByEmail(string UserName)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.SelectAll(TzAccount.User.Table).From(TzAccount.User.Table)
                .WhereField(TzAccount.User.Table, TzAccount.User.UserName.Name, Compare.Equals, DBConst.String(UserName));
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="UserType"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public string Save(string UserName,
            string Password,
            int UserType,
            bool Status, string firstName, string lastName, string email)
        {
            DBDatabase db;
            db = base.Database;
            string a = Shared.generateID();
            DBConst dbUserID = DBConst.String(a);
            DBConst dbStatus = DBConst.Const(DbType.Boolean, Status);
            DBConst dbUserType = DBConst.Int32(UserType);
            DBConst dbUserName = DBConst.String(UserName);
            DBConst dbPass = DBConst.String(Password);
            DBConst dbfirstName = DBConst.String(firstName);
            DBConst dblastName = DBConst.String(lastName);
            DBConst dbemail = DBConst.String(email);

            DBQuery insert = DBQuery.InsertInto(TzAccount.User.Table).Fields(
                TzAccount.User.UserID.Name,
               TzAccount.User.UserName.Name,
               TzAccount.User.UserType.Name,
               TzAccount.User.Status.Name,
               TzAccount.User.FirstName.Name,
               TzAccount.User.LastName.Name,
               TzAccount.User.Email.Name,
               TzAccount.User.Password.Name).Values(
               dbUserID,
               dbUserName,
               dbUserType,
               dbStatus,
               dbfirstName,
               dblastName,
               dbemail,
               dbPass
               );
            int val = 0;
            using (DbTransaction trans = db.BeginTransaction())
            {
                val = db.ExecuteNonQuery(trans, insert);
                trans.Commit();
            }
            if (val > 0)
            {
                return a;
            }
            else
            {
                return "";
            }
        }

        public bool Update(string UserId, string firstname, string lastName, string email, bool status)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbUserID = DBConst.String(UserId);
            DBConst dbfirstname = DBConst.String(firstname);
            DBConst dblastName = DBConst.String(lastName);
            DBConst dbemail = DBConst.String(email);
            DBConst dbStatus = DBConst.Const(DbType.Boolean, status);
            DBQuery update = DBQuery.Update(TzAccount.User.Table).Set(
                TzAccount.User.Status.Name, dbStatus).Set(
                TzAccount.User.FirstName.Name, dbfirstname).Set(
                TzAccount.User.LastName.Name, dblastName).Set(
                TzAccount.User.Email.Name, dbemail)
                .WhereField(TzAccount.User.UserID.Name, Compare.Equals, dbUserID);
            int i = db.ExecuteNonQuery(update);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool UpdateStatus(string UserID,
            bool Status)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbUserID = DBConst.String(UserID);
            DBConst dbStatus = DBConst.Const(DbType.Boolean, Status);
            DBQuery update = DBQuery.Update(TzAccount.User.Table).Set(
                TzAccount.User.Status.Name, dbStatus)
                .WhereField(TzAccount.User.UserID.Name, Compare.Equals, dbUserID);
            int i = db.ExecuteNonQuery(update);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool UpdateChangePassword(string UserID,
            string Password)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbUserID = DBConst.String(UserID);
            DBConst dbPassword = DBConst.String(Password);
            DBQuery update = DBQuery.Update(TzAccount.User.Table).Set(
                TzAccount.User.Password.Name, dbPassword)
                .WhereField(TzAccount.User.UserID.Name, Compare.Equals, dbUserID);
            int i = db.ExecuteNonQuery(update);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool Remove(string UserID)
        {
            DBDatabase db;
            db = base.Database;
            DBConst DBUserID = DBConst.String(UserID);

            DBQuery del = DBQuery.DeleteFrom(TzAccount.User.Table)
                                .WhereFieldEquals(TzAccount.User.UserID.Name, DBUserID);
            int i = db.ExecuteNonQuery(del);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
