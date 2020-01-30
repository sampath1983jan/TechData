using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.UIForms.FormFields
{
  public class Text:FormField
    {    
        /// <summary>
        /// 
        /// </summary>
        public int Min { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Max { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Text() : base("", "")
        {
            FieldRenderType = RenderType.TEXT;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        public Text(string clientid) : base(clientid, "")
        {
            FieldRenderType = RenderType.TEXT;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="formid"></param>
        public Text(string clientid, string formid) : base(clientid, formid)
        {
            FieldRenderType = RenderType.TEXT;
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
            this.FormFieldID =    uIFields.Save(this.FormID,
                              this.ClientID, (int)this.FieldRenderType,
                             this.Attribute.DataField, Newtonsoft.Json.JsonConvert.SerializeObject(this)
                              );
                if (this.FormFieldID == "") { return false; }
                
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

