using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tz.Core;

namespace Tz.UIForms
{
    public enum RenderType {
        TEXT=0,
        SELECTION=1, // selection from the list
        PICKER=2,   // datepicker
        UPLOAD=3,
        BOOLEAN=4,
        NUMBER=5,
        TIMEPICKER=6,
     
    }
    public enum RenderCategory {
        text,
        textarea,
        number,
        decimals,
        currency,
        number_range,
        formula,

        autocomplete,
        dropdown,
        select_with_grid,
        list,
        choice,
        week_picker,
        year_picker,
        month_picker,
        quarter_picker,

        image,
        file,
        audio,
        video,

        date,
        date_time,
        date_range,
        time,
        time_range,      
        monthyear_picker,    

       yes_no,
       question,
     
    }        
   public class FormField
    {
        private string _formid;
        public string ClientID { get; set; }
        public string FormID { get { return _formid; } }
        public string FormFieldID { get; set; }
        public RenderType FieldRenderType { get; set; }
        public FormFieldAttribute Attribute { get; set; }      
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="formID"></param>
        public FormField(string clientID, string formID) 
        {
            _formid = formID;
            ClientID = clientID;
            this.FormFieldID = "";
              Attribute = new FormFieldAttribute();
            Load();
        }
        public FormField(string clientid) {
            Attribute = new FormFieldAttribute();
            this.FormFieldID = "";
            this._formid = "";
            ClientID = clientid;
        }
        public FormField() {

        }
        private void Load() {
            Data.UIForm.UIFields uIFields = new Data.UIForm.UIFields(UIForms.Common.GetConnection(this.ClientID));
            System.Data.DataTable dt = new System.Data.DataTable();
            if (this.FormFieldID != "") {
                dt = uIFields.GetField(this.ClientID, this.FormID, this.FormFieldID);
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    this.FormFieldID = row["FieldID"] == null ? "" : (string)row["FieldID"];                 
                    this.FieldRenderType = row["FieldRenderType"] == null ? RenderType.TEXT : (RenderType)row["FieldRenderType"];                    
                    var fatt = row["FieldAttribute"] == null ? "" : (string)row["FieldAttribute"];
                    var ff = Newtonsoft.Json.JsonConvert.DeserializeObject<FormField>(fatt);
                    this.Attribute = ff.Attribute;                    
                }
            }      
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formid"></param>
        internal void SetForm(string formid) {
            _formid = formid;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual bool Save() {
            return true;
        }
        public bool ChangeShape() {
            Data.UIForm.UIFields uIFields = new Data.UIForm.UIFields(UIForms.Common.GetConnection(this.ClientID));
            uIFields.ChangeShape(this.FormID
                , this.FormFieldID,
                this.ClientID, Newtonsoft.Json.JsonConvert.SerializeObject(this));
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool Remove() {
            Data.UIForm.UIFields uIFields = new Data.UIForm.UIFields(UIForms.Common.GetConnection(this.ClientID));
            uIFields.Remove(this.FormID,  
                this.FormFieldID, 
                this.ClientID);

            return true;
        }
    }
}
