using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript
{
    public class EvaluationParam: Result
    {
        public EvaluationParam(Dictionary<string,object> list) {
            foreach (KeyValuePair<string, object> item in list)
            {
                this.AddProperty(item.Key, item.Value);
            }            
        }
        public EvaluationParam(string name,object value)
        {                      
                this.AddProperty(name,value);            
        }
         
    } 

}


