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

        public class DataScript
        {
            public const string Table = "cf_DataScript";
            public static readonly DBColumn ScriptID = DBColumn.Column("ScriptID", System.Data.DbType.String, 255, DBColumnFlags.PrimaryKey);
            public static readonly DBColumn ScriptName = DBColumn.Column("ScriptName", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn Category = DBColumn.Column("Category", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn Script = DBColumn.Column("Script", System.Data.DbType.String, 655000, DBColumnFlags.Nullable);
        }

        public class ClientServer
        {
            public const string Table = "cf_ClientServer";
            public static readonly DBColumn ClientID = DBColumn.Column("ClientID", System.Data.DbType.String, 255);
            public static readonly DBColumn ServerID = DBColumn.Column("ServerID", System.Data.DbType.String, 255);
        }
        public class ScriptIntend
        {
            public const string Table = "cf_ScriptIntend";
            public static readonly DBColumn ScriptID = DBColumn.Column("ScriptID", System.Data.DbType.String, 255);
            public static readonly DBColumn Intend = DBColumn.Column("Intend", System.Data.DbType.String, 255);
        }
        public class Component {
            public const string Table = "cf_component";
            public static readonly DBColumn ComponentID = DBColumn.Column("componentId", System.Data.DbType.String, 255,DBColumnFlags.PrimaryKey);
            public static readonly DBColumn ClientID = DBColumn.Column("clientid", System.Data.DbType.String, 255);
            public static readonly DBColumn ComponentName = DBColumn.Column("ComponentName", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn Category = DBColumn.Column("Category", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn ComponentType = DBColumn.Column("ComponentType", System.Data.DbType.Int32, DBColumnFlags.Nullable);
            public static readonly DBColumn Title = DBColumn.Column("Title", System.Data.DbType.String, 555, DBColumnFlags.Nullable);
            public static readonly DBColumn TableID = DBColumn.Column("TableID", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn PrimaryKeys = DBColumn.Column("PrimaryKeys", System.Data.DbType.String, 555, DBColumnFlags.Nullable);
            public static readonly DBColumn TitleFormat = DBColumn.Column("TitleFormat", System.Data.DbType.String, 555, DBColumnFlags.Nullable);
            public static readonly DBColumn NewLayout = DBColumn.Column("NewLayout", System.Data.DbType.String, 655000, DBColumnFlags.Nullable);
            public static readonly DBColumn ReadLayout = DBColumn.Column("ReadLayout", System.Data.DbType.String, 655000, DBColumnFlags.Nullable);
            public static readonly DBColumn IsGlobal = DBColumn.Column("IsGlobal", System.Data.DbType.Boolean, DBColumnFlags.Nullable);
            public static readonly DBColumn LastUPD = DBColumn.Column("LastUPD", System.Data.DbType.DateTime, DBColumnFlags.Nullable);
        }   
        public class ComponentAttribute{
            public const string Table = "cf_componentattribute";
            public static readonly DBColumn ClientID = DBColumn.Column("ClientID", System.Data.DbType.String, 255);
            public static readonly DBColumn AttributeName = DBColumn.Column("AttributeName", System.Data.DbType.String, 255);
            public static readonly DBColumn ComponentID = DBColumn.Column("ComponentID", System.Data.DbType.String, 255);
            public static readonly DBColumn FieldID = DBColumn.Column("FieldID", System.Data.DbType.String, 255);
            public static readonly DBColumn IsRequired = DBColumn.Column("IsRequired", System.Data.DbType.Boolean, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn IsUnique = DBColumn.Column("IsUnique", System.Data.DbType.Boolean, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn IsCore = DBColumn.Column("IsCore", System.Data.DbType.Boolean, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn IsReadOnly = DBColumn.Column("IsReadOnly", System.Data.DbType.Boolean, DBColumnFlags.Nullable);
            public static readonly DBColumn IsSecured = DBColumn.Column("IsSecured", System.Data.DbType.Boolean, 555, DBColumnFlags.Nullable);
            public static readonly DBColumn LookUpID = DBColumn.Column("LookUpID", System.Data.DbType.String, DBColumnFlags.Nullable);
            public static readonly DBColumn AttributeType = DBColumn.Column("AttributeType", System.Data.DbType.Int32, 555, DBColumnFlags.Nullable);
            public static readonly DBColumn DefaultValue = DBColumn.Column("DefaultValue", System.Data.DbType.String, 555, DBColumnFlags.Nullable);
            public static readonly DBColumn FileExtension = DBColumn.Column("FileExtension", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn RegExp = DBColumn.Column("RegExp", System.Data.DbType.String, 255, DBColumnFlags.Nullable);
            public static readonly DBColumn IsAuto = DBColumn.Column("IsAuto", System.Data.DbType.Boolean, DBColumnFlags.Nullable);
            public static readonly DBColumn LastUPD = DBColumn.Column("LastUPD", System.Data.DbType.DateTime, DBColumnFlags.Nullable);
        }
        public class ComponentModal {
            public const string Table = "cf_componentmodal";
            public static readonly DBColumn ClientID = DBColumn.Column("ClientID", System.Data.DbType.String, 255);
            public static readonly DBColumn ComponentModalID = DBColumn.Column("ComponentModalID", System.Data.DbType.String, 255,DBColumnFlags.PrimaryKey);
            public static readonly DBColumn ParentComponent = DBColumn.Column("ParentComponent", System.Data.DbType.String, 255);
            public static readonly DBColumn Name = DBColumn.Column("Name", System.Data.DbType.String, 255);
            public static readonly DBColumn Catgory = DBColumn.Column("Catgory", System.Data.DbType.String, 255);
        }
        public class ComponentModalItem {
            public const string Table = "cf_componentmodalItem";
            public static readonly DBColumn ClientID = DBColumn.Column("ClientID", System.Data.DbType.String, 255);
            public static readonly DBColumn ComponentModalID = DBColumn.Column("ComponentModalID", System.Data.DbType.String, 255);
            public static readonly DBColumn ComponentModalItemID = DBColumn.Column("ComponentModalItemID", System.Data.DbType.String, 255);
            public static readonly DBColumn ComponentID = DBColumn.Column("ComponentID", System.Data.DbType.String, 255);
            public static readonly DBColumn ChildComponentID = DBColumn.Column("ChildComponentID", System.Data.DbType.String, 255);
            //public static readonly DBColumn Left = DBColumn.Column("Left", System.Data.DbType.Int32,DBColumnFlags.Nullable);
            //public static readonly DBColumn Right = DBColumn.Column("Right", System.Data.DbType.Int32, DBColumnFlags.Nullable);
            //public static readonly DBColumn Depth = DBColumn.Column("Depth", System.Data.DbType.Int32, DBColumnFlags.Nullable);
            public static readonly DBColumn LastUPD = DBColumn.Column("LastUPD", System.Data.DbType.DateTime, DBColumnFlags.Nullable);
        }
        public class ComponentModalRelation
        {
            public const string Table = "cf_componentmodalRelation";
            public static readonly DBColumn ClientID = DBColumn.Column("ClientID", System.Data.DbType.String, 255);
            public static readonly DBColumn ComponentModalItemID = DBColumn.Column("ComponentModalItemID", System.Data.DbType.String, 255);
            public static readonly DBColumn ComponentModalRelationID = DBColumn.Column("ComponentModalRelationID", System.Data.DbType.String, 255,DBColumnFlags.PrimaryKey);
          //  public static readonly DBColumn ComponentID = DBColumn.Column("ComponentID", System.Data.DbType.String, 255);
          //  public static readonly DBColumn ChildComponentID = DBColumn.Column("ChildComponentID", System.Data.DbType.String, 255);
            public static readonly DBColumn ParentField = DBColumn.Column("ParentField", System.Data.DbType.String, 255);
            public static readonly DBColumn RelatedField = DBColumn.Column("RelatedField", System.Data.DbType.String, 255);
            public static readonly DBColumn Parent = DBColumn.Column("Parent", System.Data.DbType.String, 255);
            public static readonly DBColumn Child = DBColumn.Column("Child", System.Data.DbType.String, 255);
        }

        public class ComponentLookUpItem
        {
            public const string Table = "cf_componentlookupItem";
            public static readonly DBColumn ClientID = DBColumn.Column("ClientID", System.Data.DbType.String, 255);
            public static readonly DBColumn ComponentLookupItemID = DBColumn.Column("ComponentLookupItemID", System.Data.DbType.String, 255,DBColumnFlags.PrimaryKey);
            public static readonly DBColumn Value = DBColumn.Column("Value", System.Data.DbType.String, 255);
            public static readonly DBColumn Description = DBColumn.Column("Description", System.Data.DbType.String, 1000);
            public static readonly DBColumn LookupID = DBColumn.Column("LookupID", System.Data.DbType.String, 255);
            public static readonly DBColumn Label = DBColumn.Column("Label", System.Data.DbType.String, 255);
            public static readonly DBColumn ShortLabel = DBColumn.Column("ShortLabel", System.Data.DbType.String, 255);
            public static readonly DBColumn ParentID = DBColumn.Column("ParentID", System.Data.DbType.String, 255);
            public static readonly DBColumn Order = DBColumn.Column("Order", System.Data.DbType.Int32, 255);
            public static readonly DBColumn IsActive = DBColumn.Column("IsActive", System.Data.DbType.Boolean);
        }

        public class ComponentLookUp {
            public const string Table = "cf_componentlookup";
            public static readonly DBColumn ClientID = DBColumn.Column("ClientID", System.Data.DbType.String, 255);
            public static readonly DBColumn LookupID = DBColumn.Column("LookupID", System.Data.DbType.String, 255, DBColumnFlags.PrimaryKey);
            public static readonly DBColumn LookUpName = DBColumn.Column("Name", System.Data.DbType.String, 255);            
        }

        public class ImportExportEvents {
            public const string Table = "cf_importexportevents";
            public static readonly DBColumn ClientID = DBColumn.Column("ClientID", System.Data.DbType.String, 255);
            public static readonly DBColumn ImportExportID = DBColumn.Column("ImportExportID", System.Data.DbType.String, 255, DBColumnFlags.PrimaryKey);
            public static readonly DBColumn ExportSetting = DBColumn.Column("ExportSetting", System.Data.DbType.String, 10000);
            public static readonly DBColumn Status = DBColumn.Column("Status", System.Data.DbType.Int32, 10);
            public static readonly DBColumn Type = DBColumn.Column("Type", System.Data.DbType.Int32);
            public static readonly DBColumn EventDateTime = DBColumn.Column("EventDateTime", System.Data.DbType.DateTime);
            public static readonly DBColumn IgnoreSQLErrors = DBColumn.Column("IgnoreSQLErrors", System.Data.DbType.Boolean);
            public static readonly DBColumn FilePath = DBColumn.Column("FilePath", System.Data.DbType.String, 500);
            public static readonly DBColumn Errors = DBColumn.Column("Errors", System.Data.DbType.String, 120000);
        }
    }    
}
