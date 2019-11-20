using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tz.Page.LayoutElement;
namespace Tz.Page
{
   public class Layout
    {
        /// <summary>
        /// 
        /// </summary>
        public Header PageHeader { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Body PageBody { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Footer PageFooter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public LeftPanel PageLeft { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RightPanel PageRight { get; set; }

    }
}
