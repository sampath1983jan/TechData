using System;
using System.Collections.Generic;
using System.Text;

namespace Tz.Core
{
  public  class ComponentNode
    {
        private Component Component { get; }
        public List<LinkComponentField> Relations { get; }
        private int Left;
        private int Right;
        private int Depth;
        public string ComponentID { get; set; };
        public string ClientID { get; set; };
        public string ComponentModalID { get; set; }
        public ComponentNode(string clientID, string componentID) {
            ComponentID = componentID;
            Component = new Component(clientID, componentID);
            ClientID = clientID;
        }
        public ComponentNode(string clientID, Component c)
        {
            ComponentID = c.ComponentID;
            Component = c;
            ClientID= c.ClientID;
        }
    }
    public class LinkComponentField {
        public string ParentField { get; set; }
        public string RelatedField { get; set; }
    }
}
