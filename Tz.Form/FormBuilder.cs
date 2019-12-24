using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.UIForms
{
    public enum FormType {
        MAIN   =0,
        SUBFORM=1,
        LISTFORM=2
    }
    public class FormBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        public IFormFieldBuilder _builder;
        /// <summary>
        /// 
        /// </summary>
        public Form UIForm;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="builder"></param>
        public FormBuilder(string clientid,string componentID,List<UIFormKey> formKeys, IFormFieldBuilder builder) {
            _builder = builder;
            UIForm = new Form(clientid, formKeys);
            UIForm.ComponentID = componentID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="formid"></param>
        /// <param name="builder"></param>
        public FormBuilder(UIForms.Form uIForms, IFormFieldBuilder builder) {
            UIForm = uIForms;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="renderType"></param>
        /// <returns></returns>
        public FormField NewField(RenderType renderType)
        {
            var a = _builder.GetField(UIForm.ClientID, UIForm.FormID, renderType);
            return a;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public FormBuilder AddField(FormField field)
        {
            UIForm.FormFields.Add(field);
            return this;
        }
        public bool Remove() {
           return UIForm.Remove();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool SaveField(FormField field) {
            UIForm.FormFields.Add(field);
            return field.Save();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Save() {
            if (UIForm.Save())
            {
                foreach (FormField ff in UIForm.FormFields)
                {
                    ff.SetForm(UIForm.FormID);
                    ff.Save();                   
                }
                return true;
            }
            else { return false; }
        }

    }
}
