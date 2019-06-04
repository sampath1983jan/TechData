using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.QScript.Syntax;
using Tech.QScript;
using System.Data;
namespace Tz.Net
{
   public class DataScript
    {
       public string ScriptID { get; set; }
        public string Script { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        private QScriptStatement sq;
        private Data.DataScript dScript;
        public DataScript(string scriptID) {
            ScriptID= scriptID;
            Script = "";
            Name = "";
            Category = "";
            dScript = new Data.DataScript();
            Load();
        }
        public DataScript(string scriptname, string category, string script) {
            Script = script;
            Name =scriptname;
            Category = category;
            ScriptID = "";
            dScript = new Data.DataScript();
        }
        public DataScript() {
            Script = "";
            Name = "";
            Category = "";
            ScriptID = "";
            dScript = new Data.DataScript();
        }

        public static List<DataScript> GetDataScripts() {
            var dScript = new Data.DataScript();
            DataTable dt = new DataTable();
           dt = dScript.GetScripts();

            List<DataScript> c = dt.toList<DataScript>(new DataFieldMappings()
                  .Add(Tz.Data.TzAccount.DataScript.ScriptID.Name, "ScriptID")
                  .Add(Tz.Data.TzAccount.DataScript.Script.Name, "Script")
                  .Add(Tz.Data.TzAccount.DataScript.ScriptName.Name, "Name")
                  .Add(Tz.Data.TzAccount.DataScript.Category.Name, "Category")                  
                  , null, null).ToList();            
            return c;
        }
        public bool Save() {
            if (ScriptID == "")
            {
                 this.ScriptID= dScript.Save(Script,Name,Category);
                if (this.ScriptID != "")
                {
                    return true;
                }
                else {
                    return false;
                }                
            }
            else {
                return dScript.Update(ScriptID,Script,Name,Category);
            }            
        }
        public bool Remove() {
            return dScript.Remove(this.ScriptID);
        }
        private void Load() {
            DataTable dt = new DataTable();
            dt = dScript.GetScript(this.ScriptID);
            foreach (DataRow dr in dt.Rows) {
                Script = dr["Script"] == null ? "" : dr["Script"].ToString();
                Script = dr["ScriptName"] == null ? "" : dr["ScriptName"].ToString();
                Script = dr["Category"] == null ? "" : dr["Category"].ToString();
            }
        }        
        public dynamic GetData(string ServerID,string returnstring) {
            EvaluationParam ev = new EvaluationParam("connection", new Server(ServerID).Connection());
            sq = new QScriptStatement(Script, ev);
            var res = sq.Evaluation();
            return res[returnstring];
        }
    }
}
