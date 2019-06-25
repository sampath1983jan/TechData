using System;

namespace Tz.Core
{
    public enum ComponentType {
        core,
        attribute,
        meta,
        link,
        transaction,
        configuration,
        system,
    }

    public class Component
    {
        public string ClientID { get; set; }
        public string ComponentID { get; set; }
        public string ComponentName { get; set; }
        public ComponentType ComponentType { get; set; }
        public string Title { get; set; }
        public string TableID { get; set; }
        public string PrimaryKeys { get; set; }
        public string TitleFormat { get; set; }
        public string NewLayout { get; set; }
        public string ReadLayout { get; set; }
        public bool IsGlobal { get; set; }

        public Component(string clientID,string componentID) {
            ClientID = clientID;
            ComponentID = componentID;
        }
    }
}
