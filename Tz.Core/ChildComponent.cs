using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Tz.Core
{
  public  class ComponentNode
    {
        /// <summary>
        /// 
        /// </summary>
        private Component Component { get; }
        /// <summary>
        /// 
        /// </summary>
        public string ComponentModalID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ComponentModalItemID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<LinkComponentField> Relations { get; }        
        /// <summary>
        /// 
        /// </summary>
        public string ComponentID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ChildComponentID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ClientID { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ChildName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="componentID"></param>
        /// <param name="childComponentid"></param>
        /// <param name="componentModalItemID"></param>
        public ComponentNode(string clientID,             
            string componentID,
            string childComponentid,
            string componentmodalitemid) {
            ComponentModalID = "";
            ComponentID = componentID;
            ComponentModalItemID = componentmodalitemid;
            ChildComponentID = childComponentid;
           // Component = new Component(clientID, componentID);
            ClientID = clientID;
            this.Relations = new List<LinkComponentField>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        public ComponentNode(string clientID)
        {
            ComponentModalID = "";
            ComponentModalItemID = "";
            ComponentID = "";   
            ClientID = clientID;
            ChildComponentID = "";
            this.Relations = new List<LinkComponentField>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pfield"></param>
        /// <param name="rfield"></param>
        /// <param name="relationid"></param>
        public void AddRelation(string pfield, string rfield,string relationid,
            string parent,string child) {
            this.Relations.Add(new LinkComponentField() { ParentField = pfield,
                RelatedField = rfield , ModalItemRelationID  = relationid,
                Parent=parent,
                Child=child
            });
        }
        public void AddRelation(LinkComponentField lk )
        {
            this.Relations.Add(lk);
        }
        internal bool SaveRelation(string conn, List<LinkComponentField> lk) {
            var dataComponentModal = new Data.Component.ComponentModal(conn);
            foreach (LinkComponentField lf in lk)
            {
                if (this.Relations.Where(x => x.ModalItemRelationID == lf.ModalItemRelationID).FirstOrDefault() == null)
                {
                    dataComponentModal.SaveItemRelation(this.ClientID,
                    this.ComponentModalItemID,
                    lf.ParentField,
                    lf.RelatedField,
                    lf.Parent,
                   lf.Child);
                }
                else {
                    dataComponentModal.UpdateItemRelation(this.ClientID,
                        lf.ModalItemRelationID,
                    this.ComponentModalItemID,
                    lf.ParentField,
                    lf.RelatedField);
                }                
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool SaveModalItem(string conn) {            
            var dataComponentModal = new Data.Component.ComponentModal(conn);
            this.ComponentModalItemID = dataComponentModal.SaveModalItem(this.ClientID,
                this.ComponentModalID,
                this.ComponentID,
                this.ChildComponentID);
            if (this.ComponentModalItemID !="")
            {
                foreach (LinkComponentField lf in this.Relations) {
                    dataComponentModal.SaveItemRelation(this.ClientID,
                        this.ComponentModalItemID,
                        lf.ParentField,
                        lf.RelatedField,
                        lf.Parent,
                       lf.Child);
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
        /// <param name="mrelatedID"></param>
        /// <param name="parentfield"></param>
        /// <param name="relatedfield"></param>
        /// <returns></returns>
        public bool UpdateModalItemRelation(string mrelatedID,string parentfield,string relatedfield) {
            var c = new Net.ClientServer(ClientID);
            var item = this.Relations.Where(x => x.ModalItemRelationID == mrelatedID).FirstOrDefault();
            var dataComponentModal = new Data.Component.ComponentModal(c.GetServer().Connection());
            if (item != null)
            {
                dataComponentModal.UpdateItemRelation(this.ClientID, mrelatedID, this.ComponentModalItemID, parentfield, relatedfield);
                return true;
            }
            else
                return false;
        }

        internal bool RemoveItemRelation(string rid,string conn) {
            var dataComponentModal = new Data.Component.ComponentModal(conn);
            return dataComponentModal.RemoveItemRelation(this.ClientID, rid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modalid"></param>
        /// <returns></returns>
        public bool RemoveItem(string modalid) {
            var c = new Net.ClientServer(ClientID);
            var dataComponentModal = new Data.Component.ComponentModal(c.GetServer().Connection());
            if (dataComponentModal.RemoveModalItem(this.ClientID, modalid, this.ComponentModalItemID))
            {
                dataComponentModal.RemoveAllItemRelation(this.ClientID, this.ComponentModalItemID);
                return true;
            }
            else {
                return false;
            }
        }
        public bool RemoveAllRelations() {
            var c = new Net.ClientServer(ClientID);
            var dataComponentModal = new Data.Component.ComponentModal(c.GetServer().Connection());
            dataComponentModal.RemoveAllItemRelation(this.ClientID, this.ComponentModalItemID);
            return true;
        }

    }
    public class LinkComponentField {
        public string ParentFieldName { get; set; }
        public string RelatedFieldName { get; set; }
        public string ParentField { get; set; }
        public string RelatedField { get; set; }
        public string Parent { get; set; }
        public string Child { get; set; }
        public string ModalItemRelationID { get; set; }
    }
}
