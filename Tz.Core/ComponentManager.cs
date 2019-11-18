using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Tz.ClientManager;
namespace Tz.Core
{
    public class ComponentManager
    {
        private string _clientID;
        private IComponent _component;
        public IComponent Component => _component;
        public string ClientID => _clientID;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentType"></param>
        /// <param name="clientID"></param>
        public ComponentManager(ComponentType componentType,
            string clientID,
            string name,
            string category,
            string title,
            string titleformat
            ) {
            _clientID = clientID;
            _component = new Component(this.ClientID,name, componentType,title,titleformat,category);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="ComponentID"></param>
        public ComponentManager(string clientID, string ComponentID) {
            _clientID = clientID;            
            _component = new Component(this.ClientID, ComponentID);
        }
        
       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string SaveComponent() {
            if (Component.Attributes.Count == 0) {
                throw new Exception("", null);
            }
            Component c = (Component)_component;
            c.Save();
            return Component.ComponentID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ComponentAttribute NewAttribute() {
            return new ComponentAttribute(this.ClientID, "", "");
        }
        public void InsertAttribute(ComponentAttribute ca) {
            Component c = (Component)_component;
            c.Attributes.Add(ca);
        }
        public bool AddAttribute(ComponentAttribute ca) {
            Component c = (Component)_component;
            return c.AddAttribute(ca);
        }
        public bool ChangeAttribute(ComponentAttribute ca) {
            Component c = (Component)_component;
            return c.UpdateAttribute(ca);
        }
        public bool RemoveAttribute(string attrID) {
            Component c = (Component)_component;
            return c.RemoveAttribuet(attrID);
        }
        public bool Remove() {
            Component c = (Component)_component;
            return c.Remove();
        }
        public string GetData(List<ComponentKey> keys,int currentIndex,int pageSize) {
            Component c = (Component)_component;            
            ClientServer cs = new ClientServer(this.ClientID);
            Server s = cs.GetServer();
            var dm = new Tz.Net.DataManager(this.Component.TableID,s.ServerID,this.ClientID);
            System.Data.DataTable dt = new System.Data.DataTable();
            //var tb = new Tz.Net.Entity.Table(s.ServerID,this.Component.TableID,this.ClientID); 
            dt= dm.GetData(currentIndex,pageSize);
            int totalCount = dm.GetDataCount();
           var dtjson = dt.ToJSON();
            return "{data:" + dtjson + ",total:" + totalCount + "}";
        }
    }
}
