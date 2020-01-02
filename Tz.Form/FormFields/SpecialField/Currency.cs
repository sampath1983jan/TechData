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

        public Currency(string clientid) : base(clientid, "")
        {
            this.Category = RenderCategory.currency;
        }
        public Currency(string clientid, string formid) : base(clientid, formid)
        {
            this.Category = RenderCategory.currency;
        }
        new public bool Save()
        {
            Data.UIForm.UIFields uIFields = new Data.UIForm.UIFields(UIForms.Common.GetConnection(this.ClientID));
            if (this.FormFieldID != "")
            {
                uIFields.Save(this.FormID,
                              this.ClientID, (int)this.FieldRenderType,
                              (int)this.Category,
                              this.Left,
                              this.Top, this.FieldAttribute.FieldID, Newtonsoft.Json.JsonConvert.SerializeObject(this.FieldAttribute)
                              , this.Width, this.Height);
            }
            else
            {
                uIFields.Update(this.FormID,
                    this.FormFieldID,
                    this.ClientID,
                    (int)this.FieldRenderType,
                    (int)this.Category,
                    this.Left,
                    this.Top,
                    Newtonsoft.Json.JsonConvert.SerializeObject(this.FieldAttribute)
                    , this.Width, this.Height);
            }
            return true;
        }
    }
}
