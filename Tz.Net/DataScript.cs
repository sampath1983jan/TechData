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
        public List<ScriptIntend> ScriptIntends;
        private Data.DataScript dScript;
        public DataScript(string scriptID) {
            ScriptID= scriptID;
            Script = "";
            Name = "";
            Category = "";
            dScript = new Data.DataScript();
            ScriptIntends = new List<ScriptIntend>();
            Load();
        }
        public void AddIntend(string intend) {
            if (intend != "") {
                ScriptIntends.Add(new ScriptIntend(ScriptID, intend));
            }            
        }
        public DataScript(string scriptname, string category, string script) {
            Script = script;
            Name =scriptname;
            Category = category;
            ScriptID = "";
            dScript = new Data.DataScript();
            ScriptIntends = new List<ScriptIntend>();
        }
        public DataScript() {
            Script = "";
            Name = "";
            Category = "";
            ScriptID = "";
            ScriptIntends = new List<ScriptIntend>();
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
                    foreach (ScriptIntend si in ScriptIntends) {
                        si.Save();
                    }
                    return true;
                }
                else {
                    return false;
                }                
            }
            else {
                 dScript.Update(ScriptID,Script,Name,Category);
                foreach (ScriptIntend si in ScriptIntends)
                {
                    si.Save();
                }
                return true;
            }            
        }
        public bool Remove() {
             dScript.Remove(this.ScriptID);
            foreach (ScriptIntend si in ScriptIntends)
            {
                si.Remove();
            }
            return true;
        }
        private void Load() {
            DataTable dt = new DataTable();
            dt = dScript.GetScript(this.ScriptID);
            foreach (DataRow dr in dt.Rows) {
                Script = dr["Script"] == null ? "" : dr["Script"].ToString();
                Script = dr["ScriptName"] == null ? "" : dr["ScriptName"].ToString();
                Script = dr["Category"] == null ? "" : dr["Category"].ToString();
            }
            ScriptIntends.Add(new ScriptIntend(this.ScriptID,""));
        }        
        public dynamic GetData(string ServerID,string returnstring, List<Params> InputParam=null) {
            EvaluationParam ev = new EvaluationParam("connection", new Server(ServerID).Connection());
            foreach (Params p in InputParam) {
                ev.AddProperty(p.Name, p.Value);
            }
            try
            {
                sq = new QScriptStatement(Script, ev);
                var res = sq.Evaluation();
                return res[returnstring];
            }
            catch (System.Exception Ex) {
                throw Ex;
            }            
        }
    }
}
