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
   public class Form : DataBase
    {
        public Form(string conn)
        {
            InitDbs(conn);
        }

        public  DataTable GetForms(string clientid) {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.SelectAll(TzAccount.Form.Table).WhereField(TzAccount.Form.Table, TzAccount.ComponentAttribute.ClientID.Name,
               Compare.Equals, DBConst.String(clientid));
            return db.GetDatatable(select);
        }

        public DataTable GetForm(string clientid, string formid) {
            DBDatabase db;
            db = base.Database;
            DBQuery select;
            select = DBQuery.SelectAll(TzAccount.Form.Table).
               WhereField(TzAccount.Form.Table, TzAccount.Form.ClientID.Name, Compare.Equals, DBConst.String(clientid))
                .AndWhere(TzAccount.Form.Table, TzAccount.Form.FormID.Name, Compare.Equals, DBConst.String(formid));
            return db.GetDatatable(select);
        }
       
        public string Save(string clientid, string formName,
            string componentid,
            int formtype,
            string formKeys,
            string successmessage,
            bool capturelocation,
            bool captureip,
            string errormessage,
            bool enableDefault,
            string submit,
            string reset,
            string update,
            string cancel,
            string ipaddress,
            string location
           )
        {
            DBDatabase db;
            db = base.Database;

            string a = Shared.generateID();
            DBQuery insert = DBQuery.InsertInto(TzAccount.Form.Table).Fields(
             TzAccount.Form.ClientID.Name,
             TzAccount.Form.FormID.Name,
             TzAccount.Form.ComponentID.Name,
             TzAccount.Form.Name.Name,
             TzAccount.Form.FormType.Name,
             TzAccount.Form.FormKeys.Name,

             TzAccount.Form.SuccessMessage.Name,
             TzAccount.Form.CaptureLocation.Name,
             TzAccount.Form.CaptureIPaddress.Name,
             TzAccount.Form.ErrorMessage.Name,
             TzAccount.Form.EnableDefaultAction.Name,
             TzAccount.Form.Submit.Name,
              TzAccount.Form.Reset.Name,
             TzAccount.Form.Update.Name,
             TzAccount.Form.Cancel.Name,
             TzAccount.Form.IPAddress.Name,
             TzAccount.Form.Location.Name,

             TzAccount.Form.LastUPD.Name)
             .Values(
             DBConst.String(clientid),
             DBConst.String(a),
             DBConst.String(componentid),
             DBConst.String(formName),
              DBConst.Int32(formtype),
              DBConst.String(formKeys),

                DBConst.String(successmessage),
                   DBConst.Const(DbType.Boolean,capturelocation),
                     DBConst.Const(DbType.Boolean,captureip),
                      DBConst.String(errormessage),
                        DBConst.Const(DbType.Boolean,enableDefault),
                          DBConst.String(submit),
                            DBConst.String(reset),
                                DBConst.String(update),
                                    DBConst.String(cancel),
                                        DBConst.String(ipaddress),
                                            DBConst.String(location),

             DBConst.DateTime(DateTime.Now)

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

        public bool Update(string clientid, string formName,string formid,string formkeys,
               string successmessage,
            bool capturelocation,
            bool captureip,
            string errormessage,
            bool enableDefault,
            string submit,
            string reset,
            string update,
            string cancel,
            string ipaddress,
            string location
            )
        {
            DBDatabase db;
            db = base.Database;
            DBComparison client = DBComparison.Equal(DBField.Field(TzAccount.Form.ClientID.Name), DBConst.String(clientid));
            DBComparison form = DBComparison.Equal(DBField.Field(TzAccount.Form.FormID.Name), DBConst.String(formid));

            DBQuery upd = DBQuery.Update(TzAccount.Form.Table).Set(
            TzAccount.Form.Name.Name, DBConst.String(formName)
            ).Set(
            TzAccount.Form.FormKeys.Name, DBConst.String(formkeys)
            ).WhereAll(client, form);

            int i = db.ExecuteNonQuery(upd);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Remove(string clientID,
           string formid)
        {
            DBDatabase db;
            db = base.Database;
            DBConst dbClientID = DBConst.String(clientID);
            DBConst dbform = DBConst.String(formid);
            DBComparison client = DBComparison.Equal(DBField.Field(TzAccount.Form.ClientID.Name), dbClientID);
            DBComparison component = DBComparison.Equal(DBField.Field(TzAccount.Form.ComponentID.Name), dbform);
            DBQuery del = DBQuery.DeleteFrom(TzAccount.Form.Table)
                                .WhereAll(client, component);
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
