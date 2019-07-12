using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
namespace Tz.Core
{
public  class ComponentModal
    {
        private string ClientID { get; set; }
        private Data.Component.ComponentModal dataComponentModal;
        private Tz.Net.ClientServer c;
        private string conn;
        private string _componentModalID;


        /// <summary>
        /// 
        /// </summary>
        public string ComponentModalID { get; protected set; } 
        /// <summary>
        /// 
        /// </summary>
        public string ModalName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ParentComponent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ComponentNode> ComponentModalRoot;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>        /// 
        public ComponentModal(string clientID,string name,string category,string parentnode) {
            ClientID = clientID;
            c = new Net.ClientServer(ClientID);
            ModalName = name;
            Category = category;
            ParentComponent = parentnode;
            ComponentModalRoot = new List<ComponentNode>();
            conn = c.GetServer().Connection();
            dataComponentModal = new Data.Component.ComponentModal(c.GetServer().Connection());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>        /// 
        public ComponentModal(string clientID)
        {
            ClientID = clientID;
            c = new Net.ClientServer(ClientID);
            ModalName = "";
            Category = "";
            ParentComponent = "";
            ComponentModalRoot = new List<ComponentNode>();
            conn = c.GetServer().Connection();
            dataComponentModal = new Data.Component.ComponentModal(c.GetServer().Connection());
        }        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="componentModalID"></param>
        public ComponentModal(string clientID, string componentModalID) {
            this.ClientID = clientID;
            c = new Net.ClientServer(ClientID);
            ComponentModalID = componentModalID;
            conn = c.GetServer().Connection();
            dataComponentModal = new Data.Component.ComponentModal(conn);
            LoadModal();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public static List<ComponentModal> GetModalList(string clientid) {
            DataTable dt = new DataTable();
            DataTable dtModal = new DataTable();
            DataTable dtItem = new DataTable();
            DataTable dtRelation = new DataTable();
            List<ComponentModal> ModalList = new List<ComponentModal>();
            var c = new Net.ClientServer(clientid);
            var dataComponentModal = new Data.Component.ComponentModal(c.GetServer().Connection());
            dt = dataComponentModal.GetAllModal(clientid);
            dtModal = dt.DefaultView.ToTable(true, "Name", "Catgory", "ParentComponent", "ComponentModalID");

            dtItem = dt.DefaultView.ToTable(true, "ComponentID", "ChildComponentID", "ComponentModalItemID");
            dtRelation = dt.DefaultView.ToTable(true, "ComponentModalItemID", "ParentField", "ComponentModalID",
                "RelatedField", "ComponentModalRelationID");

            foreach (DataRow dr in dtModal.Rows)
            {
                var comp = new ComponentModal(clientid);

                comp.ModalName = dr.IsNull("Name") ? "" : dr["Name"].ToString();
                comp.Category = dr.IsNull("Catgory") ? "" : dr["Catgory"].ToString();
                comp.ParentComponent = dr.IsNull("ParentComponent") ? "" : dr["ParentComponent"].ToString();
                comp.ComponentModalID = dr.IsNull("ComponentModalID") ? "" : dr["ComponentModalID"].ToString();
                dt.DefaultView.RowFilter = "ComponentModalID =" + comp.ComponentModalID;
                dtItem = dt.DefaultView.ToTable(true);
                foreach (DataRow dRow in dtItem.Rows)
                {
                    var componentID = dRow.IsNull("ComponentID") ? "" : dRow["ComponentID"].ToString();
                    var ccomponentid = dRow.IsNull("ChildComponentID") ? "" : dRow["ChildComponentID"].ToString();
                    var cmodalitemid = dRow.IsNull("ComponentModalItemID") ? "" : dRow["ComponentModalItemID"].ToString();
                    ComponentNode cn = new ComponentNode(clientid, componentID, ccomponentid, cmodalitemid);
                    dtRelation.DefaultView.RowFilter = "ComponentModalItemID =" + cmodalitemid;
                    var dtr = dtRelation.DefaultView.ToTable(true);
                    dtRelation.DefaultView.RowFilter = "";
                    foreach (DataRow drNodeRe in dtr.Rows)
                    {
                        var mrid = drNodeRe.IsNull("ComponentModalRelationID") ? "" : drNodeRe["ComponentModalRelationID"].ToString();
                        var pf = drNodeRe.IsNull("ParentField") ? "" : drNodeRe["ParentField"].ToString();
                        var rf = drNodeRe.IsNull("RelatedField") ? "" : drNodeRe["RelatedField"].ToString();
                        cn.AddRelation(pf, rf, mrid);
                    }
                    comp.ComponentModalRoot.Add(cn);
                }
                ModalList.Add(comp);
            }
            return ModalList;
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void LoadModal() {
            DataTable dt = new DataTable();
            DataTable dtModal = new DataTable();
            DataTable dtItem = new DataTable();
            DataTable dtRelation = new DataTable();
           dt= dataComponentModal.GetModal(this.ClientID, ComponentModalID);
            dtModal= dt.DefaultView.ToTable(true, "Name", "Catgory", "ParentComponent");

            dtItem = dt.DefaultView.ToTable(true, "ComponentID", "ChildComponentID", "ComponentModalItemID");
            dtRelation = dt.DefaultView.ToTable(true, "ComponentModalItemID", "ParentField", 
                "RelatedField", "ComponentModalRelationID");

            foreach (DataRow dr in dtModal.Rows) {
                ModalName = dr.IsNull("Name") ? "" : dr["Name"].ToString();
                Category = dr.IsNull("Catgory") ? "" : dr["Catgory"].ToString();
                ParentComponent = dr.IsNull("ParentComponent") ? "" : dr["ParentComponent"].ToString();              
            }            
            foreach (DataRow dr in dtItem.Rows) {
                var componentID = dr.IsNull("ComponentID") ? "" : dr["ComponentID"].ToString();
                var ccomponentid = dr.IsNull("ChildComponentID") ? "" : dr["ChildComponentID"].ToString();
                var cmodalitemid = dr.IsNull("ComponentModalItemID") ? "" : dr["ComponentModalItemID"].ToString();
                ComponentNode cn = new ComponentNode(this.ClientID, componentID, ccomponentid, cmodalitemid);
                dtRelation.DefaultView.RowFilter = "ComponentModalItemID ="  + cmodalitemid;
                var dtr = dtRelation.DefaultView.ToTable(true);
                dtRelation.DefaultView.RowFilter = "";
                foreach (DataRow dRow in dtr.Rows) {
                    var mrid= dr.IsNull("ComponentModalRelationID") ? "" : dr["ComponentModalRelationID"].ToString();
                    var pf = dr.IsNull("ParentField") ? "" : dr["ParentField"].ToString();
                    var rf = dr.IsNull("RelatedField") ? "" : dr["RelatedField"].ToString();
                    cn.AddRelation(pf,rf,mrid);
                }                
                this.ComponentModalRoot.Add(cn);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Save() {

           this.ComponentModalID= dataComponentModal.Save(this.ClientID,
                this.ParentComponent,
                this.ModalName,
                this.Category);
            
            if (this.ComponentModalID != "") {
                foreach (ComponentNode cn in this.ComponentModalRoot) {
                    cn.SaveModalItem(conn);
                }
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Remove() {
            if (dataComponentModal.Remove(this.ClientID, this.ComponentModalID))
            {
                dataComponentModal.RemoveAllModalItem(this.ClientID, this.ComponentModalID);
                var a = "";
                foreach (ComponentNode cn in this.ComponentModalRoot)
                {
                    a = a + "," + cn.ComponentModalItemID;
                }
                if (a.StartsWith(","))
                {
                    a = a.Substring(1);
                }
                dataComponentModal.RemoveAllItemRelation(this.ClientID, a);
                return true;
            }
            else {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modalitemid"></param>
        /// <returns></returns>
        public bool RemoveItem(string modalitemid) {
            var a = this.ComponentModalRoot.Where(x => x.ComponentModalItemID == modalitemid).FirstOrDefault();
            if (a != null)
            {
                return a.RemoveItem(this.ComponentModalID);
            }
            else return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modalitemid"></param>
        /// <param name="relationshipid"></param>
        /// <returns></returns>
        public bool RemoveItemRelationship(string modalitemid, string relationshipid) {
            var a = this.ComponentModalRoot.Where(x => x.ComponentModalItemID == modalitemid).FirstOrDefault();
            if (a != null)
            {
                return a.RemoveItemRelation(relationshipid,conn);
            }
            else
                return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cn"></param>
        public void InsertNode(ComponentNode cn)
        {
            this.ComponentModalRoot.Add(cn);
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="componentID"></param>
        /// <param name="childComponent"></param>
        /// <param name="lf"></param>
        /// <returns></returns>
        public bool AddNode(string componentID, string childComponent, List<LinkComponentField> lf)
        {
            ComponentNode cn = new ComponentNode(this.ClientID);
            cn.ComponentModalID = this.ComponentModalID;
            cn.ComponentID = componentID;
            cn.ChildComponentID = childComponent;
            foreach (LinkComponentField f in lf)
            {
                cn.AddRelation(f.ParentField, f.RelatedField, f.ModalItemRelationID);
            }
            if (this.ComponentModalID != "")
            {
                return cn.SaveModalItem(conn);
            }
            else
            {
                return false;
            }
        }         
    }
}
