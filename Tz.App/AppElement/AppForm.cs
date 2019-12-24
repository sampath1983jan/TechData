using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.App.AppElement
{
    public class AppForm
    {
        private Tz.UIForms.Form _form;
        /// <summary>
        /// 
        /// </summary>
        public string ClientID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Tz.UIForms.Form Form { get { return _form; } }

        /// <summary>
        /// 
        /// </summary>
        public string ElementID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="appid"></param>
        /// <param name="elementID"></param>
        public AppForm(string clientid, string appid, string elementID) {
            this.ClientID = clientid;
            this.AppID = appid;
            this.ElementID = elementID;
            _form = new UIForms.Form(this.ClientID);
        }
        public AppForm(string clientid, string appid) {
            this.ClientID = clientid;
            this.AppID = appid;
            _form = new UIForms.Form(this.ClientID);
        }
        internal void Set(UIForms.Form form) {
            ElementID = form.FormID;
        _form = form;
        }
        internal void Load()
        {
            _form = new UIForms.Form(this.ClientID, this.ElementID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool Assign()
        {
            try
            {
                Data.App.App aa = new Data.App.App(Common.GetConnection(this.ClientID));
                if (aa.AssignElement(this.ClientID, this.AppID, (int)AppElementType.FORM, ElementID))
                {
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
