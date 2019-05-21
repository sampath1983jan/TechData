using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Domain
{
    public class Evalution : Element
    {
        /// <summary>
        /// 
        /// </summary>
        private dynamic _result;
        /// <summary>
        /// 
        /// </summary>
        public override dynamic EvalutionResult => _result;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitor"></param>
        public override void Accept(IVisitor visitor)
        {
            //visitor.Visit(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="Right"></param>
        public override void setResult(string left, object right)
        {
            _result = new Result();
            _result.left = left;
            _result.right = right;
        }
    }
}
