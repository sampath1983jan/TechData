using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data;
using Tech.Data.Query;
using Tech.Data.Schema;
using Tz.Data;
namespace Tz.Data
{
   public class Setup:DataBase
    {
        private IEnumerable<DBSchemaItemRef> tables;
        DBDatabase db;
        public Setup(string conn) {
            InitDbs(conn);
            db = base.Database;
            DBSchemaProvider provider = db.GetSchemaProvider();
             tables = provider.GetAllTables();            
        }
        public void Clear() {
            DBQuery[] all = new DBQuery[] {
            DBQuery.Drop.Table(base.Schema, TzAccount.Client.Table).IfExists(),
            DBQuery.Drop.Table(base.Schema, TzAccount.User.Table).IfExists(),
            DBQuery.Drop.Table(base.Schema, TzAccount.Server.Table).IfExists(),
            DBQuery.Drop.Table(base.Schema, TzAccount.Tables.Table).IfExists(),
            DBQuery.Drop.Table(base.Schema, TzAccount.Field.Table).IfExists(),
            DBQuery.Drop.Table(base.Schema, TzAccount.ClientServer.Table).IfExists(),
            DBQuery.Drop.Table(base.Schema, TzAccount.DataScript.Table).IfExists(),
            DBQuery.Drop.Table(base.Schema, TzAccount.ScriptIntend.Table).IfExists(),
            };
            foreach (DBQuery q in all) {
                try
                {
                    db.ExecuteNonQuery(q);
                }
                catch (System.Exception ex)
                {              
                }
            }
        }
        public void CreateAccount() {            
         //   DBSchemaItemRef table =   tables.Where(x => x.Name.ToLower() == TzAccount.Account.Table.ToLower()).FirstOrDefault ();
         ////  Tech.Data.Schema.DBSchemaTable table = provider.GetTable("", base.Schema, TzAccount.Account.Table);
         //   if (table == null)
         //   {
         //       DBQuery create;
         //       create = DBQuery.Create.Table(base.Schema, TzAccount.Account.Table)
         //                               .Add(TzAccount.Account.AccountID)
         //                               .Add(TzAccount.Account.Name)
         //                               .Add(TzAccount.Account.AccountNo)
         //                               .Add(TzAccount.Account.Status)
         //                               .Add(TzAccount.Account.NoOfUsers);
         //       db.ExecuteNonQuery(create);
         //   }
         //   else {
         //       throw new Exception("Account table Name exist");
         //   }
            //if (IsColumnExist(table, TzAccount.Account.AccountID, TzAccount.Account.AccountNo, TzAccount.Account.Name,
            //    TzAccount.Account.NoOfUsers,
            //    TzAccount.Account.Status))
            //{
            //    throw new Exception("Table Name exist");
            //}
            //else {             
                
            //}
        }
        public void CreateClient() {
            DBSchemaItemRef table = tables.Where(x => x.Name.ToLower() == TzAccount.Client .Table.ToLower()).FirstOrDefault();            
            if (table == null)
            {
                DBQuery create;
                create = DBQuery.Create.Table(base.Schema, TzAccount.Client.Table)
                                        .Add(TzAccount.Client.ClientID)
                                        .Add(TzAccount.Client.ClientName)
                                        .Add(TzAccount.Client.ClientNo)
                                        .Add(TzAccount.Client.Address)
                                        .Add(TzAccount.Client.State)
                                        .Add(TzAccount.Client.Email)
                                        .Add(TzAccount.Client.Country)
                                        .Add(TzAccount.Client.PhoneNo)
                                        .Add(TzAccount.Client.Status)
                                        .Add(TzAccount.Client.Host)
                                        //.Add(TzAccount.Client.ServerID)
                                        .Add(TzAccount.Client.OrganizationName);
                db.ExecuteNonQuery(create);
            }
            else
            {
                throw new System.Exception("Client table Name exist");
            }
        }
        public void CreateServer() {
            DBSchemaItemRef table = tables.Where(x => x.Name.ToLower() == TzAccount.Server.Table.ToLower()).FirstOrDefault();
            if (table == null)
            {
                DBQuery create;
                create = DBQuery.Create.Table(base.Schema, TzAccount.Server.Table)
                                        .Add(TzAccount.Server.ServerID)
                                        .Add(TzAccount.Server.ServerName)
                                        .Add(TzAccount.Server.Host)
                                        .Add(TzAccount.Server.Port)
                                        .Add(TzAccount.Server.UserID)
                                        .Add(TzAccount.Server.Password)
                                        .Add(TzAccount.Server.DB);
                db.ExecuteNonQuery(create);
            }
            else
            {
                throw new System.Exception("Server table Name exist");
            }
        }

