using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.UIForms.FormFields
{
    public class Boolean: FormField
    {
        public Boolean() : base("", "")
        {
            this.FieldRenderType = RenderType.BOOLEAN;
        }
        public Boolean(string clientid):base(clientid,"") {
            this.FieldRenderType = RenderType.BOOLEAN;
        }
        public Boolean(string clientid, string formid) : base(clientid, formid)
        {
            this.FieldRenderType = RenderType.BOOLEAN;
        }
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
