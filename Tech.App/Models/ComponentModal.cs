using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tz.BackApp.Models
{
    public class ComponentModal
    {

    }

    public class ComponentModalNode {        
        /// <summary>
        /// 
        /// </summary>
        public string ComponentModalItemID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<LinkComponentField> Relations { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ComponentID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ChildComponentID { get; set; }
        
    }

    public class LinkComponentField
    {
        public string ParentFieldName { get; set; }
        public string ChildFieldName { get; set; }
        public string ParentField { get; set; }
        public string RelatedField { get; set; }
        public string ModalItemRelationID { get; set; }
        public string Parent { get; set; }
        public string Child { get; set; }
        public bool IsRemoved { get; set; }

    }
}