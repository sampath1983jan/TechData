using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tz.Core;

namespace Tz.UIForms
{
   public class FormFieldAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public RenderCategory Category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float Left { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float Top { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DataField { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ComponentAttribute FieldAttribute { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool EnableLimit { get; set; }
    }
}
