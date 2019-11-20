using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tz.Page.WebControls.Grid;

namespace Tz.Page.WebControls
{
   public class Grid : Controls.Webcontrol
    {
        public enum GridColumType {
            none,
            button,
            buttonlist,
            image,
            file,
            checkbox,
            radiobutton,
            textbox,
            progressbar,
            chart
        }
        /// <summary>
        /// 
        /// </summary>
        public List<Columns> Columns { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DataSource { get; set; } // name of datacube.
        /// <summary>
        /// 
        /// </summary>
        public bool EnablePaging { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }
        
    }

    
    public class Columns
    {
        /// <summary>
        /// 
        /// </summary>
        public string DataColum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HeaderName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool EnableSort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public GridColumType ColumnType { get; set; }
       
    }
}
