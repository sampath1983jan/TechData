﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Tz.ClientManager;
namespace Tz.Core
{
    public class Lookup
    {

        /// <summary>
        /// 
        /// </summary>
        public string ClientID { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public string LookupID { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsCore { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastUPD { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<LookupItem> LookupItems;

        private Data.Component.LookUp dataLookup;
        private ClientServer c;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="lookupid"></param>
        public Lookup(string clientid, string lookupid) {
            ClientID = clientid;
          c = new ClientServer(clientid);
            LookupID = lookupid;
            LookupItems = new List<LookupItem>();
            dataLookup = new Data.Component.LookUp(c.GetServer().Connection());
            Load();
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        public Lookup(string clientid) {
            c = new ClientServer(clientid);
            ClientID = clientid;
            LookupID = "";
            Name = "";
            LookupItems = new List<LookupItem>();
            dataLookup = new Data.Component.LookUp(c.GetServer().Connection());
        }
        /// <summary>
        /// 
        /// </summary>
        public void Load() {
            DataTable dt = new DataTable();
            dt=    dataLookup.GetLookupItems(ClientID,LookupID);
            DataTable dtLookup = new DataTable();
            dtLookup= dt.DefaultView.ToTable(true, "ClientID", "LookupID", "Name", "Description", "IsCore","LastUPD","CreatedOn","Value");
            foreach (DataRow dr in dtLookup.Rows) {
                Name = dr["Name"] == null ? "" : dr["Name"].ToString();
                LookupID = dr["LookupID"] == null ? "" : dr["LookupID"].ToString();
                 IsCore = DBNull.Value.Equals(dr["IsCore"]) == true ? false : Convert.ToBoolean(dr["IsCore"]);
                Description = DBNull.Value.Equals(dr["Description"]) == true ? "" : dr["Description"].ToString();
                 CreatedOn = DBNull.Value.Equals(dr["CreatedOn"]) == true ? DateTime.Now : Convert.ToDateTime(dr["LastUPD"]);
                 LastUPD = DBNull.Value.Equals(dr["LastUPD"]) == true ? DateTime.Now : Convert.ToDateTime(dr["LastUPD"]);
             
            }
            foreach (DataRow dRow in dt.Rows) {                
               var LookUpItemID = dRow["ComponentLookupItemID"] == null ? "" : dRow["ComponentLookupItemID"].ToString();
                LookupItem li = new LookupItem(ClientID,LookUpItemID,LookupID);
                li.Description= dRow["ItemDescription"] == null ? "" : dRow["ItemDescription"].ToString();
                li.Label= dRow["Label"] == null ? "" : dRow["Label"].ToString();
                li.ShortLabel = dRow["ShortLabel"] == null ? "" : dRow["ShortLabel"].ToString();
                li.ParentID = dRow["ParentID"] == null ? "" : dRow["ParentID"].ToString();
                li.Order = dRow["Order"] == null ? 0 : Convert.ToInt32(dRow["Order"]);
                li.IsActive = dRow["IsActive"] == null ? false : Convert.ToBoolean(dRow["IsActive"]);
                Name = dRow["Name"] == null ? "" : dRow["Name"].ToString();
                li.Value = DBNull.Value.Equals(dRow["Value"]) == true ? "" : (string)(dRow["Value"]);
                this.LookupItems.Add(li);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public static List<Lookup> GetLookUps(string clientid,string lookupIds="") {
             ClientServer c = new ClientServer(clientid);
        var dataLookup = new Data.Component.LookUp(c.GetServer().Connection());
            DataTable dt= dataLookup.GetLookUpList(clientid, lookupIds);
           List< Lookup> lup = new List<Lookup>();
            foreach (DataRow dr in dt.Rows) {
                Lookup l = new Lookup(clientid);
                l.Name = dr["Name"] == null ? "" : dr["Name"].ToString();
                l.IsCore = DBNull.Value.Equals(dr["IsCore"]) == true ? false : Convert.ToBoolean( dr["IsCore"]);
                l.Description = DBNull.Value.Equals(dr["Description"]) == true ? "" : dr["Description"].ToString();
                l.CreatedOn = DBNull.Value.Equals(dr["CreatedOn"]) == true ? DateTime.Now : Convert.ToDateTime(dr["LastUPD"]);
                l.LastUPD = DBNull.Value.Equals(dr["LastUPD"]) == true ? DateTime.Now : Convert.ToDateTime( dr["LastUPD"]);
                l.LookupID = dr["LookUpID"] ==null? "": dr["LookUpID"].ToString();
           //     l.Value = DBNull.Value.Equals(dr["Value"]) == true ? "" : (string)(dr["Value"]);
                lup.Add(l);
            }
            return lup;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Save() {
            if (this.LookupID == "") {
                this.LookupID = dataLookup.SaveLookup(this.ClientID,
                 this.Name,
                 this.Description,
                 this.IsCore);
            }
            var conn = c.GetServer().Connection();
            if (this.LookupID != "")
            {
                dataLookup.UpdateLookup(this.ClientID, this.LookupID, this.Name, this.Description, this.IsCore);
                foreach (LookupItem itm in this.LookupItems)
                {
                    if (itm.LookUpItemID == "" || itm.LookUpItemID ==null) {
                        itm.Save(conn);
                    }                    
                }
                return true;
            }
            else {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="lookupitemid"></param>
        /// <returns></returns>
        public bool ChangeLookItemActive(bool isActive,string lookupitemid) {
            var item = this.LookupItems.Where(x => x.LookUpItemID == lookupitemid).FirstOrDefault();
            if (item != null)
            {
                return item.ChangeActive(c.GetServer().Connection(),isActive);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookupitemid"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool ChangeOrder(string lookupitemid, int order) {
            var item = this.LookupItems.Where(x => x.LookUpItemID == lookupitemid).FirstOrDefault();
            var max = this.LookupItems.Max(x=> x.Order);
            if (item != null)
            {
                var conn = c.GetServer().Connection();
             item.ChangeOrder(conn, order);
                
                for (int i = order; i <= max; i++) {
                    var itm = this.LookupItems.Where(x => x.Order == i && x.LookUpItemID != lookupitemid).FirstOrDefault();
                    if (itm != null)
                    {
                        itm.ChangeOrder(conn, itm.Order + 1);
                    }
                }
                
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Update() {
        return    dataLookup.UpdateLookup(this.ClientID,
                this.LookupID,
                this.Name,
                this.Description,
                this.IsCore);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Remove()
        {
            if (dataLookup.RemoveLookup(this.ClientID, this.LookupID))
            {
                dataLookup.RemoveAllLookupItems(this.ClientID, this.LookupID);
                return true;
            }
            else {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookupitemID"></param>
        /// <returns></returns>
        public bool RemoveLookupItem(string lookupitemID) {
            var item =this.LookupItems.Where(x => x.LookUpItemID == lookupitemID).FirstOrDefault();
            if (item != null)
            {
                return item.Remove(c.GetServer().Connection());
            }
            else {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookupitemid"></param>
        /// <param name="label"></param>
        /// <param name="shortlabel"></param>
        /// <param name="description"></param>
        /// <param name="parentid"></param>
        /// <param name="order"></param>
        public void AddLookupItem(string lookupitemid, string label,
            string shortlabel,
            string description,
            string parentid,
            int order,string value) {
            this.LookupItems.Add(new LookupItem(this.ClientID,
                lookupitemid,
                this.LookupID,
                label,
                shortlabel,
                description,
                parentid,
                order,
                true,value));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="shortlabel"></param>
        /// <param name="description"></param>
        /// <param name="parentid"></param>
        /// <param name="order"></param>
        public string SaveLookupItem(                        
            string label,
            string shortlabel,
            string description,
            string parentid     ,
            string value
            ) {

            return dataLookup.Save(this.ClientID,
                          label, shortlabel,
                          description,
                          parentid,
                          this.LookupID,
                         this.LookupItems.Count +1,
                          true,value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="shortlabel"></param>
        /// <param name="description"></param>
        /// <param name="parentid"></param>
        /// <param name="lookupitemid"></param>
        /// <returns></returns>
        public bool UpdateLookupItem(
        string label,
        string shortlabel,
        string description,
        string parentid,
        string lookupitemid,
            string value
        )
        {

            return dataLookup.Update(this.ClientID,
                lookupitemid,
                this.LookupID,
                label,
                shortlabel,
                description,
                parentid, value);
        }
    }
}
