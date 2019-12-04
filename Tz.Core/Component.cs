using System;
using Tz.Data;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Tz.Net;
using Tz.ClientManager;
namespace Tz.Core
{
    public enum ComponentType {
        /// <summary>
        /// Master component of the system
        /// </summary>
        core,
        /// <summary>
        /// additional information of the core
        /// </summary>
        attribute,
        /// <summary>
        /// meta information of the core or link
        /// </summary>
        meta            ,
        /// <summary>
        /// information of two or more core component
        /// </summary>
        link,
        /// <summary>
        /// transaction information of the system
        /// </summary>
        transaction,
        /// <summary>
        /// configuration of the system
        /// </summary>
        configuration,
        /// <summary>
        /// system information or global settings.
        /// </summary>
        system,
        /// <summary>
        /// it is not table
        /// </summary>
        none
    }
    /// <summary>
    /// 
    /// </summary>
    public abstract class IComponent {
        /// <summary>
        /// 
        /// </summary>
        public string ClientID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ComponentID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ComponentName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ComponentType ComponentType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TableID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PrimaryKeys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TitleFormat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NewLayout { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ReadLayout { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ComponentAttribute> Attributes;
        /// <summary>
        /// 
        /// </summary>
        public bool IsGlobal { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>    
    public class Component:IComponent
    {
        ClientServer ck;
        private Data.Component.Component dataComponent;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="componentID"></param>
        public Component(string clientID,string componentID) {
            ClientID = clientID;
            ComponentID = componentID;
            ck = new ClientServer(clientID);
            dataComponent = new Data.Component.Component(ck.GetServer().Connection());
            Attributes = new List<ComponentAttribute>();
            TableID = "";
            PrimaryKeys = "";
            Load();
        }
        public Component(string clientID) {
            ClientID = clientID;
            ComponentID = "";
            TableID = "";
            PrimaryKeys = "";
            ck = new ClientServer(clientID);
            dataComponent = new Data.Component.Component(ck.GetServer().Connection());
            Attributes = new List<ComponentAttribute>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="componentType"></param>
        public Component(string clientID,
            string name,
            ComponentType componentType,
            string title,
            string titleformat
            ,string category) {
            ClientID = clientID;
            ComponentID = "";
            TableID = "";
            PrimaryKeys = "";
            this.Title = title;
            this.TitleFormat = titleformat;
            this.ComponentName = name;
            this.Category = category;
            this.ComponentType = componentType;
            ck = new ClientServer(clientID);
            dataComponent = new Data.Component.Component(ck.GetServer().Connection());
            Attributes = new List<ComponentAttribute>();
        }
        public static List<Component> GetComponentList(string clientID) {
            var ck = new ClientServer(clientID);
            var dataComponent = new Data.Component.Component(ck.GetServer().Connection());
            var dtFields = new DataTable();
            var dt= dataComponent.GetCompnents(clientID);
            var dttable = dt.DefaultView.ToTable(true,
                "ComponentID",
                "ComponentName",
                "ComponentType",
                "Title",
                "TableID",
                "PrimaryKeys",
                "TitleFormat",
                "Category",
                "IsGlobal"
                );
            var clist = new List<Component>();
            dtFields = dt.DefaultView.ToTable(true, "FieldID",
                "AttributeName",
                "ComponentID",               
                "IsRequired",
                "IsUnique",
                "IsCore",
                "IsReadOnly",
                "IsSecured",
                "IsAuto",
                "DefaultValue",
                "FileExtension",
                "RegExp",
                "AttributeType"           
               
                );
            foreach (DataRow dr in dttable.Rows)
            {
                var c = new Component(clientID);
                c.ComponentID = dr.IsNull("ComponentID") ? "" : dr["ComponentID"].ToString();
                c.ComponentName = dr.IsNull("ComponentName") ? "" : dr["ComponentName"].ToString();
                c.ComponentType = dr.IsNull("ComponentType") ? Tz.Core.ComponentType.none : (Tz.Core.ComponentType)dr["ComponentType"];
                c.Title = dr.IsNull("Title") ? "" : dr["Title"].ToString();
                c.TableID = dr.IsNull("TableID") ? "" : dr["TableID"].ToString();
                c.PrimaryKeys = dr.IsNull("PrimaryKeys") ? "" : dr["PrimaryKeys"].ToString();
                c.TitleFormat = dr.IsNull("TitleFormat") ? "" : dr["TitleFormat"].ToString();
                c.Category = dr.IsNull("Category") ? "" : dr["Category"].ToString();
                c.IsGlobal = dr.IsNull("IsGlobal") ? false : Convert.ToBoolean(dr["IsGlobal"]);
               
                
                foreach (DataRow drow in dtFields.Rows)
                {
                    string _fieldid = "";
                    _fieldid = drow.IsNull("FieldID") ? "" : drow["FieldID"].ToString();
                    ComponentAttribute ca = new ComponentAttribute(clientID, c.TableID, _fieldid);
                    ca.AttributeName = drow.IsNull("AttributeName") ? "" : drow["AttributeName"].ToString();
                   // ca.FieldName = drow.IsNull("FieldName") ? "" : drow["FieldName"].ToString();
                    ca.IsRequired = drow.IsNull("IsRequired") ? false : Convert.ToBoolean(drow["IsRequired"]);
                    ca.IsUnique = drow.IsNull("IsUnique") ? false : Convert.ToBoolean(drow["IsUnique"]);
                    ca.IsCore = drow.IsNull("IsCore") ? false : Convert.ToBoolean(drow["IsCore"]);
                    ca.IsReadOnly = drow.IsNull("IsReadOnly") ? false : Convert.ToBoolean(drow["IsReadOnly"]);
                    ca.IsSecured = drow.IsNull("IsSecured") ? false : Convert.ToBoolean(drow["IsSecured"]);
                    ca.IsAuto = drow.IsNull("IsAuto") ? false : Convert.ToBoolean(drow["IsAuto"]);
                    ca.DefaultValue = drow.IsNull("DefaultValue") ? "" : drow["DefaultValue"].ToString();
                    ca.FileExtension = drow.IsNull("FileExtension") ? "" : drow["FileExtension"].ToString();
                    ca.RegExp = drow.IsNull("RegExp") ? "" : drow["RegExp"].ToString();
                    ca.AttributeType = drow.IsNull("AttributeType") ? ComponentAttribute.ComoponentAttributeType._string : (ComponentAttribute.ComoponentAttributeType)drow["AttributeType"];
                    //ca.FieldType = drow.IsNull("FieldType") ? DbType.String : (DbType)drow["FieldType"];
                    //ca.Length = drow.IsNull("length") ? -1 : (int)drow["length"];
                  //  ca.IsNullable = drow.IsNull("IsNullable") ? false : (bool)drow["IsNullable"];
                  //  ca.IsPrimaryKey = drow.IsNull("ISPrimaryKey") ? false : (bool)drow["ISPrimaryKey"];
                    c.Attributes.Add(ca);
                }
                clist.Add(c);
            }
            return clist;
        }
        /// <summary>
        /// 
        /// </summary>
        private void Load() {
            DataTable dt =dataComponent.GetComponent(this.ClientID,this.ComponentID);
            foreach (DataRow dr in dt.Rows) {
                ComponentName = dr.IsNull("ComponentName") ? "" : dr["ComponentName"].ToString();
                ComponentType = dr.IsNull("ComponentType") ?  Tz.Core.ComponentType.none : (Tz.Core.ComponentType) dr["ComponentType"];
                Title = dr.IsNull("Title") ? "" : dr["Title"].ToString();
               TableID= dr.IsNull("TableID") ? "" : dr["TableID"].ToString();
                PrimaryKeys = dr.IsNull("PrimaryKeys") ? "" : dr["PrimaryKeys"].ToString();
                TitleFormat = dr.IsNull("TitleFormat") ? "" : dr["TitleFormat"].ToString();
                Category = dr.IsNull("Category") ? "" : dr["Category"].ToString();
                IsGlobal = dr.IsNull("IsGlobal") ? false : Convert.ToBoolean(dr["IsGlobal"]);
            }
            Data.Component.ComponentAttribute ca = new Data.Component.ComponentAttribute(ck.GetServer().Connection());
            dt = new DataTable();
            dt= ca.GetAttributes(this.ComponentID);
            foreach (DataRow dr in  dt.Rows) {
                string _fieldid = "";
                _fieldid = dr.IsNull("FieldID") ? "" : dr["FieldID"].ToString();
                ComponentAttribute c = new ComponentAttribute(this.ClientID,this.TableID,_fieldid);
                c.AttributeName= dr.IsNull("AttributeName") ? "" : dr["AttributeName"].ToString();
                c.FieldName = dr.IsNull("FieldName") ? "" : dr["FieldName"].ToString();
                c.IsRequired = dr.IsNull("IsRequired") ? false : Convert.ToBoolean(dr["IsRequired"]);
                c.IsUnique = dr.IsNull("IsUnique") ? false :Convert.ToBoolean(dr["IsUnique"]);
                c.IsCore = dr.IsNull("IsCore") ? false : Convert.ToBoolean(dr["IsCore"]);
                c.IsReadOnly = dr.IsNull("IsReadOnly") ? false : Convert.ToBoolean(dr["IsReadOnly"]);
                c.IsSecured = dr.IsNull("IsSecured") ? false : Convert.ToBoolean(dr["IsSecured"]);
                c.IsAuto = dr.IsNull("IsAuto") ? false : Convert.ToBoolean(dr["IsAuto"]);
                c.DefaultValue = dr.IsNull("DefaultValue") ? "" : dr["DefaultValue"].ToString();
                c.FileExtension = dr.IsNull("FileExtension") ? "" : dr["FileExtension"].ToString();
                c.RegExp = dr.IsNull("RegExp") ? "" : dr["RegExp"].ToString();
                c.AttributeType = dr.IsNull("AttributeType") ? ComponentAttribute.ComoponentAttributeType._string : (ComponentAttribute.ComoponentAttributeType)dr["AttributeType"];
              // c.FieldType= dr.IsNull("FieldType") ? DbType.String :(DbType) dr["FieldType"];
             c.Length = dr.IsNull("length") ? -1 : Convert.ToInt32(dr["length"]);
                c.IsNullable = dr.IsNull("IsNullable") ? false : Convert.ToBoolean(dr["IsNullable"]);
                c.IsPrimaryKey = dr.IsNull("ISPrimaryKey") ? false : Convert.ToBoolean(dr["ISPrimaryKey"]);
                this.Attributes.Add(c);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string getCategory() {

            if (this.ComponentType == ComponentType.core)
            {
                return "Core";
            }
            else if (this.ComponentType == ComponentType.meta)
            {
                return "Meta";
            }
            else if (this.ComponentType == ComponentType.system)
            {
                return "System";
            }
            else if (this.ComponentType == ComponentType.transaction) {
         
                    return "Transaction";
            }
            else if (this.ComponentType == ComponentType.configuration)
            {
                return "Configuration";
            }
            else if (this.ComponentType == ComponentType.link)
            {
                return "Link";
            }
            else if (this.ComponentType == ComponentType.attribute)
            {
                return "Attribute";
            }
            else if (this.ComponentType == ComponentType.none)
            {
                return "None";
            }
            else
            {
                return "";
            }
        }

        internal bool SaveAsDraft() {
            //Tz.ClientManager.ClientServer c = new Tz.ClientManager.ClientServer(this.ClientID);
            //Tz.ClientManager.Server s = c.GetServer();
            // var dm = new DataManager(s, c.ClientID);
            // dm.NewTable(this.ComponentName, getCategory());
            this.ComponentID = dataComponent.SaveComponent(ClientID,
                    ComponentName,
                    (int)ComponentType,
                    TableID,
                    Title,
                    PrimaryKeys,
                    TitleFormat,
                    "", "", Category);
            return true;
        }
        internal bool Publish() {
            Tz.ClientManager.ClientServer c = new Tz.ClientManager.ClientServer(this.ClientID);
            Tz.ClientManager.Server s = c.GetServer();     
            var pk = "";
            var dm = new DataManager(s, c.ClientID);
            dm.NewTable(this.ComponentName, getCategory());
            foreach (ComponentAttribute ca in this.Attributes)
            {
                if (ca.IsPrimaryKey)
                {
                    pk = pk + ca.AttributeName.Replace(" ", "_");
                    dm.AddPrimarykey(ca.AttributeName.Replace(" ", "_"), GetFieldType(ca), ca.Length);
                }
                else
                {
                    dm.AddField(ca.AttributeName.Replace(" ", "_"), GetFieldType(ca), ca.Length, ca.IsNullable);
                }
            }
            if (pk.StartsWith(","))
            {
                pk = pk.Substring(1);
                PrimaryKeys = pk;
            }
            else
            {
                PrimaryKeys = pk;
            }
            if (PrimaryKeys == null)
            {
                PrimaryKeys = "";
            }

            dm.AcceptChanges();
            if (dm.GetTable().TableID == "")
            {
                return false;
            }
            TableID = dm.GetTable().TableID;
            foreach (ComponentAttribute ca in this.Attributes)
            {

                Net.Entity.IField f = dm.GetTable().Fields.Where(x => x.FieldName == ca.AttributeName.Replace(" ", "_")).FirstOrDefault();
                if (f != null)
                {
                    ca.setFieldID(f.FieldID);
                }
            }
            dataComponent.ChangeState(this.ClientID, this.ComponentID, 1);// publish
            foreach (ComponentAttribute ca in this.Attributes)
            {
                this.UpdateAttribute(ca, s.ServerID);
            }
                return true;
        }
        /// <summary>
        ///  save component
        /// </summary>
        /// <returns></returns>
        internal bool Save() {
            Tz.ClientManager.ClientServer c = new Tz.ClientManager.ClientServer(this.ClientID);
            Tz.ClientManager.Server s = c.GetServer();
            if (ComponentID == "")
            {             
                var dm = new DataManager(s, c.ClientID);
                dm.NewTable(this.ComponentName, getCategory());
                var pk = "";
                foreach (ComponentAttribute ca in this.Attributes) {
                    if (ca.IsPrimaryKey)
                    {
                        pk = pk + ca.AttributeName.Replace(" ", "_");
                        dm.AddPrimarykey(ca.AttributeName.Replace(" ","_"), GetFieldType(ca), ca.Length);
                    }
                    else {
                        dm.AddField(ca.AttributeName.Replace(" ", "_"), GetFieldType(ca), ca.Length, ca.IsNullable);
                    }
                }
                if (pk.StartsWith(",")) {
                    pk = pk.Substring(1);
                    PrimaryKeys = pk;
                }
                else
                {
                    PrimaryKeys = pk;
                }
                if (PrimaryKeys == null)
                {
                    PrimaryKeys = "";
                }                
                try
                {
                    dm.AcceptChanges();
                    if (dm.GetTable().TableID == "") {
                        return false;
                    }
                    TableID = dm.GetTable().TableID;
                    foreach (ComponentAttribute ca in this.Attributes)
                    {
                        Net.Entity.IField f = dm.GetTable().Fields.Where(x => x.FieldName == ca.AttributeName.Replace(" ", "_")).FirstOrDefault();
                        if (f != null) {
                            ca.setFieldID(f.FieldID);
                        }
                    }
                    this.ComponentID = dataComponent.SaveComponent(ClientID,
                   ComponentName,
                   (int)ComponentType,
                   TableID,
                   Title,
                   PrimaryKeys,
                   TitleFormat,
                   "", "",Category);
                    if (this.ComponentID != "")
                    {
                        var dataComponentAttr = new Data.Component.ComponentAttribute(ck.GetServer().Connection());
                        foreach (ComponentAttribute ca in this.Attributes) {                          
                            dataComponentAttr.Save(this.ClientID,
                                this.ComponentID,
                                ca.FieldID,
                                ca.IsRequired,
                                ca.IsCore,
                                ca.IsUnique,
                                ca.IsReadOnly,
                                ca.IsSecured,
                                ca.IsAuto,
                                ca.LookUpID,
                                ca.DefaultValue,
                                ca.FileExtension,
                                ca.RegExp,
                                ca.AttributeName,
                               (int) ca.AttributeType);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (System.Exception ex) {
                    throw ex;
                }              
               
            }
            else {
                try { 
                var dm = new DataManager(this.TableID, s.ServerID, c.ClientID);
                dm.Rename(this.ComponentName, this.getCategory());
                if (dataComponent.UpdateComponent(ClientID,
                   ComponentID,
                   ComponentName,
                   (int)ComponentType,                  
                   Title,
                   PrimaryKeys,
                   TitleFormat,
                   "", "", Category))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
                catch (System.Exception ex)
            {
                throw ex;
            }


        }
        }

        private DbType GetFieldType(ComponentAttribute ca) {
            if (ca.AttributeType == ComponentAttribute.ComoponentAttributeType._number)
            {
                return DbType.Int32;
            }
            else if (ca.AttributeType == ComponentAttribute.ComoponentAttributeType._decimal)
            {
                return DbType.Double;
            }
            else if (ca.AttributeType == ComponentAttribute.ComoponentAttributeType._string)
            {
                return DbType.String;
            }
            else if (ca.AttributeType == ComponentAttribute.ComoponentAttributeType._longstring)
            {
                return DbType.String;
            }
            else if (ca.AttributeType == ComponentAttribute.ComoponentAttributeType._lookup)
            {
                return DbType.String;
            }
            else if (ca.AttributeType == ComponentAttribute.ComoponentAttributeType._file)
            {
                return DbType.String;
            }
            else if (ca.AttributeType == ComponentAttribute.ComoponentAttributeType._picture)
            {
                return DbType.String;
            }
            else if (ca.AttributeType == ComponentAttribute.ComoponentAttributeType._currency)
            {
                return DbType.Double;
            }
            else if (ca.AttributeType == ComponentAttribute.ComoponentAttributeType._componentlookup)
            {
                return DbType.Int32;
            }
            else if (ca.AttributeType == ComponentAttribute.ComoponentAttributeType._date)
            {
                return DbType.Date;
            }
            else if (ca.AttributeType == ComponentAttribute.ComoponentAttributeType._datetime)
            {
                return DbType.DateTime;
            }
            else if (ca.AttributeType == ComponentAttribute.ComoponentAttributeType._bit)
            {
                return DbType.Boolean;
            }
            else if (ca.AttributeType == ComponentAttribute.ComoponentAttributeType._time)
            {
                return DbType.Time;
            }
            else {
                return DbType.String;
            }
        }
        /// <summary>
        /// add new attribute in the component
        /// </summary>
        /// <param name="ca"></param>
        /// <returns></returns>
        internal bool AddAttribute(ComponentAttribute ca) {
            try
            {
                Tz.ClientManager.ClientServer c = new Tz.ClientManager.ClientServer(this.ClientID);
                Tz.ClientManager.Server s = c.GetServer();
                DataManager dm = new DataManager(this.TableID, s.ServerID, this.ClientID);
                if (ca.IsPrimaryKey)
                {
                    dm.AddPrimarykey(ca.AttributeName.Replace(" ", "_"), GetFieldType(ca), ca.Length);
                }
                else
                    dm.AddField(ca.AttributeName.Replace(" ", "_"), GetFieldType(ca), ca.Length, ca.IsNullable);

                dm.AcceptChanges();
                TableID = dm.GetTable().TableID;
                Net.Entity.IField f = dm.GetTable().Fields.Where(x => x.FieldName == ca.AttributeName.Replace(" ", "_")).FirstOrDefault();
                if (f != null)
                {
                    ca.setFieldID(f.FieldID);

                    var dataComponentAttr = new Data.Component.ComponentAttribute(ck.GetServer().Connection());
                    dataComponentAttr.Save(this.ClientID,
                        this.ComponentID,
                        ca.FieldID,
                        ca.IsRequired,
                        ca.IsCore,
                        ca.IsUnique,
                        ca.IsReadOnly,
                        ca.IsSecured,
                        ca.IsAuto,
                        ca.LookUpID,
                        ca.DefaultValue,
                        ca.FileExtension,
                        ca.RegExp,
                        ca.AttributeName,
                        (int)ca.AttributeType);
                    return true;
                }
                else {
                    return false;
                }

               
            }
            catch (Exception ex) {
                throw ex;
            }          

        }
        /// <summary>
        /// attribute change
        /// </summary>
        /// <param name="ca"></param>
        /// <returns></returns>
        internal bool UpdateAttribute(ComponentAttribute ca,string serverid ="") {
            try
            {
                if (serverid == "") {
                    Tz.ClientManager.ClientServer c = new Tz.ClientManager.ClientServer(this.ClientID);
                    Tz.ClientManager.Server s = c.GetServer();
                    serverid = s.ServerID;
                }
                
                DataManager dm = new DataManager(this.TableID, serverid, this.ClientID);                
                dm.ChangeField(ca.FieldID, ca.AttributeName.Replace(" ", "_"), GetFieldType(ca), ca.Length,ca.IsNullable,ca.IsPrimaryKey,ca.AttributeName.Replace(" ", "_"));
                dm.AcceptChanges();
                var dataComponentAttr = new Data.Component.ComponentAttribute(ck.GetServer().Connection());
                dataComponentAttr.Update(this.ClientID,
                    this.ComponentID,
                    ca.FieldID,
                    ca.IsRequired,
                    ca.IsCore,
                    ca.IsUnique,
                    ca.IsReadOnly,
                    ca.IsSecured,
                    ca.IsAuto,
                    ca.LookUpID,
                    ca.DefaultValue,
                    ca.FileExtension,
                    ca.RegExp,
                    ca.AttributeName);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// remove component 
        /// </summary>
        /// <returns></returns>
        internal bool Remove() {
            try {
                dataComponent.Remove(this.ClientID, this.ComponentID);
                var datacom = new Data.Component.ComponentAttribute(ck.GetServer().Connection());
                datacom.RemoveAll(this.ComponentID);
                    
                Tz.ClientManager.ClientServer c = new Tz.ClientManager.ClientServer(this.ClientID);
                Tz.ClientManager.Server s = c.GetServer();
                DataManager dm = new DataManager(this.TableID, s.ServerID, this.ClientID);
                dm.Remove();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// remove attribute
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        internal bool RemoveAttribuet(string attributeID)
        {
            try
            {
                var datacom = new Data.Component.ComponentAttribute(ck.GetServer().Connection());
                if (datacom.Remove(attributeID, this.ComponentID))
                {
                    datacom.RemoveAll(this.ComponentID);
                    Tz.ClientManager.ClientServer c = new Tz.ClientManager.ClientServer(this.ClientID);
                    Tz.ClientManager.Server s = c.GetServer();
                    DataManager dm = new DataManager(this.TableID, s.ServerID, this.ClientID);
                    dm.RemoveField(attributeID);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
    }

    public class ComponentKey {
        public string ComponentID;
        public string Key;
        public string Value;
        public ComponentKey(string componentID,string key, string val) {
            ComponentID = componentID;
            Key = key;
            Value = val;
        }
    }
}
