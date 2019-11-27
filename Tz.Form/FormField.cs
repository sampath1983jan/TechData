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
        SELECTION=1,
        PICKER=2,
        UPLOAD=3,
        BOOLEAN=4,
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

        image,
        file,
        audio,
        video,

        date,
        date_time,
        date_range,
        time,
        time_range,
        week_picker,
        year_picker,
        month_picker,
        monthyear_picker,
        quarter_picker,

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
        public RenderCategory Category { get; set; }
        public float Left { get; set; }
        public float Top { get; set; }
        public string DataField { get; set; }
        public ComponentAttribute FieldAttribute { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="formID"></param>
        public FormField(string clientID, string formID) 
        {
            _formid = formID;
            ClientID = clientID;
            Load();
        }
        public FormField(string clientid) {
            ClientID = clientid;
        }
        public FormField() {

        }
        private void Load() {
            Data.UIForm.UIFields uIFields = new Data.UIForm.UIFields(UIForms.Common.GetConnection(this.ClientID));
            System.Data.DataTable dt = new System.Data.DataTable();
           dt=  uIFields.GetField(this.ClientID, this.FormID,this.FormFieldID);
            foreach (System.Data.DataRow row in dt.Rows) {
                this.FormFieldID = row["FieldID"] == null ? "" : (string)row["FieldID"];
                this.DataField = row["DataField"] == null ? "" : (string)row["DataField"];
                this.FieldRenderType = row["FieldRenderType"] == null ? RenderType.TEXT : (RenderType)row["FieldRenderType"];
                this.Category = row["Category"] == null ? RenderCategory.text : (RenderCategory)row["Category"];
                this.Left = row["Left"] == null ? 0 : (int)row["Left"];
                this.Top = row["Top"] == null ?  0 : (int)row["Top"];
                var fatt = row["FieldAttribute"] == null ? "" : (string)row["FieldAttribute"];
                FieldAttribute = Newtonsoft.Json.JsonConvert.DeserializeObject<ComponentAttribute>(fatt);
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
        internal bool Save() {
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
