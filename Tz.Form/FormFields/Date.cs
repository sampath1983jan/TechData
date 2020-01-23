using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.UIForms.FormFields
{
   public class Date : FormField
    {
        /// <summary>
        /// 
        /// </summary>
        public string DateFormat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TimeFormat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DateTimeFormat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pickerInterval { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AllowedDays { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AllowedFromHours { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AllowedToHours { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        public Date(string clientid) : base(clientid, "")
        {
            FieldRenderType = RenderType.PICKER;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="formid"></param>
        public Date(string clientid, string formid) : base(clientid, formid)
        {
            FieldRenderType = RenderType.PICKER;
        }
        public Date() : base("")
        {
            FieldRenderType = RenderType.PICKER;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Save()
        {
            Data.UIForm.UIFields uIFields = new Data.UIForm.UIFields(UIForms.Common.GetConnection(this.ClientID));
            if (this.FormFieldID == "")
            {
                uIFields.Save(this.FormID,
                              this.ClientID, (int)this.FieldRenderType,
                             this.Attribute.DataField, Newtonsoft.Json.JsonConvert.SerializeObject(this)
                              );
            }
            else
            {
                uIFields.Update(this.FormID,
                    this.FormFieldID,
                    this.ClientID,
                    (int)this.FieldRenderType,
                    Newtonsoft.Json.JsonConvert.SerializeObject(this));
            }
            return true;
        }
    }
}
