using System;
using System.Collections.Generic;
using System.Text;

namespace Tz.Core
{
   public class LookupItem
    {
        public string LookUpItemID { get; protected set; }
        public string ClientID { get; protected set; }
        public string LookUpID { get; protected set; }
        public string Label { get; set; }
        public string ShortLabel { get; set; }
        public string Description { get; set; }
        public string ParentID { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public LookupItem() {
            this.LookUpID = "";
            this.LookUpItemID = "";
            this.ClientID = "";
        }
        public LookupItem(string clientid,string lookupitemid,string lookupid) {
            this.LookUpID = lookupid;
            this.LookUpItemID = lookupitemid;
            this.ClientID = clientid;
        }
        public LookupItem(string clientid,
            string lookupitemid,
            string lookupid,
            string label,
            string shortlabel,
            string description,
            string parentid,
            int order,
            bool isActive)
        {
            this.LookUpID = lookupid;
            this.LookUpItemID = lookupitemid;
            this.ClientID = clientid;
            this.Label = label;
            this.ShortLabel = shortlabel;
            this.Description = description;
            this.ParentID = parentid;
            this.Order = order;
            this.IsActive = isActive;
        }

        public bool Remove(string conn) {
            var dataLookup = new Data.Component.LookUp(conn);
            return dataLookup.RemoveLookupItems(this.ClientID, this.LookUpItemID, this.LookUpID);
        }
        public bool ChangeActive(string conn,bool isActive) {
            var dataLookup = new Data.Component.LookUp(conn);
            return dataLookup.UpateActive(this.ClientID, this.LookUpItemID, isActive);
        }
        public bool ChangeOrder(string conn,int order)
        {
            var dataLookup = new Data.Component.LookUp(conn);
            return dataLookup.UpdateOrder(this.ClientID, this.LookUpItemID, order);
        }

        public bool Save(string conn) {
            var dataLookup = new Data.Component.LookUp(conn);
            if (dataLookup.Save(this.ClientID,
                        this.Label, this.ShortLabel,
                        this.Description,
                        this.ParentID,
                        this.LookUpID,
                        this.Order,
                        this.IsActive) != "")
            {
                return true;
            }
            else {
                return false;
            }

        }
        

    }
}
