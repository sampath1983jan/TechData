
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Tech.Data;
using Tech.Data.Query;

namespace Tz.Data.Component
{
    public class ComponentAttribute:DataBase
    {
        public ComponentAttribute() {
            InitDbs("");
        }

        public DataTable GetAttributes( string componentID) {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.SelectAll().From(TzAccount.ComponentAttribute.Table)
               .WhereField(TzAccount.Component.Table, TzAccount.ComponentAttribute.ComponentID.Name, Compare.Equals, DBConst.String(componentID));
            return db.GetDatatable(select);
        }

        public bool Save(string clientid,
            string componentID,
            string FieldID,
            bool isrequired,
            bool iscore,
            bool isunique,
            bool isReadOnly,
            bool isSecuired,
            bool isAuto,
            int lookupid,
            string defaultValue,
            string fileExtension,
            string regExp) {

            DBDatabase db;
            db = base.Database;

            DBConst dbcomponentID = DBConst.String(componentID);
            DBConst dbclientid = DBConst.String(clientid);
            DBConst dbFieldID = DBConst.String(FieldID);
            DBConst dbisrequired = DBConst.Const(DbType.Boolean, isrequired);
            DBConst dbiscore = DBConst.Const(DbType.Boolean, iscore);
            DBConst dbisunique = DBConst.Const(DbType.Boolean, isunique);
            DBConst dbisReadOnly = DBConst.Const(DbType.Boolean, isReadOnly);
            DBConst dbisSecuired = DBConst.Const(DbType.Boolean, isSecuired);
            DBConst dbisAuto = DBConst.Const(DbType.Boolean, isAuto);
            DBConst dblookupid = DBConst.Int32(lookupid);
            DBConst dbdefaultValue = DBConst.String(defaultValue);
            DBConst dbfileExtension = DBConst.String(fileExtension);
            DBConst dbregExp = DBConst.String(regExp);

            DBQuery insert = DBQuery.InsertInto(TzAccount.ComponentAttribute.Table).Fields(
              TzAccount.ComponentAttribute.ComponentID.Name,
              TzAccount.ComponentAttribute.FieldID.Name,
              TzAccount.ComponentAttribute.IsRequired.Name,
              TzAccount.ComponentAttribute.IsCore.Name,
              TzAccount.ComponentAttribute.IsUnique.Name,
              TzAccount.ComponentAttribute.IsReadOnly.Name,
              TzAccount.ComponentAttribute.IsSecured.Name,
              TzAccount.ComponentAttribute.IsAuto.Name,
              TzAccount.ComponentAttribute.LookUpID.Name,
              TzAccount.ComponentAttribute.DefaultValue.Name, 
              TzAccount.ComponentAttribute.FileExtension.Name,
              TzAccount.ComponentAttribute.RegExp.Name
              )
              .Values(
              dbcomponentID,
              dbFieldID,
              dbisrequired,
              dbiscore,
              dbisunique,
              dbisReadOnly,
              dbisSecuired,
              dbisAuto,
              dblookupid,
              dbdefaultValue,
              dbfileExtension,
              dbregExp
              );
            int val = 0;
            using (DbTransaction trans = db.BeginTransaction())
            {
                val = db.ExecuteNonQuery(trans, insert);
                trans.Commit();
            }
            if (val > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool Update(string clientid,
            string componentID,
            string FieldID,
            bool isrequired,
            bool iscore,
            bool isunique,
            bool isReadOnly,
            bool isSecuired,
            bool isAuto,
            int lookupid,
            string defaultValue,
            string fileExtension,
            string regExp)
        {
            DBDatabase db;
            db = base.Database;

            DBConst dbcomponentID = DBConst.String(componentID);
            DBConst dbclientid = DBConst.String(clientid);
            DBConst dbFieldID = DBConst.String(FieldID);
            DBConst dbisrequired = DBConst.Const(DbType.Boolean, isrequired);
            DBConst dbiscore = DBConst.Const(DbType.Boolean, iscore);
            DBConst dbisunique = DBConst.Const(DbType.Boolean, isunique);
            DBConst dbisReadOnly = DBConst.Const(DbType.Boolean, isReadOnly);
            DBConst dbisSecuired = DBConst.Const(DbType.Boolean, isSecuired);
            DBConst dbisAuto = DBConst.Const(DbType.Boolean, isAuto);
            DBConst dblookupid = DBConst.Int32(lookupid);
            DBConst dbdefaultValue = DBConst.String(defaultValue);
            DBConst dbfileExtension = DBConst.String(fileExtension);
            DBConst dbregExp = DBConst.String(regExp);
            DBComparison componentid = DBComparison.Equal(DBField.Field(TzAccount.ComponentAttribute.ComponentID.Name), dbcomponentID);
            DBComparison fieldid = DBComparison.Equal(DBField.Field(TzAccount.ComponentAttribute.FieldID.Name), dbFieldID);

            DBQuery update = DBQuery.Update(TzAccount.ComponentAttribute.Table).Set(
          TzAccount.ComponentAttribute.ComponentID.Name, dbcomponentID
          ).Set(
          TzAccount.ComponentAttribute.FieldID.Name, dbFieldID
          ).Set(
          TzAccount.ComponentAttribute.IsRequired.Name, dbisrequired
          ).Set(
          TzAccount.ComponentAttribute.IsCore.Name, dbiscore
          ).Set(
          TzAccount.ComponentAttribute.IsUnique.Name, dbisunique
          ).Set(
          TzAccount.ComponentAttribute.IsRequired.Name, dbisrequired
          ).Set(
          TzAccount.ComponentAttribute.IsSecured.Name, dbisSecuired
          ).Set(
          TzAccount.ComponentAttribute.IsAuto.Name, dbisAuto
          ).Set(
          TzAccount.ComponentAttribute.LookUpID.Name, dblookupid
          ).Set(
          TzAccount.ComponentAttribute.DefaultValue.Name, dbdefaultValue
          ).Set(
          TzAccount.ComponentAttribute.FileExtension.Name, dbfileExtension
          ).Set(
          TzAccount.ComponentAttribute.RegExp.Name, dbregExp
          ).WhereAll(componentid,fieldid);

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

        public bool Remove(string fieldid, string componentID) {
            DBDatabase db;
            db = base.Database;
            DBConst dbfieldid = DBConst.String(fieldid);
            DBConst dbcomponentID = DBConst.String(componentID);
            DBComparison field = DBComparison.Equal(DBField.Field(TzAccount.ComponentAttribute.FieldID.Name), dbfieldid);
            DBComparison component = DBComparison.Equal(DBField.Field(TzAccount.ComponentAttribute.ComponentID.Name), dbcomponentID);

            DBQuery del = DBQuery.DeleteFrom(TzAccount.ComponentAttribute.Table)
                                .WhereAll(field, component);
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
