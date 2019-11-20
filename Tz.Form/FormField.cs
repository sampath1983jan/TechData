using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tz.Core;

namespace Tz.UIForms
{
    public enum RenderType {
        TEXT=0,
        SELECTION=1,
        PICKER=2,
        UPLOAD=3,
        BOOLEAN=4,
    }

    public enum RenderCategory {
        text,
        textarea,
        number,
        floats,
        currency,
        number_range,


        autocomplete,
        dropdown,
        select_with_grid,
        list,
        

        image,
        file,
        audio,
        video,

        date,
        date_time,
        date_range,
        time,
        time_range,
        week_picker,
        year_picker,
        month_picker,
        monthyear_picker,
        quarter_picker,

       yes_no,
       question,


    }
        
   public class FormField: ComponentAttribute
    {
        private string _formid;
        public string FormID { get { return _formid; } }
        public string FormFieldID { get; set; }
        public RenderType FieldERenderType { get; set; }
        public RenderCategory Category { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="formID"></param>
        public FormField(string clientID, string formID) : base(clientID)
        {
            _formid = formID;
            ClientID = clientID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formid"></param>
        internal void SetForm(string formid) {
            _formid = formid;
        }
      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
        internal bool Save() {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool Remove() {
            return true;
        }
    }
}
