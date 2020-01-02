﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.UIForms.FormFields
{
   public class Upload : FormField
    {
        /// <summary>
        /// 
        /// </summary>
        public string FileExtension { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double MaxFileSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        public Upload(string clientid) : base(clientid, "")
        {
            FieldRenderType = RenderType.UPLOAD;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="formid"></param>
        public Upload(string clientid, string formid) : base(clientid, formid)
        {
            FieldRenderType = RenderType.UPLOAD;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        new public bool Save()
        {
            Data.UIForm.UIFields uIFields = new Data.UIForm.UIFields(UIForms.Common.GetConnection(this.ClientID));
            if (this.FormFieldID != "")
            {
                uIFields.Save(this.FormID,
                              this.ClientID, (int)this.FieldRenderType,
                              (int)this.Category,
                              this.Left,
                              this.Top, this.FieldAttribute.FieldID, Newtonsoft.Json.JsonConvert.SerializeObject(this.FieldAttribute)
                              , this.Width, this.Height);
            }
            else
            {
                uIFields.Update(this.FormID,
                    this.FormFieldID,
                    this.ClientID,
                    (int)this.FieldRenderType,
                    (int)this.Category,
                    this.Left,
                    this.Top,
                    Newtonsoft.Json.JsonConvert.SerializeObject(this.FieldAttribute)
                    , this.Width, this.Height);
            }
            return true;
        }

    }
}