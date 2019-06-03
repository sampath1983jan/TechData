using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data;
using Tech.Data.Query;

namespace Tz.Data
{

    public class TzAccount {
        public const string DbProvider = "MySql.Data.MySqlClient";
        public const string Schema = "";
        //public class Account
        //{
        //    public const string Table = "cf_Account";
        //    public static readonly DBColumn AccountID = DBColumn.Column("AccountID", System.Data.DbType.String,255, 
        //        DBColumnFlags.PrimaryKey);
        //    public static readonly DBColumn Name = DBColumn.Column("AccountName", System.Data.DbType.String, 225);
        //    public static readonly DBColumn NoOfUsers = DBColumn.Column("NoOfUsers", System.Data.DbType.Int32, DBColumnFlags.Nullable);
        //    public static readonly DBColumn AccountNo = DBColumn.Column("AccountNo", System.Data.DbType.String,255, DBColumnFlags.Nullable);
        //    public static readonly DBColumn Status = DBColumn.Column("Status", System.Data.DbType.Boolean);
        //}
        public class Client
        {
            public const string Table = "cf_Client";
            public static readonly DBColumn ClientID = DBColumn.Column("ClientID", System.Data.DbType.String, 255, DBColumnFlags.PrimaryKey);
            //public static readonly DBColumn AccountID = DBColumn.Column("AccountID", System.Data.DbType.String, 255,
            //   DBColumnFlags.PrimaryKey );
            public static readonly DBColumn ClientName = DBColumn.Column("ClientName", System.Data.DbType.String, 255,
              DBColumnFlags.Nullable);
            public static readonly DBColumn ClientNo = DBColumn.Column("ClientNo", System.Data.DbType.String, 255,
              DBColumnFlags.Nullable);
            public static readonly DBColumn Address = DBColumn.Column("Address", System.Data.DbType.String, 500,
            DBColumnFlags.Nullable);
            public static readonly DBColumn State = DBColumn.Column("State", System.Data.DbType.String, 255,
       DBColumnFlags.Nullable);
            public static readonly DBColumn Country = DBColumn.Column("Country", System.Data.DbType.String, 255,
       DBColumnFlags.Nullable);
            public static readonly DBColumn Email = DBColumn.Column("Email", System.Data.DbType.String, 255,
       DBColumnFlags.Nullable);
            public static readonly DBColumn PhoneNo = DBColumn.Column("PhoneNo", System.Data.DbType.String, 255,
       DBColumnFlags.Nullable);
            public static readonly DBColumn OrganizationName = DBColumn.Column("OrganizationName", System.Data.DbType.String, 255,
       DBColumnFlags.Nullable);
            public static readonly DBColumn Status = DBColumn.Column("Status", System.Data.DbType.Boolean,
       DBColumnFlags.Nullable);
            public static readonly DBColumn Host = DBColumn.Column("Host", System.Data.DbType.String, 255,
       DBColumnFlags.Nullable);
            //           public static readonly DBColumn ServerID = DBColumn.Column("serverID", System.Data.DbType.String,255,
            //DBColumnFlags.Nullable);
        }
        public class User
        {
            public const string Table = "cf_User";
            //public static readonly DBColumn ClientID = DBColumn.Column("ClientID", System.Data.DbType.String, 255);
            public static readonly DBColumn UserID = DBColumn.Column("UserID", System.Data.DbType.String, 255,
               DBColumnFlags.PrimaryKey);
            public static readonly DBColumn Password = DBColumn.Column("Password", System.Data.DbType.String, 255,
              DBColumnFlags.Nullable);
            public static readonly DBColumn UserName = DBColumn.Column("UserName", System.Data.DbType.String, 255,
              DBColumnFlags.Nullable);
            public static readonly DBColumn FirstName = DBColumn.Column("FirstName", System.Data.DbType.String, 255,
              DBColumnFlags.Nullable);
            public static readonly DBColumn LastName = DBColumn.Column("LastName", System.Data.DbType.String, 255,
              DBColumnFlags.Nullable);
            public static readonly DBColumn Email = DBColumn.Column("Email", System.Data.DbType.String, 255,
              DBColumnFlags.Nullable);
            public static readonly DBColumn UserType = DBColumn.Column("UserType", System.Data.DbType.Int32,
              DBColumnFlags.Nullable);
            public static readonly DBColumn Status = DBColumn.Column("Status", System.Data.DbType.Boolean,
    DBColumnFlags.Nullable);
        }
        public class Server
        {
            public const string Table = "cf_Server";
            public static readonly DBColumn ServerID = DBColumn.Column("ServerID", System.Data.DbType.String, 255,
     DBColumnFlags.PrimaryKey);
            public static readonly DBColumn ServerName = DBColumn.Column("ServerName", System.Data.DbType.String, 255,
               DBColumnFlags.Nullable);
            public static readonly DBColumn Host = DBColumn.Column("Host", System.Data.DbType.String, 255,
               DBColumnFlags.Nullable);
            public static readonly DBColumn DB = DBColumn.Column("DB", System.Data.DbType.String, 255,
               DBColumnFlags.Nullable);
            public static readonly DBColumn UserID = DBColumn.Column("UserID", System.Data.DbType.String, 255,
              DBColumnFlags.Nullable);
            public static readonly DBColumn Password = DBColumn.Column("Password", System.Data.DbType.String, 255,
              DBColumnFlags.Nullable);
            public static readonly DBColumn Port = DBColumn.Column("Port", System.Data.DbType.Int32,
              DBColumnFlags.Nullable);
        }
        public class Tables {
            public const string Table = "cf_Table";
            public static readonly DBColumn TableID = DBColumn.Column("TableID", System.Data.DbType.String, 255,
           DBColumnFlags.PrimaryKey);
            public static readonly DBColumn ServerID = DBColumn.Column("ServerID", System.Data.DbType.String, 255,
          DBColumnFlags.Nullable);
            public static readonly DBColumn ClientID = DBColumn.Column("ClientID", System.Data.DbType.String, 255,
        DBColumnFlags.Nullable);
            public static readonly DBColumn TableName = DBColumn.Column("TableName", System.Data.DbType.String, 500,
           DBColumnFlags.Nullable);
            public static readonly DBColumn Category = DBColumn.Column("Category", System.Data.DbType.String, 500,
           DBColumnFlags.Nullable);
        }
        public class Field {
            public const string Table = "cf_Fields";

