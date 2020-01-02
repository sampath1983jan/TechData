using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.UIForms.FormFields
{
    public enum SelectionType
    {
        SINGLE = 0,
        MULTIPLE = 1
    }
    public class Selection : FormField
    {
        /// <summary>
        /// 
        /// </summary>
        public SelectionType SeletionType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ValueField;
        /// <summary>
        /// 
        /// </summary>
        public string DisplayField;
        /// <summary>
        /// 
        /// </summary>
        public string DefineSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LookUpSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ComponentSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool AllowOrderbyAlphabet { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// 

        public Selection(string clientid) : base(clientid, "")
        {
            FieldRenderType = RenderType.SELECTION;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="formid"></param>
        public Selection(string clientid, string formid) : base(clientid, formid)
        {
            FieldRenderType = RenderType.SELECTION;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        new public bool Save() {
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
