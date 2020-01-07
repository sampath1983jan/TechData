using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.App.AppElement
{
    public class AppLookup
    {
        private Tz.Core.Lookup lookup;

        public string ClientID { get; set; }
        public string AppID { get; set; }
        public string ElementID { get; set; }

        public Tz.Core.Lookup  Lookup {get  { return lookup; } }

        public AppLookup(string clientid, string appid, string elementID) {
            this.ClientID = clientid;
            this.AppID = appid;
            this.ElementID = elementID;
            lookup = new Core.Lookup(this.ClientID);

        }
        public AppLookup(string clientid, string appid)
        {
            this.ClientID = clientid;
            this.AppID = appid;
            this.ElementID = "";
            lookup = new Core.Lookup(this.ClientID);

        }
        internal void Set(Tz.Core.Lookup lk) {
            lookup = lk;
        }
        internal void Load()
        {
            lookup = new Tz.Core.Lookup(this.ClientID, this.ElementID);
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
                if (aa.AssignElement(this.ClientID, this.AppID, (int)AppElementType.LOOKUP, ElementID))
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool Remove()
        {
            var a = new Tz.Core.ComponentManager(this.ClientID, this.ElementID);
            return a.Remove();
        }
    }
}
