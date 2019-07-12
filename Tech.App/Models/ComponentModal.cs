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
        public List<LinkComponentField> Relations { get; }
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
        public string ParentField { get; set; }
        public string RelatedField { get; set; }
        public string ModalItemRelationID { get; set; }
    }
}