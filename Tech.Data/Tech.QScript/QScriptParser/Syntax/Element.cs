using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript.Syntax
{
    public interface Element
    {
         dynamic EvalutionResult { get; }
        void Accept(IVisitor visitor);
        void setResult(string name, object Right);
    }
}
