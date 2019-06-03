using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.QScript.Syntax;
using Tech.QScript;

namespace Tz.Net
{
   public class DataScript
    {
        public string ScriptID;        
        public string Script;
        private QScriptStatement sq;        
        public DataScript(string ServerID) {
            EvaluationParam ev = new EvaluationParam("connection", new Server(ServerID).Connection());
            sq = new QScriptStatement(Script, ev);                   
        }
        public dynamic GetData(string returnstring) {
            var res = sq.Evaluation();
            return res[returnstring];
        }
    }
}
