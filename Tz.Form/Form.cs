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
        public List<UIAction.Action> Actions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        public Form(string clientid,List<UIFormKey> formKeys)
        {
            this.ClientID = clientid;
            this.FormKeys = formKeys;
            FormProperties = new FormProperty();
            Actions = new List<UIAction.Action>();
                FormKeys = new List<UIFormKey>();
            this.ComponentID = "";
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
                this.FormProperties.Location = dr["Location"] == null ? "" : (string)dr["Location"];
                this.FormProperties.IPAddress = dr["IPAddress"] == null ? "" : (string)dr["IPAddress"];
            }
        }
        public void LoadFormFields()
        {
            Data.UIForm.UIFields form = new Data.UIForm.UIFields(Common.GetConnection(this.ClientID));
          DataTable dt=  form.GetFormFields(this.ClientID, this.FormID);
            FormFields = new List<FormField>();
            foreach (System.Data.DataRow row in dt.Rows)
            {
                var ff = new FormField(this.ClientID);
                ff.FormFieldID = row["FieldID"] == null ? "" : (string)row["FieldID"];
                ff.DataField = row["DataField"] == null ? "" : (string)row["DataField"];
                ff.FieldRenderType = row["FieldRenderType"] == null ? RenderType.TEXT : (RenderType)row["FieldRenderType"];
                ff.Category = row["Category"] == null ? RenderCategory.text : (RenderCategory)row["Category"];
                ff.Left = row["Left"] == null ? 0 : (int)row["Left"];
                ff.Top = row["Top"] == null ? 0 : (int)row["Top"];
                var fatt = row["FieldAttribute"] == null ? "" : (string)row["FieldAttribute"];
                ff.FieldAttribute = Newtonsoft.Json.JsonConvert.DeserializeObject<ComponentAttribute>(fatt);
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
                    this.FormProperties.IPAddress,
                    this.FormProperties.Location
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
                return form.Update(this.ClientID, this.Name, this.FormID, Newtonsoft.Json.JsonConvert.SerializeObject(this.FormKeys),
                     this.FormProperties.SuccessMessage,
                    this.FormProperties.CaptureLocation,
                    this.FormProperties.CaptureIPaddress,
                    this.FormProperties.ErrorMessage,
                    this.FormProperties.EnableDefaultAction,
                    this.FormProperties.Submit,
                    this.FormProperties.Reset,
                    this.FormProperties.Update,
                    this.FormProperties.Cancel,
                    this.FormProperties.IPAddress,
                    this.FormProperties.Location);
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
        public string IPAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Location { get; set; }
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
            IPAddress = "";
            Location = "";
            ErrorMessage = "";
            Submit = "";
            Reset = "";
            Update = "";
            Cancel = "";
            EnableDefaultAction = false;

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
