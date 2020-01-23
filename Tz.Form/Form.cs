using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tz.ClientManager;
using System.Data;
using Tz.Controls;
 
using Tz.UIAction;
using Tz.Core;
using Newtonsoft.Json;
using System.IO;

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
        public FormProperty FormProperties { get; set; }
     
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
        public List<UIAction.Action> Actions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        public Form(string clientid,List<UIFormKey> formKeys)
        {
            this.ClientID = clientid;
            this.Name = "";
            this.Description = "";
            this.ComponentID = "";
            this.FormID = "";
            this.FormKeys = formKeys;
            FormProperties = new FormProperty();
            Actions = new List<UIAction.Action>();              
            this.ComponentID = "";
            FormFields = new List<FormField>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        public Form(string clientid) {
            this.ClientID = clientid;           
            FormProperties = new FormProperty();
            Actions = new List<UIAction.Action>();
            FormKeys = new List<UIFormKey>();
            this.ComponentID = "";
            FormFields = new List<FormField>();
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
            FormProperties = new FormProperty();
            Actions = new List<UIAction.Action>();
            FormKeys = new List<UIFormKey>();
            this.ComponentID = "";
            FormFields = new List<FormField>();
            Load();
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
                this.Description = dr["Description"] == null ? "" : dr["Description"].ToString();
                this.ComponentID = dr["ComponentID"] == null ? "" : dr["ComponentID"].ToString();
                this.FormType = dr["FormType"] == null ? FormType.MAIN : (FormType)dr["FormType"];
                string fkey = dr["FormKeys"] == null ? "" : (string)dr["FormKeys"];
                this.FormKeys = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UIFormKey>>(fkey);
                this.FormProperties.CaptureLocation = dr["CaptureLocation"] == null ? false: (bool)dr["CaptureLocation"];
                this.FormProperties.CaptureIPaddress = dr["CaptureIPaddress"] == null ? false : (bool)dr["CaptureIPaddress"];
                this.FormProperties.ErrorMessage = dr["ErrorMessage"] == null ? "" : (string)dr["ErrorMessage"];
                this.FormProperties.EnableDefaultAction = dr["EnableDefaultAction"] == null ?false : (bool)dr["EnableDefaultAction"];
                this.FormProperties.Submit = dr["Submit"] == null ? "" : (string)dr["Submit"];
                this.FormProperties.Reset = dr["Reset"] == null ? "" : (string)dr["Reset"];
                this.FormProperties.Update = dr["Update"] == null ? "" : (string)dr["Update"];
                this.FormProperties.Cancel = dr["Cancel"] == null ? "" : (string)dr["Cancel"];             
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="formids"></param>
        /// <returns></returns>
        public static List<Form> GetForms(string clientid,string formids) {
            DataTable dt;
            var Forms = new List<Form>();
            string conn = Common.GetConnection(clientid);
            Data.UIForm.Form form = new Data.UIForm.Form(Common.GetConnection(clientid));
            dt = form.GetForms(clientid, formids);
            foreach (DataRow dr in dt.Rows) {
                var f = new Form(clientid);
                f.FormID = dr["FormID"] == null ? "" : dr["FormID"].ToString();
                f.Name = dr["Name"] == null ? "" : dr["Name"].ToString();
                f.Description = dr["Description"] == null ? "" : dr["Description"].ToString();
                f.ComponentID = dr["ComponentID"] == null ? "" : dr["ComponentID"].ToString();
                f.FormType =  dr["FormType"] == DBNull.Value ? UIForms.FormType.MAIN : (UIForms.FormType)Convert.ToInt32(dr["FormType"]);
                string fkey = dr["FormKeys"] == null ? "" : (string)dr["FormKeys"];
                f.FormKeys = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UIFormKey>>(fkey);
                f.FormProperties.CaptureLocation = dr["CaptureLocation"] == DBNull.Value ? false : Convert.ToBoolean(dr["CaptureLocation"]);
                f.FormProperties.CaptureIPaddress = dr["CaptureIPaddress"] == DBNull.Value ? false : Convert.ToBoolean(dr["CaptureIPaddress"]);
                f.FormProperties.ErrorMessage = dr["ErrorMessage"] == null ? "" : (string)dr["ErrorMessage"];
                f.FormProperties.EnableDefaultAction = dr["EnableDefaultAction"] == DBNull.Value ? false : Convert.ToBoolean(dr["EnableDefaultAction"]);
                f.FormProperties.Submit = dr["Submit"] == null ? "" : (string)dr["Submit"];
                f.FormProperties.Reset = dr["Reset"] == null ? "" : (string)dr["Reset"];
                f.FormProperties.Update = dr["Update"] == null ? "" : (string)dr["Update"];
                f.FormProperties.Cancel = dr["Cancel"] == null ? "" : (string)dr["Cancel"];
                Forms.Add(f);
            }
            return Forms;
        }
        /// <summary>
        /// 
        /// </summary>
        public void LoadFormFields()
        {
            Data.UIForm.UIFields form = new Data.UIForm.UIFields(Common.GetConnection(this.ClientID));
          DataTable dt=  form.GetFormFields(this.ClientID, this.FormID);
            FormFields = new List<FormField>();
            foreach (System.Data.DataRow row in dt.Rows)
            {
                var serial = new JsonSerializer();
                serial.Formatting = Formatting.Indented;
                var ff = new FormField(this.ClientID);
                RenderType rt = row["FieldRenderType"] == null ? RenderType.TEXT : (RenderType)row["FieldRenderType"];
                var fatt = row["FieldAttribute"] == null ? "" : (string)row["FieldAttribute"];
                if (rt == RenderType.BOOLEAN)
                {
                    using (var sr = new StringReader(fatt))
                    using (var jr = new JsonTextReader(sr))
                        ff = serial.Deserialize<FormFields.Boolean>(jr);
                }
                else if (rt == RenderType.NUMBER)
                {
                    using (var sr = new StringReader(fatt))
                    using (var jr = new JsonTextReader(sr))
                        ff = serial.Deserialize<FormFields.Numeric>(jr);
                  //  ff = Newtonsoft.Json.JsonConvert.DeserializeObject<FormFields.Numeric>(fatt);
                }
                else if (rt == RenderType.PICKER)
                {
                    using (var sr = new StringReader(fatt))
                    using (var jr = new JsonTextReader(sr))
                        ff = serial.Deserialize<FormFields.Date>(jr);

                  //  ff = Newtonsoft.Json.JsonConvert.DeserializeObject<FormFields.Date>(fatt);
                }
                else if (rt == RenderType.SELECTION)
                {
                    using (var sr = new StringReader(fatt))
                    using (var jr = new JsonTextReader(sr))
                        ff = serial.Deserialize<FormFields.Selection>(jr);
                   // ff = Newtonsoft.Json.JsonConvert.DeserializeObject<FormFields.Selection>(fatt);
                }
                else if (rt == RenderType.TEXT)
                {
                    using (var sr = new StringReader(fatt))
                    using (var jr = new JsonTextReader(sr))
                        ff = serial.Deserialize<FormFields.Text>(jr);
                   // ff = Newtonsoft.Json.JsonConvert.DeserializeObject<FormFields.Text>(fatt);

                }
                else if (rt == RenderType.UPLOAD) {
                    using (var sr = new StringReader(fatt))
                    using (var jr = new JsonTextReader(sr))
                        ff = serial.Deserialize<FormFields.Upload>(jr);
                  //  ff = Newtonsoft.Json.JsonConvert.DeserializeObject<FormFields.Upload>(fatt);
                }              
                ff.SetForm(this.FormID);
                ff.FormFieldID = row["FieldID"] == null ? "" : (string)row["FieldID"];
                //   ff.Attribute.DataField = row["DataField"] == null ? "" : (string)row["DataField"];
                ff.FieldRenderType = rt;
                FormFields.Add(ff);
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
                this.FormID = form.Save(this.ClientID, 
                    this.Name,
                    this.ComponentID,
                    (int)this.FormType,Newtonsoft.Json.JsonConvert.SerializeObject(this.FormKeys),
                    this.FormProperties.SuccessMessage,
                    this.FormProperties.CaptureLocation,
                    this.FormProperties.CaptureIPaddress,
                    this.FormProperties.ErrorMessage,
                    this.FormProperties.EnableDefaultAction,
                    this.FormProperties.Submit,
                    this.FormProperties.Reset,
                    this.FormProperties.Update,
                    this.FormProperties.Cancel,
                    this.Description
                    );
                if (this.FormID == "")
                {
                    return false;
                }
                else
                    return true;
            }
            else
            {
                return form.Update(this.ClientID, this.Name,this.Description, this.FormID, Newtonsoft.Json.JsonConvert.SerializeObject(this.FormKeys),
                     this.FormProperties.SuccessMessage,
                    this.FormProperties.CaptureLocation,
                    this.FormProperties.CaptureIPaddress,
                    this.FormProperties.ErrorMessage,
                    this.FormProperties.EnableDefaultAction,
                    this.FormProperties.Submit,
                    this.FormProperties.Reset,
                    this.FormProperties.Update,
                    this.FormProperties.Cancel,
                    this.ComponentID,
                    Newtonsoft.Json.JsonConvert.SerializeObject(this.FormKeys));
            }
        }

        public bool SaveComponent(string compID) {
            if (this.ComponentID == "")
            {
                Data.UIForm.Form form = new Data.UIForm.Form(Common.GetConnection(this.ClientID));
                form.UpdateComponent(this.ClientID, this.FormID, compID);
                this.ComponentID = compID;
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool Remove()
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
    
    public class FormProperty {
        /// <summary>
        /// 
        /// </summary>
        public string SuccessMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool CaptureLocation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool CaptureIPaddress { get; set; }  
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Submit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Reset { get; set; }
        /// <summary>
        /// /
        /// </summary>
        public string Update { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Cancel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool EnableDefaultAction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public   FormProperty() {
            SuccessMessage = "";
            CaptureIPaddress = false;
            CaptureLocation = false;         
            ErrorMessage = "";
            Submit = "";
            Reset = "";
            Update = "";
            Cancel = "";
            EnableDefaultAction = true;
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
