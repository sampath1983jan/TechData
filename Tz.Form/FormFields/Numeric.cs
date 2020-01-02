using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.UIForms.FormFields
{
    public class Numeric:FormField
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public string DisplayFormat { get; set; }


        public Numeric(string clientid) : base(clientid, "")
        {
            FieldRenderType = RenderType.NUMBER
                ;
        }
        public Numeric(string clientid, string formid) : base(clientid, formid)
        {
            FieldRenderType = RenderType.NUMBER;
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
