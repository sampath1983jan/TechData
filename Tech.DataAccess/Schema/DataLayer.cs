using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data.Query;
using Tech.DataAccess.Tables;
namespace Tech.DataAccess.Schema
{
    public class DataLayer:DataBase
    {
        public DataLayer(string Conn) {
            base.InitDbs(Conn);
        }

        public void CreateSchool() {
            DBQuery create;
            create = DBQuery.Create.Table(Tech.DataAccess.DataBase.Schema, Tables.Schools.Table)
                                           .Add(Tables.Schools.ID)
                                           .Add(Tables.Schools.Name)
                                           .Add(Tables.Schools.Address)
                                           .Add(Tables.Schools.OwnerName)
                                           .Add(Tables.Schools.DateCreated);
             base.Database.ExecuteNonQuery(create, onerror =>
             {
                 throw new ArgumentException(onerror.Message , onerror.Exception);
             });
         
        }

        public void Insert(int id,
            string name,
            string address,
            string ownerName) {
            DBConst did = DBConst.Int32(id);
            DBConst dname = DBConst.String(name);
            DBConst daddress = DBConst.String(address);
            DBConst downerName = DBConst.String(ownerName);
            DBConst createDate = DBConst.DateTime(DateTime.Now.Date);

            DBQuery insert = DBQuery.InsertInto(Schema, Schools.Table)
                         .Fields(Schools.Name.Name,
                                 Schools.ID.Name,
                                 Schools.Address.Name,
                                 Schools.OwnerName.Name, Schools.DateCreated.Name)
                         .Values(dname,did,daddress,downerName,createDate);

            using (DbTransaction trans = this.Database.BeginTransaction())
            {

                Database.ExecuteNonQuery(trans, insert);
                trans.Commit();
            }            

        }

        public System.Data.DataTable Get() {
            DBQuery getcount = DBQuery.SelectAll().From(Tech.DataAccess.DataBase.Schema, Tables.Schools.Table);
            return  (System.Data.DataTable)(Database.GetDatatable(getcount));
            
        }
    }
}
