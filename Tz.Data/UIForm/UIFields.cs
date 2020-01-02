using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tz.Global;
using System.Data;
using Tech.Data.Query;
using Tech.Data;
using System.Data.Common;

namespace Tz.Data.UIForm
{
    public class UIFields:DataBase
    {
        public UIFields(string conn)
        {
            InitDbs(conn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="formid"></param>
        /// <returns></returns>
        public DataTable GetFormFields(string clientid,string formid)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.SelectAll(TzAccount.FormFields.Table).
                From(TzAccount.FormFields.Table ).
                WhereField(TzAccount.FormFields.Table, TzAccount.FormFields.ClientID.Name,
               Compare.Equals, DBConst.String(clientid))
               .AndWhere( DBComparison.Equal(DBField.Field(TzAccount.FormFields.FormID.Name), DBConst.String(formid)));
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="formid"></param>
        /// <param name="fieldid"></param>
        /// <returns></returns>
        public DataTable GetField(string clientid,
            string formid,
            string fieldid)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.SelectAll(TzAccount.FormFields.Table).
                WhereField(TzAccount.FormFields.Table, TzAccount.FormFields.ClientID.Name,
               Compare.Equals, DBConst.String(clientid))
               .AndWhere(DBComparison.Equal(DBField.Field(TzAccount.FormFields.FormID.Name), DBConst.String(formid)))
               .AndWhere(DBComparison.Equal(DBField.Field(TzAccount.FormFields.FormID.Name), DBConst.String(fieldid)));
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formid"></param>
        /// <param name="clientid"></param>
        /// <param name="rendertype"></param>
        /// <param name="category"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public string Save(string formid,
            string clientid,
            int rendertype,
            int category,
            float left,
            float top,
            string attributeId,
            string attribute,
            int width,
            int height)
        {
            DBDatabase db;
            db = base.Database;

            string a = Shared.generateID();
            DBQuery insert = DBQuery.InsertInto(TzAccount.FormFields.Table).Fields(             
                TzAccount.FormFields.ClientID.Name,
                TzAccount.FormFields.FormID.Name,
             TzAccount.FormFields.FieldID.Name,
             TzAccount.FormFields.DataField.Name,
             TzAccount.FormFields.FieldRenderType.Name,
             TzAccount.FormFields.Category.Name,
              TzAccount.FormFields.Left.Name,
               TzAccount.FormFields.Top.Name,
                TzAccount.FormFields.FieldAttribute.Name,
             TzAccount.FormFields.LastUPD.Name,
             TzAccount.FormFields.CreatedDate.Name,
             TzAccount.FormFields.Height.Name,
             TzAccount.FormFields.Width.Name)
             .Values(
             DBConst.String(clientid),
             DBConst.String(formid),
             DBConst.String(a),
             DBConst.String(attributeId),
             DBConst.Int32(rendertype),
              DBConst.Int32(category),
              DBConst.Double(left),
              DBConst.Double(top),
              DBConst.String(attribute),
             DBConst.DateTime(DateTime.Now),
             DBConst.DateTime(DateTime.Now),
             DBConst.Int32(height), DBConst.Int32(width)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formid"></param>
        /// <param name="formfieldid"></param>
        /// <param name="clientid"></param>
        /// <param name="rendertype"></param>
        /// <param name="category"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public bool Update(string formid,
            string formfieldid,
            string clientid,
            int rendertype,
            int category,
            float left,
            float top,             
            string attribute,
            int width,
            int height)
        {
            DBDatabase db;
            db = base.Database;
            DBComparison client = DBComparison.Equal(DBField.Field(TzAccount.FormFields.ClientID.Name), DBConst.String(clientid));
            DBComparison form = DBComparison.Equal(DBField.Field(TzAccount.FormFields.FormID.Name), DBConst.String(formid));
            DBComparison formfield = DBComparison.Equal(DBField.Field(TzAccount.FormFields.FieldID.Name), DBConst.String(formfieldid));

            DBQuery update = DBQuery.Update(TzAccount.FormFields.Table).Set(
            TzAccount.FormFields.FieldRenderType.Name, DBConst.Int32(rendertype)
            ).Set(
            TzAccount.FormFields.Category.Name, DBConst.Int32(category)
            ).Set(
            TzAccount.FormFields.Left.Name, DBConst.Double(left)
            ).Set(
            TzAccount.FormFields.Top.Name, DBConst.Double(top)
            ).Set(
            TzAccount.FormFields.Height.Name, DBConst.Double(width)
            ).Set(
            TzAccount.FormFields.Width.Name, DBConst.Double(width)
            ).Set(
            TzAccount.FormFields.FieldAttribute.Name, DBConst.String(attribute)
            ).WhereAll(client, form, formfield);

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
        /// <param name="formid"></param>
        /// <param name="formfieldid"></param>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public bool Remove(string formid,
            string formfieldid,
            string clientid) {
            DBDatabase db;
            db = base.Database;
            DBComparison client = DBComparison.Equal(DBField.Field(TzAccount.FormFields.ClientID.Name), DBConst.String(clientid));
            DBComparison form = DBComparison.Equal(DBField.Field(TzAccount.FormFields.FormID.Name), DBConst.String(formid));
            DBComparison formfield = DBComparison.Equal(DBField.Field(TzAccount.FormFields.FieldID.Name), DBConst.String(formfieldid));
            DBQuery del = DBQuery.DeleteFrom(TzAccount.FormFields.Table)
                            .WhereAll(client, form, formfield);
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
