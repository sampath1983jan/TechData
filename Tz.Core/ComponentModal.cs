using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Tz.Core
{
public  class ComponentModal
    {
        private string ClientID;
        public Node<ComponentNode> ComponentModalRoot;
        public ComponentModal(string clientID,string componentID) {
            ClientID = clientID;
            ComponentModalRoot = new Node<ComponentNode>(new ComponentNode(clientID, componentID));
        }
        private void LoadModal() {

        }
        public void AddChild(string componentID) {
            ComponentModalRoot.AddChild(new ComponentNode(ClientID, componentID));
        }
        public void AddChild(ComponentNode compNode)
        {
            ComponentModalRoot.AddChild(compNode);
        }
        public void AddChild(Node<ComponentNode> compNode)
        {
            ComponentModalRoot.AddChild(compNode);
        }
        public void ChangeNode(string componentID) {
            ComponentModal mvModal = new ComponentModal(ClientID, componentID);
            AddChild(mvModal.ComponentModalRoot);
        }
        public bool Remove(string componentID) {
            this.ComponentModalRoot.RemoveChild(this.ComponentModalRoot.Children.Where(x=>x.Value.ComponentID == componentID).FirstOrDefault());
            return true;
        }
    }
}
