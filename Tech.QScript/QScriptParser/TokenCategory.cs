using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.QScript
{
    public enum TokenCatagory
    {      
        Unknown,
        WhiteSpace,
        Query,
        Declaration,
        Expression,
        Function,
        Statement,
        Invalid,
        Literal,
    }
}
