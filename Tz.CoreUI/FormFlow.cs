using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tz.Core;
using Tz.UIForms;
using Tz.Controls;
using Tz.UIAction;
using System.Data;
namespace Tz.CoreUI
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessFlow
    {
        /// <summary>
        /// 
        /// </summary>        
       public Component Component { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UIForms.Form Form { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ClientID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private UIForms.FormBuilder formBuilder;
        /// <summary>
        /// 
        /// </summary>
        private FormField CurrentField { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private List<UIAction.Action> Actions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Component> GetComponentList(string clientID) {
          return  Tz.Core.Component.GetComponentList(clientID);         
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="componentID"></param>
        public BusinessFlow(string clientid, string componentID) {
            this.ClientID = clientid;
            this.Component = new Component(this.ClientID, componentID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BusinessFlow NewForm() {
            var pkeys = this.Component.PrimaryKeys.Split(',');
            if (this.Component.PrimaryKeys == "")
                throw new Exception("Component must have atleast one key field.");
            var k = new List<UIFormKey>();
            formBuilder = new FormBuilder(this.ClientID, this.Component.ComponentID, k, new FormFieldBuilder());
            Form= formBuilder.UIForm;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fkey"></param>
        /// <returns></returns>
        public BusinessFlow NewForm(List<UIFormKey> fkey) {
            var pkeys = this.Component.PrimaryKeys.Split(',');
            foreach (UIFormKey fk in fkey) {
                if (fk.Key != "")
                {
                    var v = pkeys.Where(x => x == fk.Key).FirstOrDefault();
                    if (v == null)                     
                        throw new Exception("Key field dosn't Exist. Contact your system administrator");                    
                }
                else  
                    throw new Exception("Key field cannot be empty. Contact your system administrator");                          
            }
            formBuilder = new FormBuilder(this.ClientID, this.Component.ComponentID, fkey, new FormFieldBuilder());
            Form = formBuilder.UIForm;
            return this;
        }

        public BusinessFlow NewField(string componentField,RenderType renderType,RenderCategory category,
            float top,float left) {
            var f = this.Component.Attributes.Where(x => x.FieldID == componentField).FirstOrDefault();
            CurrentField= this.formBuilder.NewField(renderType);
            CurrentField.Category = category;
            CurrentField.Top = top;
            CurrentField.Left = left;
            CurrentField.DataField = f.FieldID;
            CurrentField.FieldAttribute = f;
            CurrentField.ClientID = this.ClientID;
            return this;
        }
        public FormField GetFormField(string formFieldid) {
           return this.Form.FormFields.Where(x => x.FormFieldID == formFieldid).FirstOrDefault();
        }
        public BusinessFlow SaveField() {
            if (this.Form.FormID != "")
            {
                formBuilder.SaveField(CurrentField);
                return this;
            }
            else {
                throw new Exception("Form doestn't exist. pleast contact system admin");
            }        
        }
        public BusinessFlow SaveForm() {
            this.formBuilder.Save();
            if (this.formBuilder.UIForm.ID != "")
            {
                return this;
            }
            else
                throw new Exception("Unable to save form. please contact system admin");             
        }
       
     }
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