        public void CreateUser()
        {
            DBSchemaItemRef table = tables.Where(x => x.Name.ToLower() == TzAccount.User.Table.ToLower()).FirstOrDefault();
            if (table == null)
            {
                DBQuery create;
                create = DBQuery.Create.Table(base.Schema, TzAccount.User.Table)                                        
                                        .Add(TzAccount.User.UserID)
                                        .Add(TzAccount.User.UserName)
                                        .Add(TzAccount.User.Password)
                                        .Add(TzAccount.User.Email)
                                        .Add(TzAccount.User.FirstName)
                                        .Add(TzAccount.User.LastName)
                                        .Add(TzAccount.User.Status)
                                        .Add(TzAccount.User.UserType);
                db.ExecuteNonQuery(create);
            }
            else
            {
                throw new System.Exception("User table Name exist");
            }
        }

        public void CreateTable()
        {
            DBSchemaItemRef table = tables.Where(x => x.Name.ToLower() == TzAccount.Tables.Table.ToLower()).FirstOrDefault();
            if (table == null)
            {
                DBQuery create;
                create = DBQuery.Create.Table(base.Schema, TzAccount.Tables.Table)
                                        .Add(TzAccount.Tables.TableID)
                                        .Add(TzAccount.Tables.ServerID)
                                         .Add(TzAccount.Tables.ClientID)
                                        .Add(TzAccount.Tables.TableName)
                                        .Add(TzAccount.Tables.Category)                                      
                                        ;
                db.ExecuteNonQuery(create);
            }
            else
            {
                throw new System.Exception("'Table' table Name exist");
            }
        }
        public void CreateField()
        {
            DBSchemaItemRef table = tables.Where(x => x.Name.ToLower() == TzAccount.Field.Table.ToLower()).FirstOrDefault();
            if (table == null)
            {
                DBQuery create;
                create = DBQuery.Create.Table(base.Schema, TzAccount.Field.Table)
                                        .Add(TzAccount.Field.TableID)
                                        .Add(TzAccount.Field.FieldID)
                                        .Add(TzAccount.Field.FieldName)
                                        .Add(TzAccount.Field.FieldType)
                                        .Add(TzAccount.Field.Length)
                                        .Add(TzAccount.Field.IsNullable)
                                        .Add(TzAccount.Field.ISPrimaryKey)
                                        ;
                db.ExecuteNonQuery(create);
            }
            else
            {
                throw new System.Exception("'Fields' table Name exist");
            }
        }
        public void CreateClientServer()
        {
            DBSchemaItemRef table = tables.Where(x => x.Name.ToLower() == TzAccount.ClientServer.Table.ToLower()).FirstOrDefault();
            if (table == null)
            {
                DBQuery create;
                create = DBQuery.Create.Table(base.Schema, TzAccount.ClientServer.Table)
                                        .Add(TzAccount.ClientServer.ClientID)
                                        .Add(TzAccount.ClientServer.ServerID);                                        
                db.ExecuteNonQuery(create);
            }
            else
            {
                throw new System.Exception("'Client Server' table Name exist");
            }
        }

        public void CreateDataScript() {
            DBSchemaItemRef table = tables.Where(x => x.Name.ToLower() == TzAccount.DataScript.Table.ToLower()).FirstOrDefault();
            if (table == null)
            {
                DBQuery create;
                create = DBQuery.Create.Table(base.Schema, TzAccount.DataScript.Table)
                                        .Add(TzAccount.DataScript.ScriptID)
                                         .Add(TzAccount.DataScript.ScriptName)
                                          .Add(TzAccount.DataScript.Category)
                                        .Add(TzAccount.DataScript.Script);
                db.ExecuteNonQuery(create);
            }
            else
            {
                throw new System.Exception("'Client Server' table Name exist");
            }
        }
        public void CreateScriptIntend()
        {
            DBSchemaItemRef table = tables.Where(x => x.Name.ToLower() == TzAccount.ScriptIntend.Table.ToLower()).FirstOrDefault();
            if (table == null)
            {
                DBQuery create;
                create = DBQuery.Create.Table(base.Schema, TzAccount.ScriptIntend.Table)
                                        .Add(TzAccount.ScriptIntend.ScriptID)
                                         .Add(TzAccount.ScriptIntend.Intend)
                                          
                                        ;
                db.ExecuteNonQuery(create);
            }
            else
            {
                throw new System.Exception("'Script intend' table Name exist");
            }
        }
        private bool IsColumnExist(DBSchemaTable table, params DBColumn[] columns)
        {
            foreach (DBColumn col in columns)
            {
                if (table.Columns.Contains(col.Name)) {
                    return true;
                };                              
            }
            return false;
        }
    }
}
