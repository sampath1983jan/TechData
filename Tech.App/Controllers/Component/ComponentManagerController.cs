using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tz.BackApp.Models;
using Tz.Core;
namespace Tz.BackApp.Controllers.Component
{
    public class ComponentManagerController : Controller
    {
        // GET: ComponentManager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialViewExample()
        {
            return PartialView();
        }

        public ActionResult Lookup()
        {
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public JsonpResult GetLookups(string clientid) {

            return new JsonpResult(Tz.Core.Lookup.GetLookUps(clientid));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="lookupid"></param>
        /// <returns></returns>
        public JsonpResult GetLookup(string clientid, string lookupid)
        {
            Tz.Core.Lookup lk = new Lookup(clientid, lookupid);
            return new JsonpResult(lk);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonpResult SaveLookUp(string clientid, string name) {
            Tz.Core.Lookup lk = new Lookup(clientid);
            lk.Name = name;
            return new JsonpResult(lk.Save());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="lookupid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonpResult UpdateLookUp(string clientid, string lookupid, string name) {
            Tz.Core.Lookup lk = new Lookup(clientid, lookupid);
            lk.Name = name;
            return new JsonpResult(lk.Update());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="lookupid"></param>
        /// <param name="Lookupitems"></param>
        /// <returns></returns>
        public JsonpResult AddLookupItems(string clientid, string lookupid, string Lookupitems) {
            var litems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Tz.BackApp.Models.LookupItem>>(Lookupitems);
            Tz.Core.Lookup lk = new Lookup(clientid, lookupid);
            int a = 0;
            if (lk.LookupItems.Count > 0) {
                a = lk.LookupItems.Max(x => x.Order);
            }
            
            foreach (Tz.BackApp.Models.LookupItem litm in litems) {
                a = a + 1;
                lk.AddLookupItem(litm.LookUpItemID,
                    litm.Label,
                    litm.ShortLabel,
                    litm.Description,
                    litm.ParentID,
                    a);
            }
            return new JsonpResult(lk.Save());
        }

        public JsonpResult UpdateLookupItem(string clientid, string lookupid, string Lookupitem)
        {
            var litm = Newtonsoft.Json.JsonConvert.DeserializeObject<Tz.BackApp.Models.LookupItem>(Lookupitem);
            Tz.Core.Lookup lk = new Lookup(clientid, lookupid);

            return new JsonpResult(lk.UpdateLookupItem(
                    litm.Label,
                    litm.ShortLabel,
                    litm.Description,
                    litm.ParentID,
                    litm.LookUpItemID));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="lookupid"></param>
        /// <returns></returns>
        public JsonpResult RemoveLookup(string clientid, string lookupid) {
            Tz.Core.Lookup lk = new Lookup(clientid, lookupid);
            return new JsonpResult(lk.Remove());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="lookupid"></param>
        /// <param name="lookupitemid"></param>
        /// <returns></returns>
        public JsonpResult RemoveLookupItem(string clientid, string lookupid, string lookupitemid)
        {
            Tz.Core.Lookup lk = new Lookup(clientid, lookupid);
            return new JsonpResult(lk.RemoveLookupItem(lookupitemid));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="lookupid"></param>
        /// <param name="lookupitemid"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public JsonpResult ChangeActive(string clientid, string lookupid, string lookupitemid, bool isActive)
        {
            Tz.Core.Lookup lk = new Lookup(clientid, lookupid);
            return new JsonpResult(lk.ChangeLookItemActive(isActive, lookupitemid));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="lookupid"></param>
        /// <param name="lookupitemid"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public JsonpResult ChangeOrder(string clientid, string lookupid, string lookupitemid, int order) {
            Tz.Core.Lookup lk = new Lookup(clientid, lookupid);
            return new JsonpResult(lk.ChangeOrder(lookupitemid, order));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public JsonpResult GetComponents(string clientid)
        {
            //Tz.Net.ClientServer c = new Net.ClientServer(clientid);
            //Tz.Core.ComponentManager cm = new Core.ComponentManager()
            return new JsonpResult(Core.Component.GetComponentList(clientid));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="componentID"></param>
        /// <returns></returns>
        public JsonpResult GetComponent(string clientid, string componentID) {
            //   Tz.Net.ClientServer c = new Net.ClientServer(clientid);
            Tz.Core.ComponentManager cm = new Core.ComponentManager(clientid, componentID);
            return new JsonpResult(cm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="component"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public JsonpResult SaveComponent(string component, string attribute) {
            // Tz.Net.ClientServer c = new Net.ClientServer(clientid);
            string clientid = Request.Params["clientkey"];
            Tz.BackApp.Models.Component mc = new Models.Component();
            List<Models.Attribute> at = new List<Models.Attribute>();
            mc = Newtonsoft.Json.JsonConvert.DeserializeObject<Tz.BackApp.Models.Component>(component);
            at = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Attribute>>(attribute);
            Core.ComponentManager mg = new ComponentManager((Core.ComponentType)mc.ComponentType,
                clientid, mc.ComponentName, mc.Category, mc.Title, mc.TitleFormat);
            //foreach (Models.Attribute att in at) {
            //    mg.InsertAttribute(new ComponentAttribute(clientid) { AttributeName = att.AttributeName,
            //        AttributeType = (Core.ComponentAttribute.ComoponentAttributeType)att.AttributeType,
            //        ClientID = clientid,
            //        DefaultValue = att.DefaultValue,
            //        FileExtension = att.FileExtension,
            //        IsAuto = att.IsAuto,
            //        IsCore = att.IsCore,
            //        IsNullable = att.IsNullable,
            //        IsPrimaryKey = att.IsPrimaryKey,
            //        IsReadOnly = att.IsReadOnly,
            //        IsRequired = att.IsRequired,
            //        IsSecured = att.IsSecured,
            //        IsUnique = att.IsUnique,
            //        Length = att.Length,
            //        RegExp = att.RegExp
            //    });
            //}
            return new JsonpResult(mg.SaveComponent());

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="componentid"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        public JsonpResult RemoveAttribute(string clientid, string componentid, string attributeID) {
            //    Tz.Net.ClientServer c = new Net.ClientServer(clientid);
            Core.ComponentManager mg = new ComponentManager(clientid, componentid);
            return new JsonpResult(mg.RemoveAttribute(attributeID));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="componentid"></param>
        /// <returns></returns>
        public JsonpResult RemoveComponent(string clientid, string componentid) {
            //   Tz.Net.ClientServer c = new Net.ClientServer(clientid);
            Core.ComponentManager mg = new ComponentManager(clientid, componentid);
            return new JsonpResult(mg.Remove());
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="componentId"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public JsonpResult AddAttribute(string clientid, string componentId, string attribute) {
            //  Tz.Net.ClientServer c = new Net.ClientServer(clientid);
            Models.Attribute att = new Models.Attribute();
            att = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Attribute>(attribute);
            Core.ComponentManager mg = new ComponentManager(clientid, componentId);
            var natt = new ComponentAttribute(clientid)
            {
                AttributeName = att.AttributeName,
                AttributeType = (Core.ComponentAttribute.ComoponentAttributeType)att.AttributeType,
                ClientID = clientid,
                DefaultValue = att.DefaultValue,
                FileExtension = att.FileExtension,
                IsAuto = att.IsAuto,
                IsCore = att.IsCore,
                IsNullable = att.IsNullable,
                IsPrimaryKey = att.IsPrimaryKey,
                IsReadOnly = att.IsReadOnly,
                IsRequired = att.IsRequired,
                IsSecured = att.IsSecured,
                IsUnique = att.IsUnique,
                Length = att.Length,
                RegExp = att.RegExp
            };
            natt.setFieldID(att.FieldID);
            if (att.FieldID != "")
            {
                return new JsonpResult(mg.ChangeAttribute(natt));
            }
            else {
                return new JsonpResult(mg.AddAttribute(natt));
            }

        }


        #region Component modal relation

        public JsonpResult GetModalList(string clientid) {
            //Tz.Core.ComponentModal cm = new ComponentModal(clientid);
            return new JsonpResult(Tz.Core.ComponentModal.GetModalList(clientid));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="modalid"></param>
        /// <returns></returns>
        public JsonpResult GetModal(string clientid, string modalid) {
            Tz.Core.ComponentModal cm = new Core.ComponentModal(clientid, modalid);
            return new JsonpResult(cm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="name"></param>
        /// <param name="category"></param>
        /// <param name="parentcomponent"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public JsonpResult SaveModal(string clientid,
            string name,
            string category,
            string parentcomponent,
             string nodes) {

            Tz.Core.ComponentModal cm = new Core.ComponentModal(clientid, name, category, parentcomponent);
            var att = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.ComponentModalNode>>(nodes);
            foreach (Models.ComponentModalNode n in att) {
                if (n.Relations != null) {
                    var a = new ComponentNode(clientid)
                    {
                        ComponentID = n.ComponentID,
                        ComponentModalID = "",
                        ComponentModalItemID = "",
                        ChildComponentID = n.ChildComponentID
                    };

                    foreach (Models.LinkComponentField lf in n.Relations)
                    {
                        if (n.Relations != null)
                        {
                            if (lf.IsRemoved == false) {
                                a.AddRelation(lf.ParentField, lf.RelatedField, "", lf.Parent, lf.Child);
                            }                            
                        }
                    }
                    cm.InsertNode(a);
                }
            }
            return new JsonpResult(cm.Save());
        }

        public JsonpResult UpdateModal(string clientid,
            string modalid,
            string name,
            string category,
            string nodes)
        {
            Tz.Core.ComponentModal cm = new Core.ComponentModal(clientid,modalid);
            cm.ModalName = name;
            cm.Category = category;
            cm.Update();
            var att = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.ComponentModalNode>>(nodes);
            foreach (Models.ComponentModalNode n in att)
            {               
              var list = new List<Tz.Core.LinkComponentField>();
                foreach (Models.LinkComponentField lf in n.Relations)
                {
                    if (lf.IsRemoved == true)
                    { cm.RemoveItemRelationship(n.ComponentModalItemID, lf.ModalItemRelationID); }
                        if (lf.ModalItemRelationID == null)
                    {
                        lf.ModalItemRelationID = "";
                    }
                    list.Add(new Core.LinkComponentField()
                    {
                        ModalItemRelationID = lf.ModalItemRelationID,
                        Child = lf.Child,
                        Parent = lf.Parent,
                        ParentField = lf.ParentField,
                        ParentFieldName = "",
                        RelatedField = lf.RelatedField,
                        RelatedFieldName = ""
                    });
                }
                if (n.ComponentModalItemID != "")
                {
                    cm.AddRelation(n.ComponentModalItemID, list);
                }
                else {                    
                    cm.AddNode(n.ComponentID, n.ChildComponentID, list);
                }                
            }
            return new JsonpResult(true);
        }

        public JsonpResult AddNode(string clientid,
            string modalid,
            string node
            )
        {
            var att = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.ComponentModalNode>(node);
            Tz.Core.ComponentModal cm = new Core.ComponentModal(clientid, modalid);
            var list = new List<Tz.Core.LinkComponentField>();
            foreach (Models.LinkComponentField lf in att.Relations) {
                if (lf.ModalItemRelationID == null) {
                    lf.ModalItemRelationID = "";
                }
                list.Add(new Core.LinkComponentField()
                {
                    ModalItemRelationID = lf.ModalItemRelationID,
                    Child = lf.Child,
                    Parent = lf.Parent,
                    ParentField = lf.ParentField,
                    ParentFieldName = "",
                    RelatedField = lf.RelatedField,
                    RelatedFieldName = ""
                });
            }
            cm.AddNode(att.ComponentID, att.ChildComponentID, list);
            return new JsonpResult(cm.Save());
        }

        public JsonpResult ChangeRelation(string clientid,
            string modalid,
            string modalitemid,
            string relation)
        {
            Tz.Core.ComponentModal cm = new Core.ComponentModal(clientid, modalid);
            var rs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.LinkComponentField>>(relation);
            var ls = new List<Tz.Core.LinkComponentField>();
            foreach (Models.LinkComponentField lf in rs)
            {
                if (lf.ModalItemRelationID == null)
                {
                    lf.ModalItemRelationID = "";
                }
                ls.Add(new Core.LinkComponentField()
                {
                    ModalItemRelationID = lf.ModalItemRelationID,
                    Child = lf.Child,
                    Parent = lf.Parent,
                    ParentField = lf.ParentField,
                    ParentFieldName = "",
                    RelatedField = lf.RelatedField,
                    RelatedFieldName = ""
                });
                
            }
            return new JsonpResult(cm.AddRelation(modalitemid, ls));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="modalid"></param>
        /// <returns></returns>
        public JsonpResult RemoveModal(string clientid, string modalid) {
            Tz.Core.ComponentModal cm = new Core.ComponentModal(clientid, modalid);
            return new JsonpResult(cm.Remove());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="modalid"></param>
        /// <param name="modalitemid"></param>
        /// <returns></returns>
        public JsonpResult RemoveModalItem(string clientid,
            string modalid,
            string modalitemid)
        {
            Tz.Core.ComponentModal cm = new Core.ComponentModal(clientid, modalid);
            return new JsonpResult(cm.RemoveItem(modalitemid));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="modalid"></param>
        /// <param name="modalitemid"></param>
        /// <param name="relation"></param>
        /// <returns></returns>
        public JsonpResult RemoveItemRelation(string clientid,
            string modalid, 
            string modalitemid,
            string  relation) {
            Tz.Core.ComponentModal cm = new Core.ComponentModal(clientid, modalid);
            return new JsonpResult(cm.RemoveItemRelationship(modalitemid,relation));
        }

        public JsonpResult RemoveItemAllRelation(string clientid,
            string modalid,
            string modalitemid)
        {
            Tz.Core.ComponentModal cm = new Core.ComponentModal(clientid, modalid);
            return new JsonpResult(cm.RemoveAllRelationByModalItem(modalitemid));
        }


        #endregion
    }


}