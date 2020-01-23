using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.UIForms.FormFields
{
   public class Currency : Numeric
    {
        public string CurrencyType { get; set; }

        public Currency() : base("", "")
        {
            this.Attribute.Category = RenderCategory.currency;
        }

        public Currency(string clientid) : base(clientid, "")
        {
            this.Attribute.Category = RenderCategory.currency;
        }
        public Currency(string clientid, string formid) : base(clientid, formid)
        {
            this.Attribute .Category = RenderCategory.currency;
        }
        new public  bool Save()
        {
            Data.UIForm.UIFields uIFields = new Data.UIForm.UIFields(UIForms.Common.GetConnection(this.ClientID));
            if (this.FormFieldID != "")
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
