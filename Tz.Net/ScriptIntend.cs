using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Tz.Net
{
    public class ScriptIntend
    {
        public string ScriptID { get; set; }
        public string Intend { get; set; }
        private Data.ScriptIntend scriptIntend;

        public ScriptIntend(string intend)
        {
            scriptIntend = new Data. ScriptIntend();
           DataTable dt= scriptIntend.GetScript(intend);
            if (dt.Rows.Count > 0) {
                ScriptID = dt.Rows[0]["ScriptID"].ToString();
            }else
            {
                ScriptID = "";
            }
        }
        public ScriptIntend(string scriptID, string intend) {
            this.ScriptID = scriptID;
            scriptIntend = new Data.ScriptIntend();
            if (intend == "")
            {
               
                DataTable dt = scriptIntend.GetIntend(ScriptID);
                if (dt.Rows.Count > 0)
                {
                    this.Intend = dt.Rows[0]["intend"].ToString();
                }
            }
            else {
                this.Intend = intend;
            }           
            
            
        }
        public bool Save() {
            if (scriptIntend.GetIntend(ScriptID).Rows.Count > 0)
            {
                return scriptIntend.Update(ScriptID, Intend);
            }
            else
                return scriptIntend.Save(ScriptID, Intend);
        }
        public bool Update() {
            return scriptIntend.Update(ScriptID, Intend);
        }
        public bool Remove() {
            return scriptIntend.Remove(ScriptID, Intend);
        }

    }
}
