using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tz.UIForms.FormFields;

namespace Tz.UIForms
{
    public interface IFormFieldBuilder {
          FormField GetField(string clientid, string formid, RenderType renderType);
        bool SaveField();

    }
    public class FormFieldBuilder: IFormFieldBuilder
    {
        public FormField GetField(string clientid, string formid, RenderType renderType)
        {
            if (renderType == RenderType.SELECTION)
            {
                return new Selection(clientid, formid);
            }
            else if (renderType == RenderType.BOOLEAN)
            {
                return new FormFields.Boolean(clientid, formid);
            }
            else if (renderType == RenderType.TEXT)
            {
                return new FormFields.Text(clientid, formid);
            }
            else if (renderType == RenderType.PICKER)
            {
                return new FormFields.Picker(clientid, formid);
            }
            else if (renderType == RenderType.UPLOAD)
            {
                return new FormFields.Upload(clientid, formid);
            }
            else
                return null;
        }

        public bool SaveField()
        {
            throw new NotImplementedException();
        }
    }
}   
