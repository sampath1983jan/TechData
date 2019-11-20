using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tz.ClientManager;
using System.Data;
using Tz.Controls;
namespace Tz.UIForms
{
 /// <summary>
 /// 
 /// </summary>
    public class Form:Webcontrol
    {
        /// <summary>
        /// 
        /// </summary>
        public string FormID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ClientID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String ComponentID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //new public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UIForms.FormType FormType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<UIFormKey> FormKeys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<FormField> FormFields { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        public Form(string clientid,List<UIFormKey> formKeys)
        {
            this.ClientID = clientid;
            this.FormKeys = formKeys;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="formid"></param>
        public Form(string clientid, string formid)
        {
            this.ClientID = clientid;
            this.FormID = formid;
        }
        /// <summary>
        /// 
        /// </summary>
        private void Load()
        {
            DataTable dt;
            string conn = Common.GetConnection(this.ClientID);
            Data.UIForm.Form form = new Data.UIForm.Form(Common.GetConnection(this.ClientID));
            dt = form.GetForm(this.ClientID, this.FormID);
            foreach (DataRow dr in dt.Rows)
            {
                this.Name = dr["Name"] == null ? "" : dr["Name"].ToString();
                this.ComponentID = dr["ComponentID"] == null ? "" : dr["ComponentID"].ToString();
                this.FormType = dr["FormType"] == null ? FormType.MAIN : (FormType)dr["FormType"];
                string fkey = dr["FormKeys"] == null ? "" : (string)dr["FormKeys"];
                this.FormKeys = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UIFormKey>>(fkey);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            Data.UIForm.Form form = new Data.UIForm.Form(Common.GetConnection(this.ClientID));
            if (this.FormID == "")
            {
                this.FormID = form.Save(this.ClientID, this.Name, this.ComponentID, (int)this.FormType,Newtonsoft.Json.JsonConvert.SerializeObject(this.FormKeys));
                if (this.FormID == "")
                {
                    return false;
                }
                else
                    return true;
            }
            else
            {
                return form.Update(this.ClientID, this.Name, this.FormID, Newtonsoft.Json.JsonConvert.SerializeObject(this.FormKeys));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Remove()
        {
            Data.UIForm.Form form = new Data.UIForm.Form(Common.GetConnection(this.ClientID));
            return form.Remove(this.ClientID, this.FormID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldid"></param>
        /// <returns></returns>
        public bool RemoveField(string fieldid)
        {
            var f = this.FormFields.Where(x => x.FormFieldID == fieldid).FirstOrDefault();
            if (f != null)
            {
                return f.Remove();
            }
            else
            {
                return false;
            }
        }
    }
    /// <summary>
    /// /
    /// </summary>
    public static class Common
    {
        public static string GetConnection(string clientID)
        {
            string conn;
            ClientServer ck;
            ck = new ClientServer(clientID);
            conn = ck.GetServer().Connection();
            return conn;
        }
    }
}