            public static readonly DBColumn FieldID = DBColumn.Column("FieldID", System.Data.DbType.String, 255,
          DBColumnFlags.PrimaryKey);
            public static readonly DBColumn TableID = DBColumn.Column("TableID", System.Data.DbType.String, 255);
            public static readonly DBColumn FieldName = DBColumn.Column("FieldName", System.Data.DbType.String, 500,
           DBColumnFlags.Nullable);
            public static readonly DBColumn FieldType = DBColumn.Column("FieldType", System.Data.DbType.String, 500,
           DBColumnFlags.Nullable);
            public static readonly DBColumn Length = DBColumn.Column("Length", System.Data.DbType.String, 255,
          DBColumnFlags.Nullable);
            public static readonly DBColumn ISPrimaryKey = DBColumn.Column("ISPrimaryKey", System.Data.DbType.Boolean,
           DBColumnFlags.Nullable);
            public static readonly DBColumn IsNullable = DBColumn.Column("IsNullable", System.Data.DbType.Boolean,
           DBColumnFlags.Nullable);
        }
        public class ClientServer
        {
            public const string Table = "cf_ClientServer";
            public static readonly DBColumn ClientID = DBColumn.Column("ClientID", System.Data.DbType.String, 255);
            public static readonly DBColumn ServerID = DBColumn.Column("ServerID", System.Data.DbType.String, 255);
        }
    }

    
}
