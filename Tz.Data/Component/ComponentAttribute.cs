
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Tech.Data;
using Tech.Data.Query;
using Tz.Data;
using Tz.Global;
namespace Tz.Data.Component
{
    public class ComponentAttribute:DataBase
    {
        public ComponentAttribute(string conn) {
            InitDbs(conn);
        }

        public DataTable GetAttributes( string componentID) {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.Select()
                .Field(TzAccount.Field.Table,TzAccount.Field.FieldName.Name)
                .Field(TzAccount.Field.Table, TzAccount.Field.FieldType.Name)
                .Field(TzAccount.Field.Table, TzAccount.Field.Length.Name)
                .Field(TzAccount.Field.Table, TzAccount.Field.IsNullable.Name)
                .Field(TzAccount.Field.Table, TzAccount.Field.ISPrimaryKey.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.FieldID.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.AttributeName.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.AttributeType.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.IsRequired.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.IsCore.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.IsUnique.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.IsAuto.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.IsSecured.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.IsReadOnly.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.FileExtension.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.LookUpID.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.RegExp.Name)
                .Field(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.DefaultValue.Name)
                .From(TzAccount.ComponentAttribute.Table).LeftJoin(TzAccount.Field.Table)
                .On(TzAccount.ComponentAttribute.Table,TzAccount.ComponentAttribute.FieldID.Name,Compare.Equals,
                TzAccount.Field.Table,TzAccount.Field.FieldID.Name)
               .WhereField(TzAccount.ComponentAttribute.Table, TzAccount.ComponentAttribute.ComponentID.Name, Compare.Equals, DBConst.String(componentID));
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
            string lookupid,
            string defaultValue,
            string fileExtension,
            string regExp,
            string AttributeName,
            int AttributeType) {
            lookupid= lookupid == null ? "" : lookupid;
            defaultValue = defaultValue == null ? "" : defaultValue;
            fileExtension = fileExtension == null ? "" : fileExtension;
            regExp = regExp == null ? "" : regExp;
            AttributeName = AttributeName == null ? "" : AttributeName;
            FieldID = FieldID == null ? "" : FieldID;

            DBDatabase db;
            db = base.Database;
            DBConst dbAttributeName = DBConst.String(AttributeName);
            DBConst dbcomponentID = DBConst.String(componentID);
            DBConst dbclientid = DBConst.String(clientid);
            DBConst dbFieldID = DBConst.String(FieldID);
            DBConst dbisrequired = DBConst.Const(DbType.Boolean, isrequired);
            DBConst dbiscore = DBConst.Const(DbType.Boolean, iscore);
            DBConst dbisunique = DBConst.Const(DbType.Boolean, isunique);
            DBConst dbisReadOnly = DBConst.Const(DbType.Boolean, isReadOnly);
            DBConst dbisSecuired = DBConst.Const(DbType.Boolean, isSecuired);
            DBConst dbisAuto = DBConst.Const(DbType.Boolean, isAuto);
            DBConst dblookupid = DBConst.String (lookupid);
            DBConst dbdefaultValue = DBConst.String(defaultValue);
            DBConst dbfileExtension = DBConst.String(fileExtension);
            DBConst dbregExp = DBConst.String(regExp);
            DBConst dbAttributeType = DBConst.Int32(AttributeType);


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
              TzAccount.ComponentAttribute.RegExp.Name,
              TzAccount.ComponentAttribute.ClientID.Name,
              TzAccount.ComponentAttribute.AttributeName.Name,
              TzAccount.ComponentAttribute.AttributeType.Name
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
              dbregExp,
              dbclientid,
              dbAttributeName,
              dbAttributeType
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
            string lookupid,
            string defaultValue,
            string fileExtension,
            string regExp,
            string attributeName)
        {
            lookupid = lookupid == null ? "" : lookupid;
            defaultValue = defaultValue == null ? "" : defaultValue;
            fileExtension = fileExtension == null ? "" : fileExtension;
            regExp = regExp == null ? "" : regExp;
            attributeName = attributeName == null ? "" : attributeName;
            FieldID = FieldID == null ? "" : FieldID;

            DBDatabase db;
            db = base.Database;
            DBConst dbattributeName = DBConst.String(attributeName);
            DBConst dbcomponentID = DBConst.String(componentID);
            DBConst dbclientid = DBConst.String(clientid);
            DBConst dbFieldID = DBConst.String(FieldID);
            DBConst dbisrequired = DBConst.Const(DbType.Boolean, isrequired);
            DBConst dbiscore = DBConst.Const(DbType.Boolean, iscore);
            DBConst dbisunique = DBConst.Const(DbType.Boolean, isunique);
            DBConst dbisReadOnly = DBConst.Const(DbType.Boolean, isReadOnly);
            DBConst dbisSecuired = DBConst.Const(DbType.Boolean, isSecuired);
            DBConst dbisAuto = DBConst.Const(DbType.Boolean, isAuto);
            DBConst dblookupid = DBConst.String(lookupid);
            DBConst dbdefaultValue = DBConst.String(defaultValue);
            DBConst dbfileExtension = DBConst.String(fileExtension);
            DBConst dbregExp = DBConst.String(regExp);
            DBComparison componentid = DBComparison.Equal(DBField.Field(TzAccount.ComponentAttribute.ComponentID.Name), dbcomponentID);
            DBComparison fieldid = DBComparison.Equal(DBField.Field(TzAccount.ComponentAttribute.FieldID.Name), dbFieldID);
            DBComparison Client = DBComparison.Equal(DBField.Field(TzAccount.ComponentAttribute.ClientID.Name), dbclientid);

            DBQuery update = DBQuery.Update(TzAccount.ComponentAttribute.Table).Set(
          TzAccount.ComponentAttribute.IsRequired.Name, dbisrequired
          ).Set(
          TzAccount.ComponentAttribute.AttributeName.Name, dbattributeName
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
          ).WhereAll(componentid,fieldid, Client);

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
        public bool RemoveAll(string componentID)
        {
            DBDatabase db;
            db = base.Database;
            //DBConst dbfieldid = DBConst.String(fieldid);
            DBConst dbcomponentID = DBConst.String(componentID);
            //DBComparison field = DBComparison.Equal(DBField.Field(TzAccount.ComponentAttribute.FieldID.Name), dbfieldid);
            DBComparison component = DBComparison.Equal(DBField.Field(TzAccount.ComponentAttribute.ComponentID.Name), dbcomponentID);

            DBQuery del = DBQuery.DeleteFrom(TzAccount.ComponentAttribute.Table)
                                .WhereAll( component);
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
