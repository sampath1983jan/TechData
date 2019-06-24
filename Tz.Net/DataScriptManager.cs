using System.Collections.Generic;

namespace Tz.Net
{
    public class DataScriptManager
    {
        public string Intend { get; set; }
        private DataScript dataScript { get; set; }
        public List< Params> InputParam { get; set; }
        ClientServer cs;
        private Server s;
        public DataScriptManager(string intend,string clientID) {
            Intend = intend;
            cs = new ClientServer(clientID);
            s= cs.GetServer();
            ScriptIntend scriptIntend = new ScriptIntend(intend);
            if (scriptIntend.ScriptID == "") {
                throw new System.Exception("Data:Script ID Not exist", null);
            }
                dataScript = new DataScript(scriptIntend.ScriptID);
            InputParam = new List<Params>();
        }

        public DataScriptManager AddParam(string name,string value)
        {
            InputParam.Add(new Params() { Name = name, Value = value });
            return this;
        }
        public dynamic GetResult(string returnValue) {
            try {
                return dataScript.GetData(s.ServerID, returnValue, InputParam);
            }
            catch (System.Exception Ex)
            {
                return Ex.Message;
            }

        }
    }
}
