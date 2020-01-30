using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.UIForms.FormFields
{
   public class Picker   : FormField
    {
        public Picker() : base("", "")
        {
            FieldRenderType = RenderType.PICKER;
        }
        public Picker(string clientid) : base(clientid, "")
        {
            FieldRenderType = RenderType.PICKER;
        }
        public Picker(string clientid, string formid) : base(clientid, formid)
        {
            FieldRenderType = RenderType.PICKER;
        }
        public override bool Save()
        {
            Data.UIForm.UIFields uIFields = new Data.UIForm.UIFields(UIForms.Common.GetConnection(this.ClientID));
            if (this.FormFieldID == "")
            {
                this.FormFieldID = uIFields.Save(this.FormID,
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
