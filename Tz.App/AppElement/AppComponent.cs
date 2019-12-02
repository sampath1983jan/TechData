using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.App.AppElement
{
    public class AppComponent
    {
        private Tz.Core.IComponent component;
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
        public string ElementID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  Core.IComponent Component { get { return component; } }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="appid"></param>
        /// <param name="elementID"></param>
        public AppComponent(string clientid, string appid,string elementID) {
            this.ClientID = clientid;
            this.AppID = appid;
          //  component = new Tz.Core.ComponentManager(this.ClientID, this.component.ComponentID).Component;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool Assign() {
         //   var a = new Tz.Core.ComponentManager(component);
            try
            {               
                    Data.App.App aa = new Data.App.App(Common.GetConnection(this.ClientID));
                if (aa.AssignElement(this.ClientID, this.AppID, (int)AppElementType.COMPONENT, ElementID))
                {
                    return true;
                }
                else return false;                 
                
            }
            catch (Exception ex) {
                throw ex;
            }            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool Remove() {
            var a = new Tz.Core.ComponentManager(this.ClientID, this.ElementID);
           return a.Remove();
        }
    }
}
