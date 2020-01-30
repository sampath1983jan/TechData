using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tz.BackApp.Models
{
    public class FormField
    {
        public string FormID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DateFormat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TimeFormat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DateTimeFormat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pickerInterval { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AllowedDays { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AllowedFromHours { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AllowedToHours { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SeletionType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ValueField;
        /// <summary>
        /// 
        /// </summary>
        public string DisplayField;
        /// <summary>
        /// 
        /// </summary>
        public string DefineSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LookUpSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ComponentSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool AllowOrderbyAlphabet { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// 
        /// </summary>    
        ///         
        public int Min { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Max { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DisplayFormat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FormFieldID { get; set; }
         
        /// <summary>
        /// 
        /// </summary>
        public int Category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float Left { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float Top { get; set; }
        /// <summary>
        /// /
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

        /// <summary>
        /// 
        /// </summary>
        public string FileExtension { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double MaxFileSize { get; set; }
    }
}

public class FieldPosition {
    public int Left { get; set; }
    public int Top { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string FieldID { get; set; }
}