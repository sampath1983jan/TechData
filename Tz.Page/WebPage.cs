using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Page
{
    /// <summary>
    /// 
    /// </summary>
    public class WebPage
    {
        /// <summary>
        /// 
        /// </summary>
       public MasterPage MainPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PageID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ClientID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PageName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        public WebPage(string clientID,Layout layout) {
            MainPage = new MasterPage();
            MainPage.PageLayout = layout;
        }
    }
}
